namespace Common.DataSources
{
    public class FileInputDataSource : IInputDataSource, IDisposable
    {
        private readonly StreamReader _reader;

        public FileInputDataSource(string path)
        {
            _reader = new StreamReader(path);
        }

        public string? ReadLine()
        {
            return _reader.ReadLine();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
