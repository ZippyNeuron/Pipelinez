using ZippyNeuron.Pipelinez.Extensions;

namespace ZippyNeuron.Pipelinez;

internal sealed class PipelineReactionsRunner(
    IServiceProvider _serviceProvider,
    IPipelineReactionInvoker _pipelineReactionInvoker,
    IPipelineStateBag _pipelineStateBag) : IPipelineReactionsRunner
{
    public async Task RunParallel<TInput, TOutput>(
        TInput input,
        TOutput output,
        IEnumerable<PipelineReactionDefinition<TInput, TOutput>> reactionDefinitions)
    {
        List<Task<bool>> tasks = [];

        var reactionsList =
            reactionDefinitions.ToReactionList();

        foreach (var reactionDefinition in reactionsList)
        {
            var task = _pipelineReactionInvoker
                .Invoke(
                    reactionDefinition.Reaction,
                    input,
                    output,
                    _serviceProvider,
                    _pipelineStateBag);

            if (task == null)
                continue;

            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
    }

    public async Task RunSerial<TInput, TOutput>(
        TInput input,
        TOutput output,
        IEnumerable<PipelineReactionDefinition<TInput, TOutput>> reactionDefinitions)
    {
        foreach (var reactionDefinition in reactionDefinitions)
        {
            var task = _pipelineReactionInvoker
                .Invoke(
                    reactionDefinition.Reaction,
                    input,
                    output,
                    _serviceProvider,
                    _pipelineStateBag);

            if (task == null)
                continue;

            if (await task && reactionDefinition.HasReactions)
            {
                await RunSerial(input, output, reactionDefinition.Reactions);
            }
        }
    }
}