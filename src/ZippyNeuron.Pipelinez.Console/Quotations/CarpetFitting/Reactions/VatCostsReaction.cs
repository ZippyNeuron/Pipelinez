using ZippyNeuron.Pipelinez.Console.Financial;

namespace ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting.Reactions;

public sealed class VatCostsReaction : IPipelineReaction<CarpetFittingInput, CarpetFittingOutput>
{
    public Task<bool> React(
        CarpetFittingInput input,
        CarpetFittingOutput output,
        IServiceProvider? serviceProvide,
        IPipelineStateBag pipelineStateBag)
    {
        output.VatRate = pipelineStateBag.Get<double>("VatRate");

        var vat = (output.Total * output.VatRate)
            .ToRounded(2);

        output.Vat += vat;
        output.Total += vat;

        return Task.FromResult(true);
    }
}