namespace ZippyNeuron.Pipelinez.Extensions;

public static class PipelineReactionDefinitionsExtensions
{
    public static IEnumerable<PipelineReactionDefinition<TInput, TOutput>> ToReactionList<TInput, TOutput>(
        this IEnumerable<PipelineReactionDefinition<TInput, TOutput>> reactionDefinitions)
    {
        var list = new List<PipelineReactionDefinition<TInput, TOutput>>();

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