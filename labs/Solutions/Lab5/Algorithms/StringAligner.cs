using Lab5.Models;
using System.Text;

namespace Lab5.Algorithms
{
    public static class StringAligner
    {
        public static StringPair Align(StringPair query, AlignmentContext context)
        {
            Direction[,] backtrackTable = BuildBacktrackTable(query, context);
            return ReconstructAlignment(query, backtrackTable);
        }

        private static Direction[,] BuildBacktrackTable(StringPair query, AlignmentContext context)
        {
            string first = query.First;
            string second = query.Second;
            int rowCount = first.Length + 1;
            int columnCount = second.Length + 1;

            int[,] tempScore = new int[rowCount, columnCount]; // Stores the max gain up to each cell
            Direction[,] backtrack = new Direction[rowCount, columnCount]; // Stores direction of optimal path

            for (int row = 1; row < rowCount; row++)
            {
                tempScore[row, 0] = tempScore[row - 1, 0] + context.GapPenalty; // Only gaps in second string
                backtrack[row, 0] = Direction.Up; // Move came from above
            }

            for (int col = 1; col < columnCount; col++)
            {
                tempScore[0, col] = tempScore[0, col - 1] + context.GapPenalty; // Only gaps in first string
                backtrack[0, col] = Direction.Left; // Move came from left
            }

            for (int row = 1; row < rowCount; row++)
            {
                for (int col = 1; col < columnCount; col++)
                {
                    int match = tempScore[row - 1, col - 1] + context.GetCost(first[row - 1], second[col - 1]); // Match/mismatch
                    int gapFirst = tempScore[row, col - 1] + context.GapPenalty; // Gap in first string
                    int gapSecond = tempScore[row - 1, col] + context.GapPenalty; // Gap in second string

                    if (match >= gapFirst && match >= gapSecond)
                    {
                        tempScore[row, col] = match; // Best: match or mismatch
                        backtrack[row, col] = Direction.Diagonal; // Came from top-left
                    }
                    else if (gapSecond >= gapFirst)
                    {
                        tempScore[row, col] = gapSecond; // Best: gap in second
                        backtrack[row, col] = Direction.Up; // Came from above
                    }
                    else
                    {
                        tempScore[row, col] = gapFirst; // Best: gap in first
                        backtrack[row, col] = Direction.Left; // Came from left
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
                Direction dir = backtrack[row, col]; // Current direction from DP table

                if (dir == Direction.Diagonal)
                {
                    alignedFirst.Insert(0, query.First[row - 1]); // Add matching/mismatching char
                    alignedSecond.Insert(0, query.Second[col - 1]);
                    row--;
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
