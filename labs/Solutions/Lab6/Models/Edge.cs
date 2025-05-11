namespace Lab6.Models
{
    public class Edge
    {
        public int From { get; }
        public int To { get; }
        public int Capacity { get; }

        public int FlowForward { get; set; } = 0;

        public int FlowBackward => -FlowForward;

        public int ResidualCapacityTo(int target)
        {
            if (target == To)
                return Capacity - FlowForward; // forward direction
            if (target == From)
                return FlowForward; // backward capacity
            throw new ArgumentException("Target must be From or To");
        }

        public void AddFlowTo(int target, int amount)
        {
            if (target == To)
                FlowForward += amount;
            else if (target == From)
                FlowForward -= amount;
            else
                throw new ArgumentException("Target must be From or To");
        }

        public Edge(int from, int to, int capacity)
        {
            From = from;
            To = to;
            Capacity = capacity;
        }
    }
}
