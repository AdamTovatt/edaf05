namespace Lab1.Helpers.DataSources
{
    /// <summary>
    /// Represents a datasource that reads from the console (std in).
    /// </summary>
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