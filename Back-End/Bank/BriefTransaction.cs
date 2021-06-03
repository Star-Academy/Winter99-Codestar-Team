using System;

namespace Back_End.Bank
{
    public class BriefTransaction
    {
        private const string TimeFormat = "HH:mm:ss";
        private const string DateFormat = "MM/dd/yyyy";
        public string Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public string TrackingId { get; set; }
        public long Amount { get; set; }
        public string Type { get; set; }
    }
}