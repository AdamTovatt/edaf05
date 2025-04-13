namespace Lab1.Models
{
    public class InputData
    {
        public int WordCount { get; }
        public int QueryCount { get; }
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
