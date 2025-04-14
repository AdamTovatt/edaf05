namespace Lab1.Models
{
    public readonly struct SearchState
    {
        public Node Node { get; } // the node that is being searched
        public int Distance { get; } // the distance to that node

        public SearchState(Node node, int distance)
        {
            Node = node;
            Distance = distance;
        }
    }
}
