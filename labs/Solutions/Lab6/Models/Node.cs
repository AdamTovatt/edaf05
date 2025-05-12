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

        public override string ToString()
        {
            return $"{Id}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Node other)
                return Id == other.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
