namespace ZippyNeuron.Pipelinez;

public sealed class PipelineFactory(IServiceProvider _serviceProvider) 
    : IPipelineFactory
{
    public IPipeline<TInput, TOutput> Create<TInput, TOutput>() 
        where TOutput : new() =>
            new Pipeline<TInput, TOutput>(_serviceProvider);
}