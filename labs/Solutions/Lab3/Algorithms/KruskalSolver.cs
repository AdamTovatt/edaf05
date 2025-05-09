using Common;
using Lab3.Models;

namespace Lab3.Algorithms
{
    public static class KruskalSolver
    {
        public static int ComputeMinimumTotalWeight(InputData input, SectionTimer? sectionTimer = null)
        {
            List<Connection> connections = input.Connections;

            sectionTimer?.StartSection("sortConnections");
            connections.Sort((a, b) => a.Weight.CompareTo(b.Weight)); // sort ascending aka from lowest to highest
            sectionTimer?.StopSection("sortConnections");

            UnionFind unionFind = new UnionFind(input.NumberOfPeople); // initialize a new instance of the union find class, in this, each person is their own "tree"

            int totalWeight = 0;
            int edgesUsed = 0;

            sectionTimer?.StartSection("createTree");
            foreach (Connection connection in connections)
            {
                // will try to union the nodes / persons (whatever you want to call it) so they are considered part of the same tree
                // this means that the trees that they are part of will end up with the same root
                bool didUnion = unionFind.Union(connection.PersonA, connection.PersonB); // if they were already part of the same tree this will return false

                if (didUnion) // if we actually changed anything and now connected two previously not connected trees let's...
                {
                    totalWeight += connection.Weight; // increase the total tree weight with the weight that was added to it
                    edgesUsed++; // remember that we used one edge more in this total tree we're building

                    if (edgesUsed == input.NumberOfPeople - 1)
                        break; // we should've gone through all the edges at this point
                }
            }
            sectionTimer?.StopSection("createTree");

            return totalWeight;
        }
    }
}
