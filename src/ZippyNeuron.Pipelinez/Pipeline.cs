namespace ZippyNeuron.Pipelinez;

internal sealed class Pipeline<TInput, TOutput>(IServiceProvider _serviceProvider) 
    : IPipeline<TInput, TOutput>
{
    public async Task<TOutput> Action<TPipelineCore>(TInput input)
        where TPipelineCore : IPipelineCore<TInput, TOutput>, new() =>
            await new TPipelineCore().Action(input, _serviceProvider);
}