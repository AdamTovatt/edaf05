using Lab5.Models;
using System.Text;

namespace Lab5.Algorithms
{
    public static class StringAligner
    {
        public static StringPair Align(StringPair query, AlignmentContext context)
        {
            // If we think of the backtrack table as a 2d table that is [row, col]
            // Contains best action to take at each character index where "row" is index in first word and "col" is index in second word
            Direction[,] backtrackTable = BuildBacktrackTable(query, context);
            return ReconstructAlignment(query, backtrackTable);
        }

        // Builds a backtrack table. That is basically a 2d table with width and height that are like the length of the words we
        // want to align but + 1 to allow for a worst case scenario for both words
        // it's based on the score that can be achieved at any combination of the characters from the words
        // to create a table that stores the best way to get the best score
        private static Direction[,] BuildBacktrackTable(StringPair query, AlignmentContext context)
        {
            string first = query.First;
            string second = query.Second;
            int rowCount = first.Length + 1; // + 1 to be able to insert "worst case" in the score table
            int columnCount = second.Length + 1;

            int[,] tempScore = new int[rowCount, columnCount]; // Stores the max gain up to each cell
            Direction[,] backtrack = new Direction[rowCount, columnCount]; // Stores direction of optimal path

            for (int row = 1; row < rowCount; row++) // Initialize all rows with "worst case"
            {
                tempScore[row, 0] = tempScore[row - 1, 0] + context.GapPenalty; // Only gaps in second string
                backtrack[row, 0] = Direction.Up; // Move came from above
            }

            for (int col = 1; col < columnCount; col++) // Initialize all columns with "worst case"
            {
                tempScore[0, col] = tempScore[0, col - 1] + context.GapPenalty; // Only gaps in first string
                backtrack[0, col] = Direction.Left; // Move came from left
            }

            for (int row = 1; row < rowCount; row++) // go through the table but skip "worst case"
            {
                for (int col = 1; col < columnCount; col++)
                {
                    // we use row - 1 and col - 1 since we've added an extra row and column for the worst case so the index is offset by one and that takes it back

                    int match = tempScore[row - 1, col - 1] + context.GetCost(first[row - 1], second[col - 1]); // Cost if we matched the characters
                    int gapFirst = tempScore[row, col - 1] + context.GapPenalty; // Cost if we inserted a gap in the first string
                    int gapSecond = tempScore[row - 1, col] + context.GapPenalty; // Cost if we inserted a gap in the second string

                    if (match >= gapFirst && match >= gapSecond) // if the best option was to match the characters instead of inserting a gap
                    {
                        tempScore[row, col] = match; // we use the match score

                        // record that we "came from the top left" meaning we increased the row and column indexes equally since we
                        // took a character from both strings
                        backtrack[row, col] = Direction.Diagonal;
                    }
                    else if (gapSecond >= gapFirst) // the best option was to insert a gap in the second string
                    {
                        tempScore[row, col] = gapSecond; // use that score then

                        // "we came from above" by aligning first[row - 1] (aka the row above)
                        // with a gap (i.e., we skipped a character in the second string)
                        backtrack[row, col] = Direction.Up;
                    }
                    else // only option left now is that putting a gap in the first string was the best
                    {
                        tempScore[row, col] = gapFirst; // use that score then

                        // "we came from the left" by aligning second[col - 1] (aka the column to the left)
                        // with a gap (i.e., we skipped a character in the first string)
                        backtrack[row, col] = Direction.Left;
                    }
                }
            }

            return backtrack;
        }

        private static StringPair ReconstructAlignment(StringPair query, Direction[,] backtrack)
        {
            StringBuilder alignedFirst = new StringBuilder();
            StringBuilder alignedSecond = new StringBuilder();

            int row = query.First.Length;
            int col = query.Second.Length;

            while (row > 0 || col > 0)
            {
                Direction dir = backtrack[row, col]; // Current direction to move backwards in

                if (dir == Direction.Diagonal) // All of these cases just do the same thing we did when building the backtrack table but in reverse
                {
                    alignedFirst.Insert(0, query.First[row - 1]); // When creating the diagonal move we took one character from each string
                    alignedSecond.Insert(0, query.Second[col - 1]); // so we do that here too
                    row--; // Move symmetrically backwards
                    col--;
                }
                else if (dir == Direction.Up)
                {
                    alignedFirst.Insert(0, query.First[row - 1]); // Add char and gap
                    alignedSecond.Insert(0, '*');
                    row--;
                }
                else // Direction.Left
                {
                    alignedFirst.Insert(0, '*'); // Add gap and char
                    alignedSecond.Insert(0, query.Second[col - 1]);
                    col--;
                }
            }

            return new StringPair(alignedFirst.ToString(), alignedSecond.ToString());
        }
    }
}
