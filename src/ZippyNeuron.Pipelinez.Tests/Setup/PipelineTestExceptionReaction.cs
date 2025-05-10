namespace ZippyNeuron.Pipelinez.Tests.Setup;

public class PipelineTestExceptionReaction : IPipelineReaction<PipelineTestInput, PipelineTestOutput>
{
    public Task<bool> React(PipelineTestInput input, PipelineTestOutput output, IServiceProvider serviceProvider, IPipelineStateBag pipelineStateBag)
    {
        throw new PipelineReactionException("Outer Exception", new Exception("Inner Exception"));
    }
}