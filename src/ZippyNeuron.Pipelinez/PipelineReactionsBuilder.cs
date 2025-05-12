namespace ZippyNeuron.Pipelinez;

public sealed class PipelineReactionsBuilder<TInput, TOutput> 
    : IPipelineReactionsBuilder<TInput, TOutput>
{
    private List<PipelineReactionsBuilder<TInput, TOutput>> ReactionTypes { get; init; } = [];
    private Type? ReactionType { get; init; }

    public IPipelineReactionsBuilder<TInput, TOutput> Add<TPipelineReaction>(
        Action<IPipelineReactionsBuilder<TInput, TOutput>>? builder = null)
            where TPipelineReaction : IPipelineReaction<TInput, TOutput>
    {
        var pipelineReactionsBuilder =
            new PipelineReactionsBuilder<TInput, TOutput>()
            {
                ReactionType = typeof(TPipelineReaction)
            };

        ReactionTypes.Add(pipelineReactionsBuilder);

        builder?.Invoke(pipelineReactionsBuilder);

        return this;
    }

    public IEnumerable<PipelineReactionDefinition<TInput, TOutput>> Build()
    {
        List<PipelineReactionDefinition<TInput, TOutput>> reactionEntities = [];

        RecurseToDefinitions(ReactionTypes, reactionEntities);

        return reactionEntities;
    }

    private static void RecurseToDefinitions(
        IEnumerable<PipelineReactionsBuilder<TInput, TOutput>> builderEntities, 
        List<PipelineReactionDefinition<TInput, TOutput>> reactionEntities)
    {
        foreach(var builderEntity in builderEntities)
        {
            if (builderEntity.ReactionType is null)
            {
                continue;
            }

            var reactionEntity =
                new PipelineReactionDefinition<TInput, TOutput>(builderEntity.ReactionType);

            reactionEntities.Add(reactionEntity);

            RecurseToDefinitions(builderEntity.ReactionTypes, reactionEntity.Reactions);
        }
    }
}