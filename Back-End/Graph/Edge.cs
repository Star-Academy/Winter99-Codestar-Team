using Back_End.Bank;

namespace Back_End.Graph
{
    public class Edge
    {
        public Account source {get; set;}

        public Account destination { get; set; }
        
        public Transaction transaction {get; set;}

        public Edge(Account source, Account destination, Transaction transaction)
        {
            this.source = source;
            this.destination = destination;
            this.transaction = transaction;
        }
    }
}