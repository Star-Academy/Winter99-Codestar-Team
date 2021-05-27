using System;
using System.Data;

namespace Back_End.Bank
{
    public class Transaction
    {
        private static readonly string TIME_FORMAT = "HH:mm:ss" ,DATE_FORMAT = "MM/dd/yyyy" ;
        private DateTime _date;
        public string SourceAccountId { get; set; }
        public string DestinationAccountId { get; set; }

        public void SetDate(DateTime value)
        {
            _date = value;
        }

        public DateTime GetDate()
        {
            return _date;
        }

        public void SetDate(string timeString)
        {
            SetDate(DateTime.ParseExact(timeString, DATE_FORMAT,null));
        }
        public DateTime Time { get; set; } // todo : type correction
        public void SetTime(string timeString)
        {
            Time = DateTime.ParseExact(timeString, TIME_FORMAT,null);
        }
        public string TrackingId { get; set; }
        public long Amount { get; set; }
        public string Type { get; set; }
        public string TransactionId { get; set; }
    }
}