using QuantityMeasurementApp.Model.Enums;

public static class WeightUnitExtensions
{
    public static double ConvertToBaseUnit(this WeightUnit unit, double value)
    {
        switch (unit)
        {
            case WeightUnit.KILOGRAM: return value;
            case WeightUnit.GRAM:     return value * 0.001;
            case WeightUnit.POUND:    return value * 0.453592;
            default: throw new ArgumentException($"Unsupported WeightUnit: {unit}");
        }
    }

    public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
    {
        switch (unit)
        {
            case WeightUnit.KILOGRAM: return baseValue;
            case WeightUnit.GRAM:     return baseValue / 0.001;
            case WeightUnit.POUND:    return baseValue / 0.453592;
            default: throw new ArgumentException($"Unsupported WeightUnit: {unit}");
        }
    }

    public static double GetConversionFactor(this WeightUnit unit)
    {
        switch (unit)
        {
            case WeightUnit.KILOGRAM: return 1.0;
            case WeightUnit.GRAM:     return 0.001;
            case WeightUnit.POUND:    return 0.453592;
            default: throw new ArgumentException($"Unsupported WeightUnit: {unit}");
        }
    }
}
