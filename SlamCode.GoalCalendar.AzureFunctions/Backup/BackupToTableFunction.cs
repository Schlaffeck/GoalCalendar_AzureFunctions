using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SlamCode.GoalCalendar.AzureFunctions.Backup.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Diagnostics;

namespace SlamCode.GoalCalendar.AzureFunctions.Backup
{
    public static class BackupToTableFunction
    {
        [FunctionName("BackupToTableFunction")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", "put", Route = Consts.Backups.ApiRouteBase + "/{version}/{id}")]HttpRequestMessage req,
            string version,
            string id,
            [Table(Consts.Backups.TableStorageName, "{version}", "{id}")] BackupKey backupKey,
            [Table(Consts.Backups.TableStorageName)] CloudTable table,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var createdNew = false;
            if (backupKey == null)
            {
                createdNew = true;
                backupKey = new BackupKey
                {
                    PartitionKey = version,
                    RowKey = id,
                    Id = Guid.NewGuid()
                };

                var insert = TableOperation.Insert(backupKey);
                await table.ExecuteAsync(insert);
            }

            var retreiveData = TableOperation.Retrieve<BackupData>(version, id);
            var backupDataResult = await table.ExecuteAsync(retreiveData);
            var backupData = backupDataResult.Result as BackupData;

            if (backupData == null)
            {
                backupData = new BackupData();
            }

            backupData.BackupVersion = Consts.Backups.BackupVersion;
            backupData.DataFormat = req.Content.Headers.ContentType.MediaType;
            backupData.Data = await req.Content.ReadAsStringAsync();
            backupData.LastSaveUTCTime = DateTime.UtcNow;

            log.Info($"Saving read data: {backupData.Data}");

            var storageService = ServicesProvider.GetStorageService();
            var blob = await storageService.GetCloudBlobReferenceAsync(Consts.Backups.BackupsBlobContainerName, $"{version}_{id}");

            blob.Properties.ContentType = req.Content.Headers.ContentType.MediaType;
            await blob.UploadTextAsync(backupData.Data);

            backupData.Data = blob.Uri.ToString();

            var update = TableOperation.InsertOrReplace(backupData);
            var updateResult = await table.ExecuteAsync(update);

            return req.CreateResponse(createdNew ? HttpStatusCode.Created : HttpStatusCode.Accepted);
        }
    }
}
