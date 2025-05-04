using Common;
using Common.DataSources;

namespace Lab2.Models
{
    public class InputData : IParsableInputData<InputData>
    {
        public List<string> Words { get; }

        private InputData(List<string> words)
        {
            Words = words;
        }

        public static InputData Parse(IInputDataSource source)
        {
            List<string> words = new List<string>();

            string? line;
            while ((line = source.ReadLine()) != null)
            {
                words.Add(line.Trim());
            }

            return new InputData(words);
        }
    }
}
