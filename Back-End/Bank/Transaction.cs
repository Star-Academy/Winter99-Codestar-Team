using System;
using System.Data;

namespace Back_End.Bank
{
    public class Transaction
    {
        private static readonly string TimeFormat = "HH:mm:ss", DateFormat = "MM/dd/yyyy";
        public string Id { get; set; }
        public string SrcAccountId { get; set; }
        public string DestAccountId { get; set; }
        
        public DateTime? Date { get; set; }
        public string StrDate
        {
            get => Date?.ToString(DateFormat);
            set => Date =DateTime.ParseExact(value, DateFormat,null);
        }

        public DateTime? Time { get; set; }
        public string StrTime
        {
            get => Time?.ToString(TimeFormat);
            set => Time =DateTime.ParseExact(value, TimeFormat,null);
        }
        
        public string TrackingId { get; set; }
        public long Amount { get; set; }
        public string Type { get; set; }
    }
}