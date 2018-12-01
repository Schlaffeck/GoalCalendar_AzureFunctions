using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace SlamCode.GoalCalendar.AzureFunctions.Backup.Models
{
    public class BackupKey : TableEntity
    {
        public Guid Id { get; set; }
    }
}
