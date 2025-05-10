namespace ZippyNeuron.Pipelinez.Tests.Setup;

public class PipelineTestException : PipelineCore<PipelineTestInput, PipelineTestOutput>
{
    public PipelineTestException()
    {        
        Preactions = new PipelineReactionsBuilder<PipelineTestInput, PipelineTestOutput>()
            .Build();

        Reactions = new PipelineReactionsBuilder<PipelineTestInput, PipelineTestOutput>()
            .Add<PipelineTestExceptionReaction>()
            .Build();
    }
}