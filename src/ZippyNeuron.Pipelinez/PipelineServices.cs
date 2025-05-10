using Microsoft.Extensions.DependencyInjection;

namespace ZippyNeuron.Pipelinez;

public static class PipelineServices
{
    public static IServiceCollection AddPipelineServices(this IServiceCollection services) => 
        services
            .AddTransient<IPipelineFactory, PipelineFactory>()
            .AddTransient<IPipelineStateBag, PipelineStateBag>();
}