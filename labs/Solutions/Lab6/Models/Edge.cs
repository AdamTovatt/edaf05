namespace Lab6.Models
{
    public readonly struct Edge
    {
        public int From { get; }
        public int To { get; }
        public int Capacity { get; }

        public Edge(int from, int to, int capacity)
        {
            From = from;
            To = to;
            Capacity = capacity;
        }
    }
}
