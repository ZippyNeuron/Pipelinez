# Pipelinez
Pipelinez is a class library that can be used to create input and output processing lines. Based on the Chain of Responsibility pattern.

## Example Usage

### Add the Pipelinez service dependencies to your dependency injection container
```
var serviceProvider = new ServiceCollection()
    .AddPipelinezServices()
    .BuildServiceProvider();
```

### Create the pipeline factory
The pipeline factory is used to create the action pipelines.  It can be injected into your class or created directly using the service provider.
```
var pipelineFactory = 
    serviceProvider.GetRequiredService<IPipelineFactory>();
```

### Create classes that represent your pipeline input and output.
For a pipeline to function it requires data to operate on (the input) and a means to return results (the output).  The following classes enable that function.
```
public class TheInput()
{
    public Guid Id { get; set; }
}

public class TheOutput()
{
    public Guid Id { get; set; }
}
```

### Define a simple pipeline action
Preactions are processed in parallel while Reactions are processed in serial and in the order they are defined.  Preactions can be used to retrieve data that can be used during the pipeline execution.  For example, retrieving important information used in a calculation from an API or database. The PipelineReactionsBuilder can be used for both Preactions and Reactions.  Preactions and Reactions can also be chained using the builder.  Any chained reactions defined in Preactions will be flattened and processed in parallel.

```
public sealed class SimplePipeline : PipelineCore<TheInput, TheOutput>
{
    public CarpetFittingPipeline()
    {
        Preactions = []

        Reactions = new PipelineReactionsBuilder<TheInput, TheOutput>()
            .Add<SimpleReaction>()
            .Build();
    }
}
```

### Define a simple pipeline reaction
The reaction is where the actual processing of the input and output occurs.  The reaction can be used to modify the output or to perform any other processing that is required.  The reaction can also be used to throw exceptions if the processing fails.  As mentioned earlier, reactions can be chained.  If a reaction returns false, the chain reaction will end and the pipeline will continue with the next peer reaction.
```
public sealed class SimpleReaction : IPipelineReaction<TheInput, TheOutput>
{
    public Task<bool> React(
        TheInput input,
        TheOutputput output,
        IServiceProvider? serviceProvider,
        IPipelineStateBag pipelineStateBag)
    {
        // in this simple reaction we'll just assign the input Id to the output Id
        output.Id = input.Id;

        return Task.FromResult(true);
    }
}
```

### - Create a pipeline with your input and output types
Create a pipeline based on the input and output types.
```
var pipeline = pipelineFactory.CreatePipeline<TheInput, TheOutput>();
```

## Run the pipeline and return the output
The pipeline can be run using the Action method by passing the pipeline definition as the generic.  The Action method will return the output of the pipeline.  This simple pipeline consisting of a single reaction is designed to transfer the Id value of the input to the Id of the output.  To check this we can compare the Id values of the input and output.
```
var theInput = 
    new TheInput { Id = Guid.NewGuid() };

var theOutput = 
    await pipeline.Action<SimplePipeline>(theInput);

Console.WriteLIne(theOutput.Id);
```