using System;
using System.Linq;

namespace Back_End.Bank
{
    public class Transaction
    {
        private const string TimeFormat = "HH:mm:ss";
        private const string DateFormat = "MM/dd/yyyy";
        public string Id { get; set; }
        public string SrcAccountId { get; set; }
        public string DestAccountId { get; set; }

        public DateTime? Date { get; set; }

        public string StrDate
        {
            get => Date?.ToString(DateFormat);
            set => Date = DateTime.ParseExact(value, DateFormat, null);
        }

        public DateTime? Time { get; set; }

        public string StrTime
        {
            get => Time?.ToString(TimeFormat);
            set => Time = DateTime.ParseExact(value, TimeFormat, null);
        }

        public string TrackingId { get; set; }
        public long Amount { get; set; }
        public string Type { get; set; }

        public void ValidateProperties()
        {
            var emptyProperties = GetType()
                .GetProperties()
                .Where(info => info.GetValue(this) is null)
                .Select(info => info.Name)
                .ToList();
            if (emptyProperties.Any())
            {
                throw new ArgumentElementNullException(emptyProperties.Single());
            }
        }
    }
}