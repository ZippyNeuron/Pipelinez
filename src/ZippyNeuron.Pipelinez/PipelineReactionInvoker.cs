using System.Reflection;

namespace ZippyNeuron.Pipelinez;

internal sealed class PipelineReactionInvoker : IPipelineReactionInvoker
{
    public Task<bool>? Invoke<TInput, TOutput>(
        IPipelineReaction<TInput, TOutput> reaction,
        TInput input,
        TOutput output,
        IServiceProvider serviceProvider,
        IPipelineStateBag pipelineStateBag)
    {
        MethodInfo? method = typeof(IPipelineReaction<TInput, TOutput>)
            .GetMethod("React");

        return (Task<bool>?)method?
            .Invoke(reaction, [input, output, serviceProvider, pipelineStateBag]);
    }
}