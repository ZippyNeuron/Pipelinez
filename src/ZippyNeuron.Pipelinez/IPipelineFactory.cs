namespace ZippyNeuron.Pipelinez;

public interface IPipelineFactory
{
    IPipeline<TInput, TOutput> Create<TInput, TOutput>() 
        where TOutput : new();
}