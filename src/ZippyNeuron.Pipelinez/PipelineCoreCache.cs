using System.Collections.Concurrent;

namespace ZippyNeuron.Pipelinez;

internal sealed class PipelineCoreCache<TInput, TOutput>
{
    private readonly ConcurrentDictionary<Type, IPipelineCore<TInput, TOutput>> _cachedCores = [];

    public IPipelineCore<TInput, TOutput> GetOrAdd<TPipelineCore>() 
        where TPipelineCore : IPipelineCore<TInput, TOutput>, new ()
    {
        if (_cachedCores.TryGetValue(typeof(TPipelineCore), out var core))
        {
            return core;
        }

        var pipelineCore = new TPipelineCore();

        _cachedCores.TryAdd(typeof(TPipelineCore), pipelineCore);
        
        return pipelineCore;
    }
}