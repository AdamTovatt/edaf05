namespace Lab6.Algorithms
{
    public abstract class MaxFlowSolverBase : IMaxFlowSolver
    {
        protected readonly int NodeCount;

        protected MaxFlowSolverBase(int nodeCount)
        {
            NodeCount = nodeCount;
        }

        public abstract void AddUndirectedEdge(int from, int to, int capacity);
        public abstract int ComputeMaxFlow(int source, int sink);
    }
}
