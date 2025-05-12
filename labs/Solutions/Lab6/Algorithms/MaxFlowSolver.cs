namespace Lab6.Algorithms
{
    public class MaxFlowSolver
    {
        private readonly int _nodeCount;
        private readonly List<Edge>[] _adjacency;
        private readonly List<Edge> _allEdges;

        public MaxFlowSolver(int nodeCount)
        {
            _nodeCount = nodeCount;
            _adjacency = new List<Edge>[nodeCount];
            _allEdges = new List<Edge>();
            for (int i = 0; i < nodeCount; i++)
                _adjacency[i] = new List<Edge>();
        }

        public void AddEdge(int from, int to, int capacity)
        {
            Edge edge = new Edge(from, to, capacity);
            _adjacency[from].Add(edge);
            _adjacency[to].Add(edge);
            _allEdges.Add(edge);
        }

        public void DeactivateEdge(int index)
        {
            _allEdges[index].IsActive = false;
        }

        public void ResetFlows()
        {
            foreach (Edge edge in _allEdges)
                edge.Flow = 0;
        }

        public int ComputeMaxFlow(int source, int sink)
        {
            int flow = 0;
            while (true)
            {
                Edge[] parent = new Edge[_nodeCount];
                Queue<int> queue = new Queue<int>();
                bool[] visited = new bool[_nodeCount];

                visited[source] = true;
                queue.Enqueue(source);

                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();

                    foreach (Edge edge in _adjacency[current])
                    {
                        if (!edge.IsActive) continue;

                        int next = edge.GetOther(current);
                        if (visited[next]) continue;
                        if (edge.RemainingCapacity(current, next) <= 0) continue;

                        parent[next] = edge;
                        visited[next] = true;
                        queue.Enqueue(next);

                        if (next == sink)
                            break;
                    }
                }

                if (!visited[sink])
                    break;

                int bottleneck = int.MaxValue;
                int node = sink;
                while (node != source)
                {
                    Edge edge = parent[node];
                    int prev = edge.GetOther(node);
                    bottleneck = System.Math.Min(bottleneck, edge.RemainingCapacity(prev, node));
                    node = prev;
                }

                node = sink;
                while (node != source)
                {
                    Edge edge = parent[node];
                    int prev = edge.GetOther(node);
                    edge.AddFlow(prev, node, bottleneck);
                    node = prev;
                }

                flow += bottleneck;
            }

            return flow;
        }

        private class Edge
        {
            public readonly int U;
            public readonly int V;
            public readonly int Capacity;
            public int Flow;
            public bool IsActive;

            public Edge(int u, int v, int capacity)
            {
                U = System.Math.Min(u, v);
                V = System.Math.Max(u, v);
                Capacity = capacity;
                Flow = 0;
                IsActive = true;
            }

            public int GetOther(int node) => node == U ? V : U;

            public int RemainingCapacity(int from, int to)
            {
                if (from == U && to == V)
                    return Capacity - Flow;
                if (from == V && to == U)
                    return Capacity + Flow;
                return 0;
            }

            public void AddFlow(int from, int to, int amount)
            {
                if (from == U && to == V)
                    Flow += amount;
                else if (from == V && to == U)
                    Flow -= amount;
            }
        }
    }
}
