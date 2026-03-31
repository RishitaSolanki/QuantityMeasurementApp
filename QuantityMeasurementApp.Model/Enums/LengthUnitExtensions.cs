using QuantityMeasurementApp.Model.Enums;

public static class LengthUnitExtensions
{
    // Base unit: FEET
    public static double ConvertToBaseUnit(this LengthUnit unit, double value) => value * GetConversionFactor(unit);

    public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue) => baseValue / GetConversionFactor(unit);

    public static double GetConversionFactor(this LengthUnit unit) => unit switch
    {
        LengthUnit.FEET        => 1.0,
        LengthUnit.INCHES      => 1.0 / 12.0,
        LengthUnit.YARDS       => 3.0,
        LengthUnit.CENTIMETERS => 1.0 / 30.48,
        _ => throw new ArgumentException($"Unsupported LengthUnit: {unit}")
    };
}
