using Lab1.Models;

namespace Lab1.Helpers
{
    public static class PathFinder
    {
        public static int? FindShortestPath(Node start, Node end)
        {
            if (start == end)
                return 0;

            Queue<SearchState> queue = new Queue<SearchState>();
            HashSet<string> visited = new HashSet<string>();

            queue.Enqueue(new SearchState(start, 0));
            visited.Add(start.Word);

            while (queue.Count > 0)
            {
                SearchState current = queue.Dequeue();

                foreach (Node neighbor in current.Node.Neighbors)
                {
                    if (visited.Contains(neighbor.Word))
                        continue;

                    if (neighbor == end)
                        return current.Distance + 1;

                    visited.Add(neighbor.Word);
                    queue.Enqueue(new SearchState(neighbor, current.Distance + 1));
                }
            }

            return null;
        }
    }
}
