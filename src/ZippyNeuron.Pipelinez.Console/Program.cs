using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WorkbenchWebApi.Pipelines.Console.Quotations.CarpetFitting;
using ZippyNeuron.Pipelinez;

var serviceProviderBuilder = new ServiceCollection()
    .AddPipelineServices();

serviceProviderBuilder
    .AddHttpClient("VatLookupApiClient", (httpClient) =>
    {
        httpClient.BaseAddress = new Uri("https://api.vatlookup.eu");
    });

var serviceProvider = 
    serviceProviderBuilder.BuildServiceProvider();

var pipelineFactory = 
    serviceProvider.GetRequiredService<IPipelineFactory>();

var pipeline = pipelineFactory
    .Create<CarpetFittingInput, CarpetFittingOutput>();

var input = new CarpetFittingInputBuilder()
    .SetEstimatedHours(7.5)
    .SetHourlyRate(22.24)
    .SetFixedLabourCost(0)
    .SetTotalSquareMeters(9)
    .SetCarpetCostPerSquareMeter(37.48)
    .SetUnderlayCostPerSquareMeter(4.86)
    .SetThresholdCosts(12.66)
    .SetCarpetGripperCosts(36.55)
    .SetAdditionalCosts(1.24)
    .Build();

var output = await pipeline
    .Action<CarpetFittingPipeline>(input);

var jsonOptions =
    new JsonSerializerOptions() { WriteIndented = true };

Console.WriteLine(JsonSerializer.Serialize(output, jsonOptions));