namespace Lab1.Models
{
    public class Query
    {
        public string From { get; } // from word
        public string To { get; } // to word

        public Query(string from, string to)
        {
            From = from;
            To = to;
        }
    }
}
