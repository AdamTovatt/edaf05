using Lab3.Models;

namespace Lab3.Algorithms
{
    public static class KruskalSolver
    {
        public static int ComputeMinimumTotalWeight(InputData input)
        {
            List<Connection> connections = input.Connections;

            connections.Sort((a, b) => a.Weight.CompareTo(b.Weight));

            UnionFind unionFind = new UnionFind(input.NumberOfPeople);

            int totalWeight = 0;
            int edgesUsed = 0;

            foreach (Connection connection in connections)
            {
                if (unionFind.Union(connection.PersonA, connection.PersonB))
                {
                    totalWeight += connection.Weight;
                    edgesUsed++;

                    if (edgesUsed == input.NumberOfPeople - 1)
                        break;
                }
            }

            return totalWeight;
        }
    }
}
