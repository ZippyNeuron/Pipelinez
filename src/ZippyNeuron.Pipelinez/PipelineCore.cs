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
        
        using var stateBag = serviceProvider
            .GetRequiredService<IPipelineStateBag>();

        var runner = new PipelineReactionsRunner<TInput, TOutput>(
            input,
            output,
            serviceProvider,
            stateBag);

        await runner.RunParallel(Preactions);

        await runner.RunSerial(Reactions);
    
        return output;
    }
}