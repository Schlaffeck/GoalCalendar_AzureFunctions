using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace SlamCode.GoalCalendar.AzureBlobStorage
{
    public interface IStorageService
    {
        Task<CloudBlockBlob> GetCloudBlobReferenceAsync(string containerName, string cloudBlockBlobName);
    }
}
