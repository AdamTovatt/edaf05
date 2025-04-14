namespace Lab1.Models
{
    public class InputData
    {
        public int WordCount { get; } // the amount of words before the queries start
        public int QueryCount { get; } // the amount of queries after the words
        public List<string> Words { get; }
        public List<Query> Queries { get; }

        public InputData(int wordCount, int queryCount, List<string> words, List<Query> queries)
        {
            WordCount = wordCount;
            QueryCount = queryCount;
            Words = words;
            Queries = queries;
        }
    }
}
