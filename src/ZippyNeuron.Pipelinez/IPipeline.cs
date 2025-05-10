namespace ZippyNeuron.Pipelinez;

public interface IPipeline<TInput, TOutput>
{
    Task<TOutput> Action<TPipelineType>(TInput input)
        where TPipelineType : IPipelineCore<TInput, TOutput>, new();
}