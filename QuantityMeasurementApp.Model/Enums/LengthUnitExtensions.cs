using QuantityMeasurementApp.Model.Enums;

public static class LengthUnitExtensions
{
    // Conversion factors relative to FEET as base unit
    public static double ConvertToBaseUnit(this LengthUnit unit, double value)
    {
        switch (unit)
        {
            case LengthUnit.FEET:        return value;
            case LengthUnit.INCHES:      return value / 12.0;
            case LengthUnit.YARDS:       return value * 3.0;
            case LengthUnit.CENTIMETERS: return value / 30.48;
            default: throw new ArgumentException($"Unsupported LengthUnit: {unit}");
        }
    }

    public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
    {
        switch (unit)
        {
            case LengthUnit.FEET:        return baseValue;
            case LengthUnit.INCHES:      return baseValue * 12.0;
            case LengthUnit.YARDS:       return baseValue / 3.0;
            case LengthUnit.CENTIMETERS: return baseValue * 30.48;
            default: throw new ArgumentException($"Unsupported LengthUnit: {unit}");
        }
    }

    public static double GetConversionFactor(this LengthUnit unit)
    {
        switch (unit)
        {
            case LengthUnit.FEET:        return 1.0;
            case LengthUnit.INCHES:      return 1.0 / 12.0;
            case LengthUnit.YARDS:       return 3.0;
            case LengthUnit.CENTIMETERS: return 1.0 / 30.48;
            default: throw new ArgumentException($"Unsupported LengthUnit: {unit}");
        }
    }
}
