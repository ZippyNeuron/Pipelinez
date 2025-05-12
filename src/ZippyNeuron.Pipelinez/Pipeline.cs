namespace ZippyNeuron.Pipelinez;

internal sealed class Pipeline<TInput, TOutput>(IServiceProvider _serviceProvider) 
    : IPipeline<TInput, TOutput>
{
    private readonly PipelineCoreCache<TInput, TOutput> _coreCache = new();

    public async Task<TOutput> Action<TPipelineCore>(TInput input)
            where TPipelineCore : IPipelineCore<TInput, TOutput>, new()
    {
        return await _coreCache.GetOrAdd<TPipelineCore>()
            .Action(input, _serviceProvider);
    }
}