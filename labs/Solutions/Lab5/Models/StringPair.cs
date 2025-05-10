namespace Lab5.Models
{
    public readonly struct StringPair
    {
        public string First { get; }
        public string Second { get; }

        public StringPair(string first, string second)
        {
            First = first;
            Second = second;
        }

        public override string ToString()
        {
            return $"{First} {Second}";
        }
    }
}
