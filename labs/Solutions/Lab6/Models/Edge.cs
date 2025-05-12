namespace Lab6.Models
{
    public class Edge
    {
        public Node Start { get; }
        public Node End { get; }
        public int OriginalCapacity { get; }
        public int Flow { get; set; }
        public int RemainingCapacity => OriginalCapacity - Flow;

        public Edge(Node from, Node to, int originalCapacity)
        {
            Start = from;
            End = to;
            OriginalCapacity = originalCapacity;
            Flow = 0;
        }

        public Node GetOther(Node node)
        {
            if (node == Start) return End;
            if (node == End) return Start;
            throw new ArgumentException("Node not part of this edge.");
        }

        public void AddFlow(int amount)
        {
            Flow += amount;
        }

        public void Detach()
        {
            Start.Edges.RemoveAll(e => ReferenceEquals(e, this));
            End.Edges.RemoveAll(e => ReferenceEquals(e, this));
        }

        public override string ToString()
        {
            return $"{Start} -({Flow} / {OriginalCapacity})-> {End}";
        }

        public string ToStringEnd()
        {
            return $" -({Flow} / {OriginalCapacity})-> {End}";
        }
    }
}
