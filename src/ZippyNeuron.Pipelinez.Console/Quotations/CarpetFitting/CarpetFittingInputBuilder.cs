namespace WorkbenchWebApi.Pipelines.Console.Quotations.CarpetFitting;

public class CarpetFittingInputBuilder
{
    private double _hoursToFit;
    private double _hourlyRate;
    private double _fixedLabourCost;
    private double _totalSquareMeters;
    private double _carpetCostPerSquareMeter;
    private double _underlayCostPerSquareMeter;
    private double _thresholdCosts;
    private double _carpetGripperCosts;
    private double _additionalCosts;

    public CarpetFittingInputBuilder SetEstimatedHours(double value)
    {
        _hoursToFit = value;
        return this;
    }

    public CarpetFittingInputBuilder SetHourlyRate(double value)
    {
        _hourlyRate = value;
        return this;
    }

    public CarpetFittingInputBuilder SetFixedLabourCost(double value)
    {
        _fixedLabourCost = value;
        return this;
    }

    public CarpetFittingInputBuilder SetTotalSquareMeters(double value)
    {
        _totalSquareMeters = value;
        return this;
    }

    public CarpetFittingInputBuilder SetCarpetCostPerSquareMeter(double value)
    {
        _carpetCostPerSquareMeter = value;
        return this;
    }

    public CarpetFittingInputBuilder SetUnderlayCostPerSquareMeter(double value)
    {
        _underlayCostPerSquareMeter = value;
        return this;
    }

    public CarpetFittingInputBuilder SetThresholdCosts(double value)
    {
        _thresholdCosts = value;
        return this;
    }

    public CarpetFittingInputBuilder SetCarpetGripperCosts(double value)
    {
        _carpetGripperCosts = value;
        return this;
    }

    public CarpetFittingInputBuilder SetAdditionalCosts(double value)
    {
        _additionalCosts = value;
        return this;
    }

    public CarpetFittingInput Build() =>
        new()
        {
            TimeInHours = _hoursToFit,
            HourlyRate = _hourlyRate,
            FixedLabourCost = _fixedLabourCost,
            TotalSquareMeters = _totalSquareMeters,
            CarpetCostPerSquareMeter = _carpetCostPerSquareMeter,
            UnderlayCostPerSquareMeter = _underlayCostPerSquareMeter,
            ThresholdCosts = _thresholdCosts,
            CarpetGripperCosts = _carpetGripperCosts,
            AdditionalCosts = _additionalCosts
        };
}