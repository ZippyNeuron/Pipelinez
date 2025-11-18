namespace ZippyNeuron.Pipelinez.Console.Financial;

public static class DoubleExtensions
{
    public static double ToRounded(this double value, int places) =>
        Math.Round(value, places, MidpointRounding.ToPositiveInfinity);
}