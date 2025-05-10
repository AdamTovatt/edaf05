namespace Lab6.Algorithms
{
    public class MaxFlowNetwork
    {
        private readonly int _nodeCount;
        private readonly List<FlowEdge>[] _adjacencyList;
        private int[] _level = null!;
        private int[] _nextEdge = null!;

        public MaxFlowNetwork(int nodeCount)
        {
            _nodeCount = nodeCount;
            _adjacencyList = new List<FlowEdge>[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                _adjacencyList[i] = new List<FlowEdge>();
        }

        public void AddUndirectedEdge(int from, int to, int capacity)
        {
            FlowEdge forward = new FlowEdge(to, capacity);
            FlowEdge backward = new FlowEdge(from, capacity);
            forward.Reverse = backward;
            backward.Reverse = forward;

            _adjacencyList[from].Add(forward);
            _adjacencyList[to].Add(backward);
        }

        public void RemoveEdge(int from, int to)
        {
            _adjacencyList[from].RemoveAll(e => e.To == to);
            _adjacencyList[to].RemoveAll(e => e.To == from);
        }

        public int ComputeMaxFlow(int source, int sink)
        {
            int flow = 0;
            while (BuildLevelGraph(source, sink))
            {
                _nextEdge = new int[_nodeCount];
                int pushed;
                while ((pushed = Dfs(source, sink, int.MaxValue)) > 0)
                    flow += pushed;
            }
            return flow;
        }

        private bool BuildLevelGraph(int source, int sink)
        {
            _level = new int[_nodeCount];
            for (int i = 0; i < _nodeCount; i++) _level[i] = -1;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            _level[source] = 0;

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                foreach (FlowEdge edge in _adjacencyList[node])
                {
                    if (edge.Capacity > 0 && _level[edge.To] == -1)
                    {
                        _level[edge.To] = _level[node] + 1;
                        queue.Enqueue(edge.To);
                    }
                }
            }

            return _level[sink] != -1;
        }

        private int Dfs(int node, int sink, int flow)
        {
            if (node == sink)
                return flow;

            for (; _nextEdge[node] < _adjacencyList[node].Count; _nextEdge[node]++)
            {
                FlowEdge edge = _adjacencyList[node][_nextEdge[node]];
                if (edge.Capacity > 0 && _level[edge.To] == _level[node] + 1)
                {
                    int pushed = Dfs(edge.To, sink, Math.Min(flow, edge.Capacity));
                    if (pushed > 0)
                    {
                        edge.Capacity -= pushed;
                        edge.Reverse.Capacity += pushed;
                        return pushed;
                    }
                }
            }

            return 0;
        }

        private class FlowEdge
        {
            public int To { get; }
            public int Capacity { get; set; }
            public FlowEdge Reverse { get; set; } = null!;

            public FlowEdge(int to, int capacity)
            {
                To = to;
                Capacity = capacity;
            }
        }
    }
}
