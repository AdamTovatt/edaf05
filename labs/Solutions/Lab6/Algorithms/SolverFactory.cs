namespace Lab6.Algorithms
{
    public static class SolverFactory
    {
        public static IMaxFlowSolver CreateSolver<TSolver>(int nodeCount)
            where TSolver : MaxFlowSolverBase
        {
            object? instance = Activator.CreateInstance(typeof(TSolver), nodeCount);

            if (instance == null)
                throw new InvalidOperationException($"Failed to instantiate the {typeof(TSolver)} solver.");

            return (IMaxFlowSolver)instance;
        }
    }
}
