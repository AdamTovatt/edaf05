namespace Lab1.Models
{
    public readonly struct SearchState
    {
        public Node Node { get; }
        public int Distance { get; }

        public SearchState(Node node, int distance)
        {
            Node = node;
            Distance = distance;
        }
    }
}
