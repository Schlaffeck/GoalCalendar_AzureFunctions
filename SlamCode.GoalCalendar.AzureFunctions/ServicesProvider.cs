
using SlamCode.GoalCalendar.AzureFunctions.Services.CloudStorage;
using SlamCode.GoalCalendar.AzureFunctions.Services.Formatters;

namespace SlamCode.GoalCalendar.AzureFunctions
{
    public class ServicesProvider
    {
        public static IStorageService GetStorageService()
        {
            return new CloudStorageService("UseDevelopmentStorage=true");
        }

        public static IDataFormatter GetDataFormatter()
        {
            return new ResponseDataFormatter();
        }
    }
}
