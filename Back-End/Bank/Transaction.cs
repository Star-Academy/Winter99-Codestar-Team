using System;
using System.Data;

namespace Back_End.Bank
{
    public class Transaction
    {
        private static readonly string TIME_FORMAT = "HH:mm:ss" ,DATE_FORMAT = "MM/dd/yyyy" ;
        public string Id { get; set; }
        public string SrcAccountId { get; set; }
        public string DestAccountId { get; set; }

        private DateTime _date;
        public DateTime GetDate()
        {
            return _date;
        }
        public void SetDate(DateTime date)
        {
            _date = date;
        }
        public void SetDate(string dateString)
        {
            SetDate(DateTime.ParseExact(dateString, DATE_FORMAT,null));
        }

        private DateTime _time;
        public DateTime GetTime()
        {
            return _time;
        }
        public void SetTime(DateTime time)
        {
            _time = time;
        }
        public void SetTime(string timeString)
        {
            SetTime(DateTime.ParseExact(timeString, TIME_FORMAT,null));
        }
        
        public string TrackingId { get; set; }
        public long Amount { get; set; }
        public string Type { get; set; }
    }
}