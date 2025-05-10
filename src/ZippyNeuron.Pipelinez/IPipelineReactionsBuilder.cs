namespace ZippyNeuron.Pipelinez;

public interface IPipelineReactionsBuilder<TInput, TOutput>
{
    IPipelineReactionsBuilder<TInput, TOutput> Add<TPipelineReaction>(
        Action<IPipelineReactionsBuilder<TInput, TOutput>>? builder = null)
            where TPipelineReaction : IPipelineReaction<TInput, TOutput>;

    IEnumerable<PipelineReactionDefinition> Build();
}