using System.Reflection;

namespace ZippyNeuron.Pipelinez;

internal class PipelineReactionInvoker<TInput, TOutput>
{
    private readonly MethodInfo Method =
        typeof(IPipelineReaction<TInput, TOutput>).GetMethod("React")!;

    public Task<bool>? Invoke(
        Type reactionType,
        TInput input, 
        TOutput output, 
        IServiceProvider serviceProvider, 
        IPipelineStateBag pipelineStateBag)
    {
        return (Task<bool>?)Method.Invoke(
            Activator.CreateInstance(reactionType),
            [input, output, serviceProvider, pipelineStateBag]);
    }
}