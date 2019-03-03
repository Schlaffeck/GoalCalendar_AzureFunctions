using Newtonsoft.Json.Linq;

namespace SlamCode.GoalCalendar.AzureFunctions.Services.Formatters
{
    public class FormatResult
    {
        public bool Formatted { get; internal set; }
        public object FormattedObject { get; internal set; }
        public string DataType { get; internal set; }
    }
}