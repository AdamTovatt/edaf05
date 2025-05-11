namespace Lab6.Models
{
    public class Node
    {
        public int Id { get; }
        public List<Edge> Edges { get; }

        public Node(int id)
        {
            Id = id;
            Edges = new List<Edge>();
        }
    }
}
