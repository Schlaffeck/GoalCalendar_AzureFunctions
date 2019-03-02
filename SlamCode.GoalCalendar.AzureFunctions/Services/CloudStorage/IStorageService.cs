using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace SlamCode.GoalCalendar.AzureFunctions.Services.CloudStorage
{
    public interface IStorageService
    {
        Task<CloudBlockBlob> GetCloudBlobReferenceAsync(string containerName, string cloudBlockBlobName);
    }
}
