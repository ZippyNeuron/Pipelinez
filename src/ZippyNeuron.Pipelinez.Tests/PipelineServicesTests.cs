using Microsoft.Extensions.DependencyInjection;

namespace ZippyNeuron.Pipelinez.Tests;

[TestFixture]
public class PipelineServicesTests
{
    private IServiceCollection _serviceCollection;

    [SetUp]
    public void Setup()
    {
        _serviceCollection = new ServiceCollection();
    }

    [Test]
    public void AddPipelineServices_ShouldRegisterServices()
    {
        var serviceProvider = _serviceCollection
            .AddPipelineServices()
            .BuildServiceProvider();

        AssertPipelinezServicesAreRegistered(serviceProvider);
    }

    [Test]
    public void AddPipelinezServices_ShouldRegisterServices()
    {
        var serviceProvider = _serviceCollection
            .AddPipelinezServices()
            .BuildServiceProvider();

        AssertPipelinezServicesAreRegistered(serviceProvider);
    }

    private static void AssertPipelinezServicesAreRegistered(IServiceProvider serviceProvider)
    {
        var pipelineFactory = serviceProvider.GetService<IPipelineFactory>();
        var pipelineReactionsRunner = serviceProvider.GetService<IPipelineReactionsRunner>();
        var pipelineReactionInvoker = serviceProvider.GetService<IPipelineReactionInvoker>();
        var pipelineStateBag = serviceProvider.GetService<IPipelineStateBag>();

        Assert.That(pipelineFactory, Is.Not.Null);
        Assert.That(pipelineReactionsRunner, Is.Not.Null);
        Assert.That(pipelineReactionInvoker, Is.Not.Null);
        Assert.That(pipelineStateBag, Is.Not.Null);
    }
}