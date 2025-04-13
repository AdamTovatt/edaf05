using Lab1.Helpers;

namespace Lab1.Models
{
    public class Graph
    {
        private readonly Dictionary<string, Node> _nodes;

        public Graph(IEnumerable<string> words)
        {
            _nodes = words.ToDictionary(w => w, w => new Node(w));
            BuildEdges();
        }

        public Node GetNode(string word) => _nodes[word];

        private void BuildEdges()
        {
            foreach (Node from in _nodes.Values)
            {
                string suffix = from.Word.Substring(1);
                Dictionary<char, int> requiredCounts = suffix.GetCharacterCountDictionary();

                foreach (Node to in _nodes.Values)
                {
                    if (from == to)
                        continue;

                    if (to.Word.ContainsAll(requiredCounts))
                        from.Neighbors.Add(to);
                }
            }
        }
    }
}
