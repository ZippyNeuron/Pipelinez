namespace ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting;

public sealed class CarpetFittingOutput
{
    public string? Reference { get; internal set; }
    public double Time { get; internal set; }
    public double Materials { get; internal set; }
    public double Other { get; internal set; }
    public double Total { get; internal set; }
    public double Vat { get; internal set; }
    public double VatRate { get; internal set; }
}