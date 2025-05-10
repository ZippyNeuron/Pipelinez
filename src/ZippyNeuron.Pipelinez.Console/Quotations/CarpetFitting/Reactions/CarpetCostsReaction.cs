using ZippyNeuron.Pipelinez;
using WorkbenchWebApi.Pipelines.Console.Financial;

namespace WorkbenchWebApi.Pipelines.Console.Quotations.CarpetFitting.Reactions;

public sealed class CarpetCostsReaction : IPipelineReaction<CarpetFittingInput, CarpetFittingOutput>
{
    public Task<bool> React(
        CarpetFittingInput input,
        CarpetFittingOutput output,
        IServiceProvider? serviceProvider,
        IPipelineStateBag pipelineStateBag)
    {
        var carpetCosts = (input.TotalSquareMeters * input.CarpetCostPerSquareMeter)
            .ToRounded(2);

        output.Materials += carpetCosts;
        output.Total += carpetCosts;

        return Task.FromResult(true);
    }
}