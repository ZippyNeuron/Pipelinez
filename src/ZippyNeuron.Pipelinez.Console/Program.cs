using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using ZippyNeuron.Pipelinez;
using ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting;

var jsonOptions =
    new JsonSerializerOptions() { WriteIndented = true };

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

var vat1 = await pipeline
    .Action<CarpetFittingPipeline>(input);

var noVat = await pipeline
    .Action<CarpetFittingNoVatPipeline>(input);

var vat2 = await pipeline
    .Action<CarpetFittingPipeline>(input);

Console.WriteLine(JsonSerializer.Serialize(vat1, jsonOptions));

Console.WriteLine(JsonSerializer.Serialize(noVat, jsonOptions));

Console.WriteLine(JsonSerializer.Serialize(vat2, jsonOptions));