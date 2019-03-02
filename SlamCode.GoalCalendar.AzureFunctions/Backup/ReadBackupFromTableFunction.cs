using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using SlamCode.GoalCalendar.AzureFunctions.Backup.Models;

namespace SlamCode.GoalCalendar.AzureFunctions.Backup
{
    public static class ReadBackupFromTableFunction
    {
        [FunctionName("ReadBackupFromTableFunction")]
        public async static Task<HttpResponseMessage> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = Consts.Backups.ApiRouteBase + "/{version}/{id}")]HttpRequestMessage req,
            string version,
            string id,
            [Table(Consts.Backups.TableStorageName, "{version}", "{id}")] BackupData backupData,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            if (backupData == null)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }

            var storageService = ServicesProvider.GetStorageService();
            var blob = await storageService.GetCloudBlobReferenceAsync(Consts.Backups.BackupsBlobContainerName, $"{version}_{id}");
            var text = await blob.DownloadTextAsync();

            return req.CreateResponse(HttpStatusCode.OK, text, backupData.DataFormat);
        }
    }
}
