namespace ZippyNeuron.Pipelinez;

public interface IPipelineCore<TInput, TOutput>
{
    IEnumerable<PipelineReactionDefinition> Preactions { get; init; }
    IEnumerable<PipelineReactionDefinition> Reactions { get; init; }
    Task<TOutput> Action(TInput input, IServiceProvider serviceProvider);
}