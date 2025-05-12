using ZippyNeuron.Pipelinez.Console.Financial;

namespace ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting.Reactions;

public sealed class UnderlayCostsReaction : IPipelineReaction<CarpetFittingInput, CarpetFittingOutput>
{
    public Task<bool> React(
        CarpetFittingInput input,
        CarpetFittingOutput output,
        IServiceProvider? serviceProvider,
        IPipelineStateBag pipelineStateBag)
    {
        var underlayCosts = (input.TotalSquareMeters * input.UnderlayCostPerSquareMeter)
            .ToRounded(2);

        output.Materials += underlayCosts;
        output.Total += underlayCosts;

        return Task.FromResult(true);
    }
}