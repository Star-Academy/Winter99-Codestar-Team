namespace Back_End.Bank
{
    public class BriefAccount
    {
        public static BriefAccount Convert(Account account)
        {
            return new BriefAccount(account.Id, account.OwnerName, account.OwnerFamily);
        }

        private BriefAccount(string id, string ownerName, string ownerFamily)
        {
            Id = id;
            OwnerName = ownerName;
            OwnerFamily = ownerFamily;
        }

        public string Id { get; }
        public string OwnerName { get; }
        public string OwnerFamily { get; }
    }
}