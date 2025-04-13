namespace Lab1.Models
{
    public class Node
    {
        public string Word { get; }
        public List<Node> Neighbors { get; }

        public Node(string word)
        {
            Word = word;
            Neighbors = new List<Node>();
        }
    }
}
