using Lab1.Helpers.DataSources;
using Lab1.Models;

namespace Lab1.Helpers
{
    public class InputDataReader
    {
        // the source of the data, is an interface so that we can mock it or at least decide better where the data actually comes from
        // this is so that we can write unit tests easier and still have the real version of the program read input through the std in
        private readonly IInputDataSource _dataSource;

        public InputDataReader(IInputDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public InputData ReadInput()
        {
            string[] firstLine = _dataSource.ReadNextLine().Split(); // read the first line to know what to read after that
            int wordCount = int.Parse(firstLine[0]);
            int queryCount = int.Parse(firstLine[1]);

            List<string> words = new List<string>(wordCount);

            for (int i = 0; i < wordCount; i++) // read all the words
                words.Add(_dataSource.ReadNextLine());

            List<Query> queries = new List<Query>(queryCount);

            for (int i = 0; i < queryCount; i++) // read all the queries
            {
                string[] parts = _dataSource.ReadNextLine().Split(); // queries are formatted as "from to", .Split() splits on space by default
                queries.Add(new Query(parts[0], parts[1]));
            }

            return new InputData(wordCount, queryCount, words, queries);
        }
    }
}
