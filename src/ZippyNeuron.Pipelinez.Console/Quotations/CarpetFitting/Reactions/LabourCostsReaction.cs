using ZippyNeuron.Pipelinez;
using WorkbenchWebApi.Pipelines.Console.Financial;

namespace WorkbenchWebApi.Pipelines.Console.Quotations.CarpetFitting.Reactions;

public sealed class LabourCostsReaction : IPipelineReaction<CarpetFittingInput, CarpetFittingOutput>
{
    public Task<bool> React(
        CarpetFittingInput input,
        CarpetFittingOutput output,
        IServiceProvider? serviceProvider,
        IPipelineStateBag pipelineStateBag)
    {
        var labourCosts = (
            input.FixedLabourCost > 0 ?
            input.FixedLabourCost :
            input.TimeInHours * input.HourlyRate)
            .ToRounded(2);

        output.Time += labourCosts;
        output.Total += labourCosts;

        return Task.FromResult(true);
    }
}