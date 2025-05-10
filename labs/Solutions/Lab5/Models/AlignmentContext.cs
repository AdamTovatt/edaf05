namespace Lab5.Models
{
    public class AlignmentContext
    {
        private readonly Dictionary<char, int> _charToIndex;
        private readonly int[,] _costMatrix;

        public int GapPenalty { get; }

        public AlignmentContext(string characters, int[,] costMatrix, int gapPenalty = -4)
        {
            _costMatrix = costMatrix;
            GapPenalty = gapPenalty;

            _charToIndex = new Dictionary<char, int>(characters.Length);
            for (int i = 0; i < characters.Length; i++)
                _charToIndex[characters[i]] = i;
        }

        public int GetCost(char a, char b)
        {
            return _costMatrix[_charToIndex[a], _charToIndex[b]];
        }
    }
}
