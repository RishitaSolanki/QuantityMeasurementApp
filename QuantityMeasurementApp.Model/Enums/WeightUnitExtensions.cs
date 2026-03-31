using QuantityMeasurementApp.Model.Enums;

public static class WeightUnitExtensions
{
    // Base unit: KILOGRAM
    public static double ConvertToBaseUnit(this WeightUnit unit, double value) => value * GetConversionFactor(unit);

    public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue) => baseValue / GetConversionFactor(unit);

    public static double GetConversionFactor(this WeightUnit unit) => unit switch
    {
        WeightUnit.KILOGRAM => 1.0,
        WeightUnit.GRAM     => 0.001,
        WeightUnit.POUND    => 0.453592,
        _ => throw new ArgumentException($"Unsupported WeightUnit: {unit}")
    };
}
