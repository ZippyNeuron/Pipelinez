﻿using ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting.Reactions;

namespace ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting;

public sealed class CarpetFittingPipeline : PipelineCore<CarpetFittingInput, CarpetFittingOutput>
{
    public CarpetFittingPipeline()
    {
        Preactions = new PipelineReactionsBuilder<CarpetFittingInput, CarpetFittingOutput>()
            .Add<VatLookupReaction>()
            .Build();

        Reactions = new PipelineReactionsBuilder<CarpetFittingInput, CarpetFittingOutput>()
            .Add<AssignReferenceReaction>()
            .Add<LabourCostsReaction>(r => r
                .Add<CarpetCostsReaction>(r => r
                    .Add<UnderlayCostsReaction>()))
            .Add<OtherCostsReaction>()
            .Add<VatCostsReaction>()
            .Add<RoundTotalReaction>()
            .Build();
    }
}