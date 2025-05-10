namespace ZippyNeuron.Pipelinez;

public sealed class PipelineReactionDefinition(Type definitionType)
{
    public readonly List<PipelineReactionDefinition> Reactions = [];
    public Type Type { get; init; } = definitionType;
}