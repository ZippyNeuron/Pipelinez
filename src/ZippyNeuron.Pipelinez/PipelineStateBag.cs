using System.Collections.Concurrent;

namespace ZippyNeuron.Pipelinez;

public sealed class PipelineStateBag : IPipelineStateBag
{
    private readonly ConcurrentDictionary<string, object> 
        StateBag = new();

    public T? Get<T>(string key)
    {
        try
        {
            if (StateBag.TryGetValue(key, out var value))
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }
        catch (Exception) { }
            
        return default;
    }

    public void Set(string key, object obj) =>
        StateBag[key] = obj;

    public void Dispose() =>
        StateBag.Clear();
}