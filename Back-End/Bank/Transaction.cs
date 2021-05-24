namespace Back_End.Models
{
    public class Transaction
    {
        public string SourceAccountId { get; set; }
        public string DestinationAccountId { get; set; }
        public string Date { get; set; } // todo : type correction
        public string Time { get; set; } // todo : type correction
        public string TrackingId { get; set; }
        public long Amount { get; set; }
        public string Type { get; set; }
        public string TransactionId { get; set; }
    }
}