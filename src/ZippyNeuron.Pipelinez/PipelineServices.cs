using Microsoft.Extensions.DependencyInjection;

namespace ZippyNeuron.Pipelinez;

public static class PipelineServices
{
    [Obsolete("Use AddPipelinezServices instead.")]
    public static IServiceCollection AddPipelineServices(this IServiceCollection services) =>
        AddPipelinezServices(services);

    public static IServiceCollection AddPipelinezServices(this IServiceCollection services) => 
        services
            .AddSingleton<IPipelineFactory, PipelineFactory>()
            .AddTransient<IPipelineReactionsRunner, PipelineReactionsRunner>()
            .AddTransient<IPipelineReactionInvoker, PipelineReactionInvoker>()
            .AddTransient<IPipelineStateBag, PipelineStateBag>();
}