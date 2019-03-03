using Newtonsoft.Json.Linq;

namespace SlamCode.GoalCalendar.AzureFunctions.Services.Formatters
{
    public class ResponseDataFormatter : IDataFormatter
    {
        public FormatResult FormatInitialData(string intiialDataString, string dataFormatType)
        {
            if (dataFormatType == "application/json")
                return new FormatResult { Formatted = true, FormattedObject = JObject.Parse(intiialDataString), DataType = dataFormatType };

            return new FormatResult { Formatted = false, DataType = dataFormatType };
        }
    }
}
