﻿using Lab4.Models;
using Common;

namespace Lab4.Algorithms
{
    public static class ClosestPairSolver
    {
        public static double FindClosestDistance(List<Point> points, SectionTimer? sectionTimer = null)
        {
            List<Point> sortedByX = new List<Point>(points);
            List<Point> sortedByY = new List<Point>(points);

            sectionTimer?.StartSection("sortPoints");
            sortedByX.Sort((a, b) => a.X.CompareTo(b.X)); // sort by x-coordinate
            sortedByY.Sort((a, b) => a.Y.CompareTo(b.Y)); // sort by y-coordinate
            sectionTimer?.StopSection("sortPoints");

            sectionTimer?.StartSection("computeClosestDistance");
            long squaredDistance = FindClosestRecursive(sortedByX, sortedByY);
            sectionTimer?.StopSection("computeClosestDistance");

            return Math.Sqrt(squaredDistance); // convert from squared distance to real distance
        }

        private static long FindClosestRecursive(List<Point> pointsSortedByX, List<Point> pointsSortedByY)
        {
            int count = pointsSortedByX.Count;

            if (count <= 3)
                return BruteForceClosestDistance(pointsSortedByX); // base case: use brute force

            int midIndex = count / 2;
            Point midpoint = pointsSortedByX[midIndex];

            List<Point> leftByX = pointsSortedByX.GetRange(0, midIndex);
            List<Point> rightByX = pointsSortedByX.GetRange(midIndex, count - midIndex);

            List<Point> leftByY = new List<Point>();
            List<Point> rightByY = new List<Point>();

            foreach (Point point in pointsSortedByY)
            {
                if (point.X <= midpoint.X)
                    leftByY.Add(point); // maintain y-sorted order for left half
                else
                    rightByY.Add(point); // maintain y-sorted order for right half
            }

            long leftMinDistance = FindClosestRecursive(leftByX, leftByY); // recurse on left half
            long rightMinDistance = FindClosestRecursive(rightByX, rightByY); // recurse on right half

            long currentMinDistance = Math.Min(leftMinDistance, rightMinDistance); // closest distance so far

            List<Point> strip = new List<Point>();

            foreach (Point point in pointsSortedByY)
            {
                long deltaX = (long)point.X - midpoint.X;
                if (deltaX * deltaX < currentMinDistance)
                    strip.Add(point); // only consider points within currentMin from mid line
            }

            return Math.Min(currentMinDistance, ClosestInStrip(strip, currentMinDistance));
        }

        private static long ClosestInStrip(List<Point> strip, long maxDistanceSquared)
        {
            long best = maxDistanceSquared;
            int count = strip.Count;

            for (int i = 0; i < count; i++)
            {
                // not using the hardcoded 7 limit count like the course suggests here because that's slower (see comment below)
                for (int j = i + 1; j < count && Square(strip[j].Y - strip[i].Y) < best; j++)
                {
                    long dx = strip[i].X - strip[j].X;
                    long dy = strip[i].Y - strip[j].Y;
                    long distSquared = dx * dx + dy * dy;

                    if (distSquared < best)
                        best = distSquared;
                }
            }

            // the above loop could also be solved as below, which is more accurate to how it's presented in the course
            // but that solution is in reality slower, both for our test cases and probably just in general too
            /*
            for (int i = 0; i < count; i++)
            {
                int limit = Math.Min(i + 8, count); // 8 instead of 7 since we do j < limit in the loop below

                for (int j = i + 1; j < limit; j++)
                {
                    long dx = strip[i].X - strip[j].X;
                    long dy = strip[i].Y - strip[j].Y;
                    long distSquared = dx * dx + dy * dy;

                    if (distSquared < best)
                        best = distSquared;
                }
            }
            */

            return best;
        }

        private static long BruteForceClosestDistance(List<Point> points)
        {
            long minSquared = long.MaxValue;

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    long dx = points[i].X - points[j].X;
                    long dy = points[i].Y - points[j].Y;
                    long distSquared = dx * dx + dy * dy;

                    if (distSquared < minSquared)
                        minSquared = distSquared;
                }
            }

            return minSquared;
        }

        private static long Square(long value)
        {
            return value * value;
        }
    }
}
