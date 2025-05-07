using Common;
using Common.DataSources;
using System.Globalization;

namespace Lab4.Models
{
    public class InputData : IParsableInputData<InputData>
    {
        public List<Point> Points { get; }

        private InputData(List<Point> points)
        {
            Points = points;
        }

        public static InputData Parse(IInputDataSource source)
        {
            string? firstLine = source.ReadLine();

            if (firstLine == null)
                throw new InvalidOperationException("Missing number of points.");

            int n = int.Parse(firstLine, CultureInfo.InvariantCulture);
            List<Point> points = new List<Point>(n);

            for (int i = 0; i < n; i++)
            {
                string? line = source.ReadLine();

                if (line == null)
                    throw new InvalidOperationException($"Expected {n} points but got fewer.");

                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int x = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int y = int.Parse(parts[1], CultureInfo.InvariantCulture);

                points.Add(new Point(x, y));
            }

            return new InputData(points);
        }
    }
}
