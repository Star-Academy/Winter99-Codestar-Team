using System;
using System.Collections.Generic;

namespace Back_End.Bank
{
    public class Account
    {
        // todo : remove it if not required
        public Account(string id, string cardId, string sheba, string type, string branchTelephone,
            string branchAddress, string branchName, string ownerName, string ownerFamily, string ownerId)
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

        public Account()
        {
        }

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

        public void ValidateBasicValues()
        {
            if (Id is null)
                throw new ArgumentElementNullException(nameof(Id));
            if (Sheba is null)
                throw new ArgumentElementNullException(nameof(Sheba));
            if (CardId is null)
                throw new ArgumentElementNullException(nameof(CardId));
            if (Type is null)
                throw new ArgumentElementNullException(nameof(Type));
            if (BranchAddress is null)
                throw new ArgumentElementNullException(nameof(BranchAddress));
            if (BranchName is null)
                throw new ArgumentElementNullException(nameof(BranchName));
            if (BranchTelephone is null)
                throw new ArgumentElementNullException(nameof(BranchTelephone));
            if (OwnerId is null)
                throw new ArgumentElementNullException(nameof(OwnerId));
            if (OwnerFamily is null)
                throw new ArgumentElementNullException(nameof(OwnerFamily));
            if (OwnerName is null)
                throw new ArgumentElementNullException(nameof(OwnerName));
        }
    }
}