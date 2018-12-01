using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace SlamCode.GoalCalendar.AzureFunctions.Backup.Models
{
    public class BackupData : TableEntity
    {
        public DateTime LastSaveUTCTime { get; set; }

        public int BackupVersion { get; set; }

        public string DataFormat { get; set; }

        public string Data { get; set; }
    }
}
