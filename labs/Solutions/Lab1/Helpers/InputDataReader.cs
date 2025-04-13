using Lab1.Helpers.DataSources;
using Lab1.Models;

namespace Lab1.Helpers
{
    public class InputDataReader
    {
        private readonly IInputDataSource _dataSource;

        public InputDataReader(IInputDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public InputData ReadInput()
        {
            string[] firstLine = _dataSource.ReadNextLine().Split();
            int wordCount = int.Parse(firstLine[0]);
            int queryCount = int.Parse(firstLine[1]);

            List<string> words = new List<string>(wordCount);
            for (int i = 0; i < wordCount; i++)
                words.Add(_dataSource.ReadNextLine());

            List<Query> queries = new List<Query>(queryCount);
            for (int i = 0; i < queryCount; i++)
            {
                string[] parts = _dataSource.ReadNextLine().Split();
                queries.Add(new Query(parts[0], parts[1]));
            }

            return new InputData(wordCount, queryCount, words, queries);
        }
    }
}
