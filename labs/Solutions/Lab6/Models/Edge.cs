namespace Lab6.Models
{
    public class Edge
    {
        public Node Start { get; }
        public Node End { get; }
        public int Capacity { get; }
        public int Flow { get; set; }

        public Edge(Node from, Node to, int capacity)
        {
            Start = from;
            End = to;
            Capacity = capacity;
            Flow = 0;
        }

        public Node GetOther(Node node)
        {
            if (node == Start) return End;
            if (node == End) return Start;
            throw new ArgumentException("Node not part of this edge.");
        }

        public int ResidualCapacity(Node from, Node to)
        {
            if (from == Start && to == End) return Capacity - Flow;
            if (from == End && to == Start) return Flow;
            return 0;
        }

        public void AddFlow(Node from, Node to, int amount)
        {
            if (from == Start && to == End) Flow += amount;
            else if (from == End && to == Start) Flow -= amount;
        }

        public void Detach()
        {
            Start.Edges.Remove(this);
            End.Edges.Remove(this);
        }

        public override string ToString()
        {
            return $"{Start} -({Flow} / {Capacity})-> {End}";
        }

        public string ToStringEnd()
        {
            return $" -({Flow} / {Capacity})-> {End}";
        }
    }
}
