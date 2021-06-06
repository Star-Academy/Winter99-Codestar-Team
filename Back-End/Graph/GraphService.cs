using System.ComponentModel;
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
                AddAllPaths(item.Item1, d, currentLength + 1, currentPath);
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
                if (!Graph.ContainsKey(edge.source))
                    Graph.Add(edge.source, new Dictionary<Account, Transaction>());
                if (!Graph.ContainsKey(edge.destination))
                    Graph.Add(edge.destination, new Dictionary<Account, Transaction>());
                if (!Graph[edge.source].ContainsKey(edge.destination))
                    Graph[edge.source].Add(edge.destination, edge.transaction);
            }
        }

        public long GetMaxFlow(Account s, Account t)
        {
            var rGraph = GetResidualGraph();
            var parent = new Dictionary<Account, Account>();
            long maxFlow = 0;
            while (bfs(rGraph, s, t, parent))
            {
                long pathFlow = Int64.MaxValue;
                for (var v = t; v != s ; v = parent[v]) 
                {
                    pathFlow = Math.Min(pathFlow, rGraph[parent[v]][v]);
                }
                
                for (var v = t; v != s ; v = parent[v]) 
                {
                    rGraph[parent[v]][v] -= pathFlow;
                    if(rGraph[v].ContainsKey(parent[v]))
                        rGraph[v][parent[v]] += pathFlow;
                    else
                        rGraph[v][parent[v]] = pathFlow;
                }
                maxFlow += pathFlow;
            }
            return maxFlow;
        }

        private Dictionary<Account, Dictionary<Account, long>> GetResidualGraph()
        {
            var rGraph = new Dictionary<Account, Dictionary<Account, long>>();
            foreach (var i in Graph)
            {
                rGraph[i.Key] = new Dictionary<Account, long>();
                foreach (var e in i.Value)
                {
                    rGraph[i.Key][e.Key] = e.Value.Amount;
                }
            }
            return rGraph;
        }

        private bool bfs(Dictionary<Account, Dictionary<Account, long>> Graph, Account s, Account t, Dictionary<Account, Account> parent)
        {
            var visited = new HashSet<Account> { s };
            var queue = new List<Account>();

            while (queue.Count > 0)
            {
                var u = queue[0];
                queue.RemoveAt(0);

                foreach (var entry in Graph[u])
                {
                    var v = entry.Key;
                    if (!visited.Contains(v) && Graph[u][v] > 0)
                    {
                        parent[v] = u;
                        if (v == t)
                            return true;
                        queue.Add(v);
                        visited.Add(v);
                    }
                }
            }
            return false;
        }
    }
}