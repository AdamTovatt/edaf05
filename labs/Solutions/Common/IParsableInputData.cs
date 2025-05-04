using Common.DataSources;

namespace Common
{
    public interface IParsableInputData<T>
    {
        static abstract T Parse(IInputDataSource source);
    }
}
