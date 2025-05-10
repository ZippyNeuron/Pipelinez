namespace ZippyNeuron.Pipelinez.Tests.Setup;

public class PipelineTestReaction : IPipelineReaction<PipelineTestInput, PipelineTestOutput>
{
    public Task<bool> React(PipelineTestInput input, PipelineTestOutput output, IServiceProvider serviceProvider, IPipelineStateBag pipelineStateBag)
    {
        output.Id = input.Id;

        return Task.FromResult(true);
    }
}