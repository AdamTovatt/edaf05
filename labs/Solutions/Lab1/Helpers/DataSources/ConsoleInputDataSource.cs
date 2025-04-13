namespace Lab1.Helpers.DataSources
{
    public class ConsoleInputDataSource : IInputDataSource
    {
        public string ReadNextLine()
        {
            string? line = Console.ReadLine();

            if (line == null)
                throw new InvalidOperationException("Unexpected end of input.");

            return line;
        }
    }
}