using Lab5.Models;

namespace Tests.Helpers
{
    public static class AlignmentValidator
    {
        public static bool IsValidAlignment(
            StringPair original,
            StringPair aligned,
            StringPair referenceAligned,
            AlignmentContext context,
            out string? error)
        {
            error = null;

            if (aligned.First.Length != aligned.Second.Length)
            {
                error = "Aligned strings have unequal length.";
                return false;
            }

            if (!ReconstructsOriginal(original.First, aligned.First) ||
                !ReconstructsOriginal(original.Second, aligned.Second))
            {
                error = "Aligned strings do not reconstruct original input.";
                return false;
            }

            int actualScore = GetAlignmentScore(aligned, context);
            int expectedScore = GetAlignmentScore(referenceAligned, context);

            if (actualScore < expectedScore)
            {
                error = $"Score too low. Got {actualScore}, expected at least {expectedScore}.";
                return false;
            }

            if (actualScore > expectedScore)
            {
                error = $"Score too high. Got {actualScore}, expected at most {expectedScore}.";
                return false;
            }

            return true;
        }

        private static bool ReconstructsOriginal(string original, string aligned)
        {
            int index = 0;

            foreach (char c in aligned)
            {
                if (c != '*')
                {
                    if (index >= original.Length || original[index] != c)
                        return false;
                    index++;
                }
            }

            return index == original.Length;
        }

        private static int GetAlignmentScore(StringPair alignment, AlignmentContext context)
        {
            int score = 0;
            for (int i = 0; i < alignment.First.Length; i++)
            {
                char a = alignment.First[i];
                char b = alignment.Second[i];

                if (a == '*' && b == '*')
                    score -= 8;
                else if (a == '*' || b == '*')
                    score -= 4;
                else
                    score += context.GetCost(a, b);
            }

            return score;
        }
    }
}
