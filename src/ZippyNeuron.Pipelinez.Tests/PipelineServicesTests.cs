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
        var provider = _serviceCollection
            .AddPipelineServices()
            .BuildServiceProvider();

        var pipelineFactory = provider.GetService<IPipelineFactory>();
        var pipelineStateBag = provider.GetService<IPipelineStateBag>();

        Assert.That(pipelineFactory, Is.Not.Null);
        Assert.That(pipelineStateBag, Is.Not.Null);
    }
}
