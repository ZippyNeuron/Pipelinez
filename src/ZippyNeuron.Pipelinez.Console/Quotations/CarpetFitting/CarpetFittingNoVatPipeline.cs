using ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting.Reactions;

namespace ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting;

public sealed class CarpetFittingNoVatPipeline : PipelineCore<CarpetFittingInput, CarpetFittingOutput>
{
    public CarpetFittingNoVatPipeline()
    {
        Preactions = new PipelineReactionsBuilder<CarpetFittingInput, CarpetFittingOutput>()
            .Build();

        Reactions = new PipelineReactionsBuilder<CarpetFittingInput, CarpetFittingOutput>()
            .Add<AssignReferenceReaction>()
            .Add<LabourCostsReaction>(r => r
                .Add<CarpetCostsReaction>(r => r
                    .Add<UnderlayCostsReaction>()))
            .Add<OtherCostsReaction>()
            .Add<RoundTotalReaction>()
            .Build();
    }
}