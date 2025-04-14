namespace Lab1.Helpers.DataSources
{
    /// <summary>
    /// Represents a data source that reads from a file stream.
    /// </summary>
    public class FileInputDataSource : IInputDataSource, IDisposable
    {
        private readonly StreamReader _reader;

        public FileInputDataSource(string path)
        {
            _reader = new StreamReader(path);
        }

        public string ReadNextLine()
        {
            string? line = _reader.ReadLine();

            if (line == null)
                throw new InvalidOperationException("Unexpected end of input.");

            return line;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}