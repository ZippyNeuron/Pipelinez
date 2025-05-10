namespace ZippyNeuron.Pipelinez;

public interface IPipelineReaction<TInput, TOutput>
{
    Task<bool> React(
        TInput input,
        TOutput output,
        IServiceProvider serviceProvider,
        IPipelineStateBag pipelineStateBag);
}