using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace SlamCode.GoalCalendar.AzureFunctions.Services.CloudStorage
{
    public class CloudStorageService : IStorageService
    {
        private readonly CloudStorageAccount storageAccount;

        public CloudStorageService(string connectionString)
        {
            this.storageAccount = CloudStorageAccount.Parse(connectionString);
        }

        public async Task<CloudBlockBlob> GetCloudBlobReferenceAsync(string containerName, string cloudBlockBlobName)
        {
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync(
              BlobContainerPublicAccessType.Container,
              new BlobRequestOptions(),
              new OperationContext());
            CloudBlockBlob blob = container.GetBlockBlobReference(cloudBlockBlobName);
            return blob;
        }
    }
}
