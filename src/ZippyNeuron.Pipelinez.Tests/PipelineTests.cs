using NSubstitute;
using System.Reflection;
using ZippyNeuron.Pipelinez.Tests.Setup;
namespace ZippyNeuron.Pipelinez.Tests;

[TestFixture]
public class PipelineTests
{
    private IServiceProvider _serviceProvider;
    private IPipelineFactory _pipelineFactory;

    [SetUp]
    public void Setup()
    {
        _serviceProvider = Substitute.For<IServiceProvider>();
        _serviceProvider.GetService(typeof(IPipelineStateBag))
            .Returns(new PipelineStateBag());

        _pipelineFactory = new PipelineFactory(_serviceProvider);
    }

    [Test]
    public async Task Pipeline_WithInput_ReturnsCorrectOutput()
    {
        var input = new PipelineTestInput();
        var sut = _pipelineFactory.Create<PipelineTestInput, PipelineTestOutput>();

        var result = await sut.Action<PipelineTest>(input);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(input.Id));
    }

    [Test]
    public async Task Pipeline_CalledTwice_ReturnsCorrectOutput()
    {
        var input = new PipelineTestInput();
        var sut = _pipelineFactory.Create<PipelineTestInput, PipelineTestOutput>();

        var _ = await sut.Action<PipelineTest>(input);
        var result = await sut.Action<PipelineTest>(input);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(input.Id));
    }

    [Test]
    public void Pipeline_WithErrorInReaction_ThrowsException()
    {
        var input = new PipelineTestInput();
        var sut = _pipelineFactory.Create<PipelineTestInput, PipelineTestOutput>();

        Assert.ThrowsAsync<TargetInvocationException>(
            async () => await sut.Action<PipelineTestException>(input));
    }
}