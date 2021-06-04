using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Back_End.Bank;
using System;

namespace Back_End.Graph
{
    public class GraphService : IGraphService
    {
        private readonly IBankService _bankService;

        private Dictionary<Account, Dictionary<Account, Transaction>> Graph;
        private int MaxLenght;
        public GraphService(IBankService bankService)
        {
            _bankService = bankService;
        }

        public Dictionary<Account, Dictionary<Account, Transaction>> CreateGraph(Account src, Account dest, int maxLenght = 7)
        {
            Graph = new Dictionary<Account, Dictionary<Account, Transaction>>()
            {
              { src, new Dictionary<Account, Transaction>() },
              { dest, new Dictionary<Account, Transaction>() }
            };
            MaxLenght = maxLenght;

            AddAllPaths(src, dest, 0, new List<Edge>());
            return Graph;
        }

        private void AddAllPaths(Account s, Account d, int currentLength, List<Edge> currentPath)
        {
            if (s == d)
            {
                AddPath(currentPath);
                return;
            }
            if (currentLength > MaxLenght)
                return;

            foreach (var item in GetNeighbours(s))
            {
                var newEdge = new Edge(s, item.Item1, item.Item2);
                currentPath.Add(newEdge);
                AddAllPaths(item.Item1, d, currentLength+1, currentPath);
                currentPath.Remove(newEdge);
            }
        }

        private IEnumerable<Tuple<Account, Transaction>> GetNeighbours(Account account)
        {
            return _bankService.GetSrcTransactionsDestinations(account.Id).Zip(_bankService.GetSrcTransactions(account.Id), Tuple.Create);
        }

        private void AddPath(List<Edge> path)
        {
            foreach (var edge in path)
            {
                if(!Graph.ContainsKey(edge.source))
                    Graph.Add(edge.source, new Dictionary<Account, Transaction>());
                if(!Graph.ContainsKey(edge.destination))
                    Graph.Add(edge.destination, new Dictionary<Account, Transaction>());
                if(!Graph[edge.source].ContainsKey(edge.destination))
                    Graph[edge.source].Add(edge.destination, edge.transaction);
            }   
        }

        public int GetMaxFlow(Account s, Account t)
        {
            // todo : implement Ford-fulkerson algorithm
            return 0;
        }
    }
}