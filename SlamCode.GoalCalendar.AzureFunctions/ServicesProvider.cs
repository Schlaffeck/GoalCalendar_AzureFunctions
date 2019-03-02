
using SlamCode.GoalCalendar.AzureFunctions.Services.CloudStorage;

namespace SlamCode.GoalCalendar.AzureFunctions
{
    public class ServicesProvider
    {
        public static IStorageService GetStorageService()
        {
            return new CloudStorageService("UseDevelopmentStorage=true");
        }
    }
}
