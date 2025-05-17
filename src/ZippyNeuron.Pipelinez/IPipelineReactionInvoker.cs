namespace ZippyNeuron.Pipelinez;

internal interface IPipelineReactionInvoker
{
    Task<bool>? Invoke<TInput, TOutput>(
        IPipelineReaction<TInput, TOutput> reaction, 
        TInput input, 
        TOutput output, 
        IServiceProvider serviceProvider, 
        IPipelineStateBag pipelineStateBag);
}