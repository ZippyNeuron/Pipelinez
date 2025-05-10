namespace ZippyNeuron.Pipelinez;

public sealed class PipelineReactionsBuilder<TInput, TOutput> 
    : IPipelineReactionsBuilder<TInput, TOutput>
{
    private List<PipelineReactionsBuilder<TInput, TOutput>> Reactions { get; init; } = [];
    private Type Type { get; init; } = typeof(object);

    public IPipelineReactionsBuilder<TInput, TOutput> Add<TPipelineReaction>(
        Action<IPipelineReactionsBuilder<TInput, TOutput>>? builder = null)
            where TPipelineReaction : IPipelineReaction<TInput, TOutput>
    {
        var pipelineReactionsBuilder =
            new PipelineReactionsBuilder<TInput, TOutput>()
            {
                Type = typeof(TPipelineReaction)
            };

        Reactions.Add(pipelineReactionsBuilder);

        builder?.Invoke(pipelineReactionsBuilder);

        return this;
    }

    public IEnumerable<PipelineReactionDefinition> Build()
    {
        List<PipelineReactionDefinition> reactionEntities = [];

        RecurseToDefinitions(Reactions, reactionEntities);

        return reactionEntities;
    }

    private static void RecurseToDefinitions(
        IEnumerable<PipelineReactionsBuilder<TInput, TOutput>> builderEntities, 
        List<PipelineReactionDefinition> reactionEntities)
    {
        foreach(var builderEntity in builderEntities)
        {
            var reactionEntity =
                new PipelineReactionDefinition(builderEntity.Type);

            reactionEntities.Add(reactionEntity);

            RecurseToDefinitions(builderEntity.Reactions, reactionEntity.Reactions);
        }
    }
}