using Microsoft.Extensions.DependencyInjection;

namespace ZippyNeuron.Pipelinez;

public static class PipelineServices
{
    public static IServiceCollection AddPipelineServices(this IServiceCollection services) => 
        services
            .AddSingleton<IPipelineFactory, PipelineFactory>()
            .AddTransient<IPipelineReactionsRunner, PipelineReactionsRunner>()
            .AddTransient<IPipelineReactionInvoker, PipelineReactionInvoker>()
            .AddTransient<IPipelineStateBag, PipelineStateBag>();
}