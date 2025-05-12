using ZippyNeuron.Pipelinez.Console.Financial;

namespace ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting.Reactions;

public sealed class RoundTotalReaction : IPipelineReaction<CarpetFittingInput, CarpetFittingOutput>
{
    public Task<bool> React(
        CarpetFittingInput input,
        CarpetFittingOutput output,
        IServiceProvider? serviceProvider,
        IPipelineStateBag pipelineStateBag)
    {
        output.Total = output.Total.ToRounded(2);

        return Task.FromResult(true);
    }
}