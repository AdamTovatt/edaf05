namespace Lab6.Models
{
    public class Graph
    {
        public List<Node> Nodes { get; }
        public List<Edge?> Edges { get; }

        private Graph(List<Node> nodes, List<Edge?> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }

        public static Graph CreateFromInputData(InputData input)
        {
            List<Node> nodes = new List<Node>(input.NodeCount);
            for (int i = 0; i < input.NodeCount; i++)
                nodes.Add(new Node(i));

            List<Edge?> edges = new List<Edge?>(input.EdgeCount);
            for (int i = 0; i < input.Edges.Count; i++)
            {
                InputEdge inputEdge = input.Edges[i];
                Node start = nodes[inputEdge.Start];
                Node end = nodes[inputEdge.End];
                Edge edge = new Edge(start, end, inputEdge.Capacity);
                edges.Add(edge);
                start.Edges.Add(edge);
                end.Edges.Add(edge);
            }

            return new Graph(nodes, edges);
        }

        public FlowPath FindAugmentingPath(Node source, Node sink)
        {
            Dictionary<Node, Edge> cameFrom = new Dictionary<Node, Edge>();
            Queue<Node> queue = new Queue<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            queue.Enqueue(source);
            visited.Add(source);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                foreach (Edge edge in current.Edges)
                {
                    Node neighbor = edge.GetOther(current);
                    if (visited.Contains(neighbor)) continue;

                    if (edge.ResidualCapacity(current, neighbor) > 0)
                    {
                        cameFrom[neighbor] = edge;
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);

                        if (neighbor == sink)
                            return ReconstructPath(source, sink, cameFrom);
                    }
                }
            }

            return new FlowPath(source, new List<Edge>());
        }

        private FlowPath ReconstructPath(Node source, Node sink, Dictionary<Node, Edge> cameFrom)
        {
            List<Edge> path = new();
            Node current = sink;

            while (current != source)
            {
                Edge edge = cameFrom[current];
                path.Insert(0, edge);
                current = edge.GetOther(current);
            }

            return new FlowPath(source, path);
        }

        public void RemoveEdgeByIndex(int index)
        {
            if (index < 0 || index >= Edges.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Edge? edge = Edges[index];
            if (edge == null) return;

            edge.Detach();
            Edges[index] = null;
        }

        public void ResetFlows()
        {
            foreach (Edge? edge in Edges)
            {
                if (edge != null)
                    edge.Flow = 0;
            }
        }
    }
}
