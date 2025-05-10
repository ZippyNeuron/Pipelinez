using ZippyNeuron.Pipelinez;
using WorkbenchWebApi.Pipelines.Console.Financial;

namespace WorkbenchWebApi.Pipelines.Console.Quotations.CarpetFitting.Reactions;

public sealed class OtherCostsReaction : IPipelineReaction<CarpetFittingInput, CarpetFittingOutput>
{
    public Task<bool> React(
        CarpetFittingInput input,
        CarpetFittingOutput output,
        IServiceProvider? serviceProvider,
        IPipelineStateBag pipelineStateBag)
    {
        var otherCosts = (input.ThresholdCosts + input.CarpetGripperCosts + input.AdditionalCosts)
            .ToRounded(2);

        output.Other += otherCosts;
        output.Total += otherCosts;

        return Task.FromResult(true);
    }
}