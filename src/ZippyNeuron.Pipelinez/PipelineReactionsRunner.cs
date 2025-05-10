using ZippyNeuron.Pipelinez.Extensions;

namespace ZippyNeuron.Pipelinez;

internal sealed class PipelineReactionsRunner<TInput, TOutput>(
    TInput _input,
    TOutput _output,
    IServiceProvider _serviceProvider,
    IPipelineStateBag _pipelineStateBag)
{
    private readonly PipelineReactionInvoker<TInput, TOutput> Invoker = new();

    public async Task RunSerial(IEnumerable<PipelineReactionDefinition> reactionDefinitions)
    {
        foreach (var reactionDefinition in reactionDefinitions)
        {
            var task = Invoker
                .Invoke(
                    reactionDefinition.Type,
                    _input, 
                    _output, 
                    _serviceProvider, 
                    _pipelineStateBag);

            if (task != null && await task && reactionDefinition.Reactions.Count > 0)
            {
                await RunSerial(reactionDefinition.Reactions);
            }
        }
    }

    public async Task RunParallel(IEnumerable<PipelineReactionDefinition> reactionDefinitions)
    {
        List<Task<bool>> tasks = [];

        var reactionsList = 
            reactionDefinitions.ToReactionList();

        foreach (var reactionDefinition in reactionsList)
        {
            var task = Invoker
                .Invoke(
                    reactionDefinition.Type, 
                    _input, 
                    _output, 
                    _serviceProvider, 
                    _pipelineStateBag);

            if (task == null)
                continue;

            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
    }
}