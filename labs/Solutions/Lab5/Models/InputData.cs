using Common;
using Common.DataSources;
using System.Globalization;

namespace Lab5.Models
{
    public class InputData : IParsableInputData<InputData>
    {
        public AlignmentContext Context { get; }
        public List<StringPair> Queries { get; }

        private InputData(AlignmentContext context, List<StringPair> queries)
        {
            Context = context;
            Queries = queries;
        }

        public static InputData Parse(IInputDataSource source)
        {
            AlignmentContext context = ParseAlignmentContext(source);
            List<StringPair> queries = ParseQueries(source);
            return new InputData(context, queries);
        }

        private static AlignmentContext ParseAlignmentContext(IInputDataSource source)
        {
            string characters = ParseCharacterLine(source);
            int[,] costMatrix = ParseCostMatrix(source, characters.Length);
            return new AlignmentContext(characters, costMatrix);
        }

        private static string ParseCharacterLine(IInputDataSource source)
        {
            string? line = source.ReadLine();
            if (line == null)
                throw new InvalidOperationException("Missing character line.");

            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            char[] characters = new char[parts.Length];

            for (int i = 0; i < parts.Length; i++)
                characters[i] = parts[i][0];

            return new string(characters);
        }

        private static int[,] ParseCostMatrix(IInputDataSource source, int size)
        {
            int[,] costMatrix = new int[size, size];

            for (int row = 0; row < size; row++)
            {
                string? line = source.ReadLine();
                if (line == null)
                    throw new InvalidOperationException($"Missing row {row + 1} of cost matrix.");

                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < size; col++)
                    costMatrix[row, col] = int.Parse(parts[col], NumberStyles.Integer, CultureInfo.InvariantCulture);
            }

            return costMatrix;
        }

        private static List<StringPair> ParseQueries(IInputDataSource source)
        {
            string? queryCountLine = source.ReadLine();
            if (queryCountLine == null)
                throw new InvalidOperationException("Missing query count.");

            int queryCount = int.Parse(queryCountLine, NumberStyles.Integer, CultureInfo.InvariantCulture);
            List<StringPair> queries = new List<StringPair>(queryCount);

            for (int i = 0; i < queryCount; i++)
            {
                string? line = source.ReadLine();
                if (line == null)
                    throw new InvalidOperationException($"Missing query {i + 1}.");

                int spaceIndex = line.IndexOf(' ');
                if (spaceIndex == -1)
                    throw new InvalidOperationException("Malformed query line.");

                string first = line.Substring(0, spaceIndex);
                string second = line.Substring(spaceIndex + 1);
                queries.Add(new StringPair(first, second));
            }

            return queries;
        }
    }
}
