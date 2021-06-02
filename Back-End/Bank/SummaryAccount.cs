namespace Back_End.Bank
{
    public class SummaryAccount
    {
        public static SummaryAccount Convert(Account account)
        {
            return new SummaryAccount(account.Id, account.CardId,account.Sheba,account.Type,account.BranchTelephone,account.BranchAddress,account.BranchName,account.OwnerName, account.OwnerFamily,account.OwnerId);
        }
        
        private SummaryAccount(string id, string cardId, string sheba, string type, string branchTelephone, string branchAddress, string branchName, string ownerName, string ownerFamily, string ownerId)
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


        public string Id { get;  }
        public string CardId { get;  }
        public string Sheba { get;  }
        public string Type { get;  }
        public string BranchTelephone { get;  }
        public string BranchAddress { get;  }
        public string BranchName { get;  }
        public string OwnerName { get;  }
        public string OwnerFamily { get;  }
        public string OwnerId { get;  }

    }
}