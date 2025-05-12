namespace Lab6.Models
{
    public readonly struct EdgeReference
    {
        public int From { get; }
        public int To { get; }

        public EdgeReference(int from, int to)
        {
            From = from;
            To = to;
        }
    }
}