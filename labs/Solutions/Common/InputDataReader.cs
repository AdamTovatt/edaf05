using Common.DataSources;

namespace Common
{
    public class InputDataReader<T> where T : IParsableInputData<T>
    {
        private readonly IInputDataSource _source;

        public InputDataReader(IInputDataSource source)
        {
            _source = source;
        }

        public T Read()
        {
            return T.Parse(_source);
        }
    }
}
