using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// A utility class to measure elapsed time for named code sections.
    /// Supports starting and stopping multiple named timers,
    /// and outputs a summary with durations and percentage of total time.
    /// </summary>
    public class SectionTimer
    {
        private readonly Dictionary<string, Stopwatch> _activeSections = new Dictionary<string, Stopwatch>();
        private readonly Dictionary<string, TimeSpan> _completedSections = new Dictionary<string, TimeSpan>();
        private readonly HashSet<string> _sectionsToExcludeFromSum = new HashSet<string>();

        public string? TimerTitle { get; set; }

        public SectionTimer(string? timerTitle = "Section Timing Results:")
        {
            TimerTitle = timerTitle;
        }

        /// <summary>
        /// Starts timing a section with the given name.
        /// </summary>
        /// <param name="name">The name of the section to start timing.</param>
        /// <param name="excludeFromTotalSum">Parameter to control wether or not this section is included in the total sum when calculating percentages of total sum.
        /// Default is false but can be set to true if you for example want to have a section that represents full task that was measured.</param>
        /// <exception cref="InvalidOperationException">Thrown if the section is already running.</exception>
        public void StartSection(string name, bool excludeFromTotalSum = false)
        {
            if (_activeSections.ContainsKey(name))
            {
                throw new InvalidOperationException($"Section '{name}' is already running.");
            }

            if (excludeFromTotalSum) _sectionsToExcludeFromSum.Add(name);
            Stopwatch stopwatch = new Stopwatch();
            _activeSections[name] = stopwatch;
            stopwatch.Start();
        }

        /// <summary>
        /// Stops timing a section with the given name.
        /// </summary>
        /// <param name="name">The name of the section to stop timing.</param>
        /// <exception cref="InvalidOperationException">Thrown if the section is not currently running.</exception>
        public void StopSection(string name)
        {
            if (!_activeSections.TryGetValue(name, out Stopwatch? stopwatch))
            {
                throw new InvalidOperationException($"Section '{name}' is not running.");
            }

            stopwatch.Stop();
            _activeSections.Remove(name);

            if (_completedSections.ContainsKey(name))
            {
                _completedSections[name] += stopwatch.Elapsed;
            }
            else
            {
                _completedSections[name] = stopwatch.Elapsed;
            }
        }

        /// <summary>
        /// Returns a formatted string containing the elapsed time for each section,
        /// with durations in milliseconds and their percentage of the total time.
        /// </summary>
        /// <returns>A formatted timing summary.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(TimerTitle);
            builder.AppendLine("------------------------");

            TimeSpan total = TimeSpan.Zero;
            foreach (KeyValuePair<string, TimeSpan> entry in _completedSections)
            {
                if (!_sectionsToExcludeFromSum.Contains(entry.Key))
                    total += entry.Value;
            }

            int namePadding = _completedSections.Keys.Max(k => k.Length) + 2;

            foreach (KeyValuePair<string, TimeSpan> entry in _completedSections)
            {
                string paddedName = $"{entry.Key}:".PadRight(namePadding);
                string formattedTime = FormatTime(entry.Value);
                double percent = total.TotalMilliseconds > 0
                    ? (entry.Value.TotalMilliseconds / total.TotalMilliseconds) * 100.0
                    : 0.0;
                builder.AppendLine($"{paddedName}{formattedTime} ms ({percent.ToString("F1", CultureInfo.InvariantCulture)}% / total)");
            }

            return builder.ToString();
        }

        private static string FormatTime(TimeSpan time)
        {
            double ms = time.TotalMilliseconds;
            if (ms < 10.0)
            {
                return ms.ToString("F2", CultureInfo.InvariantCulture);
            }
            else if (ms < 100.0)
            {
                return ms.ToString("F1", CultureInfo.InvariantCulture);
            }
            else
            {
                return ((int)ms).ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
