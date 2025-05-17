namespace ZippyNeuron.Pipelinez;

internal interface IPipelineReactionsRunner
{
    Task RunParallel<TInput, TOutput>(
        TInput input,
        TOutput output,
        IEnumerable<PipelineReactionDefinition<TInput, TOutput>> reactionDefinitions);

    Task RunSerial<TInput, TOutput>(
        TInput input,
        TOutput output,
        IEnumerable<PipelineReactionDefinition<TInput, TOutput>> reactionDefinitions);
}