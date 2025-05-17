using Microsoft.Extensions.DependencyInjection;

namespace ZippyNeuron.Pipelinez;

public abstract class PipelineCore<TInput, TOutput> 
    : IPipelineCore<TInput, TOutput> where TOutput : new()
{
    public IEnumerable<PipelineReactionDefinition<TInput, TOutput>> Preactions { get; init; }
    public IEnumerable<PipelineReactionDefinition<TInput, TOutput>> Reactions { get; init; }

    public PipelineCore()
    {
        Preactions = [];
        Reactions = [];
    }

    public async Task<TOutput> Action(TInput input, IServiceProvider serviceProvider)
    {
        var output = new TOutput();
        
        var pipelineReactionsRunner = serviceProvider
            .GetRequiredService<IPipelineReactionsRunner>();

        await pipelineReactionsRunner
            .RunParallel(input, output, Preactions);
        
        await pipelineReactionsRunner
            .RunSerial(input, output, Reactions);
    
        return output;
    }
}