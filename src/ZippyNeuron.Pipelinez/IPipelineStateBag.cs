namespace ZippyNeuron.Pipelinez;

public interface IPipelineStateBag : IDisposable
{
    T? Get<T>(string key);
    void Set(string key, object obj);
}