using System;
using System.Data;

namespace Back_End.Bank
{
    public class Transaction
    {
        public string SourceAccountId { get; set; }
        public string DestinationAccountId { get; set; }
        public DateTime Date { get; set; } // todo : type correction
        
        public void SetDate(string timeString)
        {
            Time = DateTime.ParseExact(timeString, "yyyy-MM-dd HH:mm",null);
        }
        public DateTime Time { get; set; } // todo : type correction

       
        public void SetTime(string timeString)
        {
            Time = DateTime.ParseExact(timeString, "yyyy-MM-dd HH:mm:ss",null);
        }
        public string TrackingId { get; set; }
        public long Amount { get; set; }
        public string Type { get; set; }
        public string TransactionId { get; set; }
    }
}