namespace SlamCode.GoalCalendar.AzureFunctions.Services.Formatters
{
    public interface IDataFormatter
    {
        FormatResult FormatInitialData(string intiialDataString, string dataFormatType);
    }
}
