namespace ZippyNeuron.Pipelinez;

public interface IPipelineCore<TInput, TOutput>
{
    IEnumerable<PipelineReactionDefinition<TInput, TOutput>> Preactions { get; init; }
    IEnumerable<PipelineReactionDefinition<TInput, TOutput>> Reactions { get; init; }
    Task<TOutput> Action(TInput input, IServiceProvider serviceProvider);
}