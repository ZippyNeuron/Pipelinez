namespace ZippyNeuron.Pipelinez.Console.Quotations.CarpetFitting;

public sealed class CarpetFittingInput
{
    public Guid FittingId { get; init; }
    public double TimeInHours { get; init; }
    public double HourlyRate { get; init; }
    public double FixedLabourCost { get; init; }
    public double TotalSquareMeters { get; init; }
    public double CarpetCostPerSquareMeter { get; init; }
    public double UnderlayCostPerSquareMeter { get; init; }
    public double ThresholdCosts { get; init; }
    public double CarpetGripperCosts { get; init; }
    public double AdditionalCosts { get; init; }
}