namespace ZippyNeuron.Pipelinez;

public interface IPipeline<TInput, TOutput>
{
    Task<TOutput> Action<TPipelineCore>(TInput input)
        where TPipelineCore : IPipelineCore<TInput, TOutput>, new();
}