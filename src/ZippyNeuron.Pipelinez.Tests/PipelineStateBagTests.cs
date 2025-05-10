namespace ZippyNeuron.Pipelinez.Tests;

[TestFixture]
public class PipelineStateBagTests
{
    private IPipelineStateBag _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new PipelineStateBag();
    }

    [TearDown]
    public void TearDown()
    {
        _sut.Dispose();
    }

    [Test]
    public void SetAndGet_ValidKeyAndValue_ReturnsCorrectValue()
    {
        var key = "TestKey";
        var value = 42;
        _sut.Set(key, value);

        var result = _sut.Get<int>(key);

        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void SetAndGet_InvalidKey_ReturnsDefault()
    {
        var key = "TestKey";

        var result = _sut.Get<int>(key);

        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void SetAndGet_KeyIsInvalidType_ReturnsDefault()
    {
        var key = "TestKey";
        var value = "TestValue";
        _sut.Set(key, value);

        var result = _sut.Get<bool>(key);

        Assert.That(result, Is.EqualTo(false));
    }
}