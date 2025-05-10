namespace Lab6.Models
{
    public readonly struct SolveResult
    {
        public int RoutesRemoved { get; }
        public int FinalFlow { get; }

        public SolveResult(int routesRemoved, int finalFlow)
        {
            RoutesRemoved = routesRemoved;
            FinalFlow = finalFlow;
        }

        public override string ToString()
        {
            return $"{RoutesRemoved} {FinalFlow}";
        }
    }
}
