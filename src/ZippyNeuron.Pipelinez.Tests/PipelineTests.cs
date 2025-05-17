using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using System;
using System.Reflection;
using ZippyNeuron.Pipelinez.Tests.Setup;

namespace ZippyNeuron.Pipelinez.Tests;

[TestFixture]
public class PipelineTests
{
    private IServiceCollection _serviceCollection;

    [SetUp]
    public void Setup()
    {
        _serviceCollection = new ServiceCollection()
            .AddPipelineServices();
    }

    [Test]
    public async Task Pipeline_WithInput_ReturnsCorrectOutput()
    {
        var input = new PipelineTestInput();
        var serviceProvider = _serviceCollection.BuildServiceProvider();
        var pipelineFactory = serviceProvider.GetRequiredService<IPipelineFactory>();
        var sut = pipelineFactory.Create<PipelineTestInput, PipelineTestOutput>();

        var result = await sut.Action<PipelineTest>(input);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(input.Id));
    }

    [Test]
    public async Task Pipeline_InvokerReturnsNull_NoErrorOutputUnchanged()
    {
        var input = new PipelineTestInput();
        _serviceCollection.RemoveAll<IPipelineReactionInvoker>();
        var pipelineReactionInvoker = Substitute.For<IPipelineReactionInvoker>();
        _ = pipelineReactionInvoker.Invoke(
            Arg.Any<IPipelineReaction<PipelineTestInput, PipelineTestOutput>>(),
            Arg.Any<PipelineTestInput>(),
            Arg.Any<PipelineTestOutput>(),
            Arg.Any<IServiceProvider>(),
            Arg.Any<IPipelineStateBag>()
            ).Returns(null as Task<bool>);
        _serviceCollection.AddTransient(_ => pipelineReactionInvoker);
        var serviceProvider = _serviceCollection.BuildServiceProvider();
        var pipelineFactory = serviceProvider.GetRequiredService<IPipelineFactory>();
        var sut = pipelineFactory.Create<PipelineTestInput, PipelineTestOutput>();

        var output = await sut.Action<PipelineTest>(input);

        Assert.That(output, Is.Not.Null);
        Assert.That(output.Id, Is.Null);
    }

    [Test]
    public async Task Pipeline_CalledTwice_ReturnsCorrectOutput()
    {
        var input = new PipelineTestInput();
        var serviceProvider = _serviceCollection.BuildServiceProvider();
        var pipelineFactory = serviceProvider.GetRequiredService<IPipelineFactory>();
        var sut = pipelineFactory.Create<PipelineTestInput, PipelineTestOutput>();

        var _ = await sut.Action<PipelineTest>(input);
        var result = await sut.Action<PipelineTest>(input);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(input.Id));
    }

    [Test]
    public void Pipeline_WithErrorInReaction_ThrowsException()
    {
        var input = new PipelineTestInput();
        var serviceProvider = _serviceCollection.BuildServiceProvider();
        var pipelineFactory = serviceProvider.GetRequiredService<IPipelineFactory>();
        var sut = pipelineFactory.Create<PipelineTestInput, PipelineTestOutput>();

        Assert.ThrowsAsync<TargetInvocationException>(
            async () => await sut.Action<PipelineTestException>(input));
    }
}