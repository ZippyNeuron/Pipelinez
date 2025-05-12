using System.Reflection;

namespace ZippyNeuron.Pipelinez;

internal class PipelineReactionInvoker<TInput, TOutput>
{
    private readonly MethodInfo Method =
        typeof(IPipelineReaction<TInput, TOutput>).GetMethod("React")!;

    public Task<bool>? Invoke(
        IPipelineReaction<TInput, TOutput> reaction,
        TInput input, 
        TOutput output, 
        IServiceProvider serviceProvider, 
        IPipelineStateBag pipelineStateBag)
    {
        return (Task<bool>?)Method.Invoke(
            reaction,
            [input, output, serviceProvider, pipelineStateBag]);
    }
}