using Common.DataSources;

public class StringInputDataSource : IInputDataSource
{
    private readonly StringReader _reader;

    public StringInputDataSource(string input)
    {
        _reader = new StringReader(input);
    }

    public string? ReadLine()
    {
        return _reader.ReadLine();
    }
}
