namespace ZippyNeuron.Pipelinez;

public sealed class PipelineReactionException(string message, Exception ex)
    : Exception(message, ex) { }