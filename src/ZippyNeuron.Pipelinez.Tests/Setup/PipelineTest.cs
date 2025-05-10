namespace ZippyNeuron.Pipelinez.Tests.Setup;

public class PipelineTest : PipelineCore<PipelineTestInput, PipelineTestOutput>
{
    public PipelineTest()
    {        
        Preactions = new PipelineReactionsBuilder<PipelineTestInput, PipelineTestOutput>()
            .Add<PipelineTestReaction>(r => 
                r.Add<PipelineTestReaction>())
            .Build();

        Reactions = new PipelineReactionsBuilder<PipelineTestInput, PipelineTestOutput>()
            .Add<PipelineTestReaction>(r =>
                r.Add<PipelineTestReaction>())
            .Build();
    }
}