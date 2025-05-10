using NSubstitute;
using ZippyNeuron.Pipelinez.Tests.Setup;

namespace ZippyNeuron.Pipelinez.Tests;

[TestFixture]
public class PipelineFactoryTests
{
    private IServiceProvider _serviceProvider;

    [SetUp]
    public void Setup()
    {
        _serviceProvider = Substitute.For<IServiceProvider>();
    }

    [Test]
    public void PipelineFactory_CanCreate_ReturnsPipelineFactory()
    {
        IPipelineFactory sut = 
            new PipelineFactory(_serviceProvider);

        Assert.That(sut, Is.Not.Null);
    }

    [Test]
    public void PipelineFactory_CanCreatePipeline_ReturnsPipeline()
    {
        IPipelineFactory pipelineFactory = 
            new PipelineFactory(_serviceProvider);

        var sut = pipelineFactory
            .Create<PipelineTestInput, PipelineTestOutput>();

        Assert.That(sut, Is.Not.Null);
    }
}