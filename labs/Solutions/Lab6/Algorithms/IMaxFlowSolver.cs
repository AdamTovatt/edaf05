namespace Lab6.Algorithms
{
    public interface IMaxFlowSolver
    {
        void AddUndirectedEdge(int from, int to, int capacity);
        int ComputeMaxFlow(int source, int sink);
    }
}
