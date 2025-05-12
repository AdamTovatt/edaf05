namespace Lab6.Models
{
    public readonly struct SolveResult
    {
        public int RemovedCount { get; }
        public int MaxFlow { get; }

        public SolveResult(int removedCount, int maxFlow)
        {
            RemovedCount = removedCount;
            MaxFlow = maxFlow;
        }

        public override string ToString()
        {
            return $"{RemovedCount} {MaxFlow}";
        }
    }
}
