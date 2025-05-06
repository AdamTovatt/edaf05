namespace Lab3.Models
{
    public readonly struct Connection
    {
        public int PersonA { get; }
        public int PersonB { get; }
        public int Weight { get; }

        public Connection(int personA, int personB, int weight)
        {
            PersonA = personA;
            PersonB = personB;
            Weight = weight;
        }
    }
}
