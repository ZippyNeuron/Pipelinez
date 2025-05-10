namespace ZippyNeuron.Pipelinez.Extensions;

public static class PipelineReactionDefinitionsExtensions
{
    public static IEnumerable<PipelineReactionDefinition> ToReactionList(
        this IEnumerable<PipelineReactionDefinition> reactionDefinitions)
    {
        var list = new List<PipelineReactionDefinition>();

        foreach (var reactionDefinition in reactionDefinitions)
        {
            list.Add(reactionDefinition);

            if (reactionDefinition.Reactions.Count > 0)
            {
                list.AddRange(ToReactionList(reactionDefinition.Reactions));
            }
        }

        return list;
    }
}