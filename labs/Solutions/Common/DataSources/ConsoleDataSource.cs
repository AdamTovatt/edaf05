namespace Common.DataSources
{
    public class ConsoleInputDataSource : IInputDataSource
    {
        public string? ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
