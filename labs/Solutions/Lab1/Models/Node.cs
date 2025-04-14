namespace Lab1.Models
{
    public class Node
    {
        public string Word { get; } // the word that this node represents
        public List<Node> Neighbors { get; } // the nodes next to this node

        public Node(string word)
        {
            Word = word;
            Neighbors = new List<Node>();
        }
    }
}
