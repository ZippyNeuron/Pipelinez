using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ZippyNeuron.Pipelinez;
using ZippyNeuron.Pipelinez.Console.VatApi;

namespace WorkbenchWebApi.Pipelines.Console.Quotations.CarpetFitting.Reactions;

public sealed class VatLookupReaction : IPipelineReaction<CarpetFittingInput, CarpetFittingOutput>
{
    public async Task<bool> React(
        CarpetFittingInput input,
        CarpetFittingOutput output,
        IServiceProvider? serviceProvider,
        IPipelineStateBag pipelineStateBag)
    {
        try
        {
            var httpClientFactory =
                serviceProvider?.GetService<IHttpClientFactory>();

            var vatLookupApiClient = httpClientFactory?
                .CreateClient("VatLookupApiClient");

            var jsonRates = await vatLookupApiClient!
                .GetStringAsync("rates/uk");

            var vatResult = JsonSerializer.Deserialize<VatResult>(
                jsonRates, 
                JsonSerializerOptions.Web);

            var vatRate = (vatResult?
                .Rates?.First(r => r.Name == "Standard")?
                .Rates?.FirstOrDefault() ?? 0) / 100;

            pipelineStateBag.Set("VatRate", vatRate);
            
            return true;
        } catch(Exception ex)
        {
            var logger =
                serviceProvider?.GetService<ILogger>();

            logger?.LogCritical("Failed to retrieve VAT rate");

            throw new PipelineReactionException("Failed to retrieve VAT rate", ex);
        }
    }
}