namespace ZippyNeuron.Pipelinez;

public sealed class PipelineReactionDefinition<TInput, TOutput>(Type type)
{
    public readonly List<PipelineReactionDefinition<TInput, TOutput>> Reactions = [];
    public IPipelineReaction<TInput, TOutput> Reaction { get; } 
        = (IPipelineReaction<TInput, TOutput>)Activator.CreateInstance(type)!;
    public bool HasReactions => Reactions.Count > 0;
}