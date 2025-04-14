using Lab1.Models;

namespace Lab1.Helpers
{
    public static class PathFinder
    {
        /// <summary>
        /// Will find the shortest path from the start node to the end node.
        /// </summary>
        /// <param name="start">The node to start from.</param>
        /// <param name="end">The node to end at.</param>
        public static int? FindShortestPath(Node start, Node end)
        {
            if (start == end)
                return 0;

            Queue<SearchState> queue = new Queue<SearchState>(); // a queue of nodes and distances to them
            HashSet<string> visited = new HashSet<string>(); // keeps track of what we've visited and not

            queue.Enqueue(new SearchState(start, 0)); // let's add the start node to the queue
            visited.Add(start.Word); // ... and the word of the start node to words that we've visited

            while (queue.Count > 0) // as long as we have something to do, do it
            {
                SearchState current = queue.Dequeue(); // take thte next node ("SearchState") to process

                foreach (Node neighbor in current.Node.Neighbors) // go through all the nodes next to the current node
                {
                    if (visited.Contains(neighbor.Word))
                        continue; // if we've checked this word, just skip it

                    if (neighbor == end) // we've reached our target
                        return current.Distance + 1;

                    // if we get here, we've not reached our target

                    visited.Add(neighbor.Word);
                    queue.Enqueue(new SearchState(neighbor, current.Distance + 1)); // let's add this neighbor to the queue of nodes to search
                }
            }

            return null;
        }
    }
}
