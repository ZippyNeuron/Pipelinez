namespace ZippyNeuron.Pipelinez.Console.VatApi;

public sealed class VatResult
{
    public string? Disclaimer { get; set; }
    public List<VatRateResult>? Rates { get; set; }
}