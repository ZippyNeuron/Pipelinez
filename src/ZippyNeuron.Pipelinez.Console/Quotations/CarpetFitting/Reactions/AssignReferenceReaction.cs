using ZippyNeuron.Pipelinez;

namespace WorkbenchWebApi.Pipelines.Console.Quotations.CarpetFitting.Reactions;

public sealed class AssignReferenceReaction : IPipelineReaction<CarpetFittingInput, CarpetFittingOutput>
{
    public Task<bool> React(
        CarpetFittingInput input,
        CarpetFittingOutput output,
        IServiceProvider? serviceProvider,
        IPipelineStateBag pipelineStateBag)
    {
        output.Reference = Guid.NewGuid().ToString();

        return Task.FromResult(true);
    }
}