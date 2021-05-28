using System.Collections.Generic;

namespace Back_End.Bank
{
    public class Account
    {
        public Account(string id, string cardId, string sheba, string type, string branchTelephone, string branchAddress, string branchName, string ownerName, string ownerFamily, string ownerId)
        {
            Id = id;
            CardId = cardId;
            Sheba = sheba;
            Type = type;
            BranchTelephone = branchTelephone;
            BranchAddress = branchAddress;
            BranchName = branchName;
            OwnerName = ownerName;
            OwnerFamily = ownerFamily;
            OwnerId = ownerId;
        }
        
        public Account(){}
        public string Id { get; set; }
        public string CardId { get; set; }
        public string Sheba { get; set; }
        public string Type { get; set; }
        public string BranchTelephone { get; set; }
        public string BranchAddress { get; set; }
        public string BranchName { get; set; }
        public string OwnerName { get; set; }
        public string OwnerFamily { get; set; }
        public string OwnerId { get; set; }

        public List<string> SrcTransactions { get; set; }
        public List<string> DestTransactions { get; set; }
        
    }
}