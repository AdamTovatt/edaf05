using System.Globalization;

namespace Lab6.Models
{
    public readonly struct InputEdge
    {
        public int From { get; init; }
        public int To { get; init; }
        public int Capacity { get; init; }

        public InputEdge(int start, int end, int capacity)
        {
            From = start;
            To = end;
            Capacity = capacity;
        }

        public static InputEdge FromString(string line)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int from = int.Parse(parts[0], CultureInfo.InvariantCulture);
            int to = int.Parse(parts[1], CultureInfo.InvariantCulture);
            int capacity = int.Parse(parts[2], CultureInfo.InvariantCulture);

            return new InputEdge(from, to, capacity);
        }
    }
}
