using System.Text;

namespace Lab6.Models
{
    public class FlowPath
    {
        private readonly List<Edge> _edges;
        private readonly Node _source;

        public FlowPath(Node source, List<Edge> edges)
        {
            _source = source;
            _edges = edges;
        }

        public bool IsEmpty => _edges.Count == 0;

        public int CalculateBottleneck()
        {
            int maxPossibleFlow = int.MaxValue;
            Node current = _source;

            foreach (Edge edge in _edges)
            {
                Node next = edge.GetOther(current);
                int residual = edge.RemainingCapacity;
                maxPossibleFlow = Math.Min(maxPossibleFlow, residual);
                current = next;
            }

            return maxPossibleFlow;
        }

        public void ApplyFlow(int flow)
        {
            Node current = _source;

            foreach (Edge edge in _edges)
            {
                Node next = edge.GetOther(current);
                edge.AddFlow(flow);
                current = next;
            }
        }

        public override string ToString()
        {
            if (IsEmpty)
                return "[Empty path]";

            StringBuilder stringBuilder = new StringBuilder();
            bool first = true;

            foreach (Edge edge in _edges)
            {
                if (first)
                {
                    stringBuilder.Append(edge.ToString());
                    first = false;
                }
                else
                {
                    stringBuilder.Append(edge.ToStringEnd());
                }
            }

            return stringBuilder.ToString();
        }
    }
}
