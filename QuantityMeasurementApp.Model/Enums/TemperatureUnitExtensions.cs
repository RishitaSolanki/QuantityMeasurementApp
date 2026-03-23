using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Model.Exceptions;

public static class TemperatureUnitExtensions
{
    // Base unit is CELSIUS
    public static double ConvertToBaseUnit(this TemperatureUnit unit, double value)
    {
        switch (unit)
        {
            case TemperatureUnit.CELSIUS:    return value;
            case TemperatureUnit.FAHRENHEIT: return (value - 32) * 5.0 / 9.0;
            default: throw new ArgumentException($"Unsupported TemperatureUnit: {unit}");
        }
    }

    public static double ConvertFromBaseUnit(this TemperatureUnit unit, double baseValue)
    {
        switch (unit)
        {
            case TemperatureUnit.CELSIUS:    return baseValue;
            case TemperatureUnit.FAHRENHEIT: return baseValue * 9.0 / 5.0 + 32;
            default: throw new ArgumentException($"Unsupported TemperatureUnit: {unit}");
        }
    }

    public static bool SupportsArithmetic(this TemperatureUnit unit) => false;

    public static void ValidateOperationSupport(this TemperatureUnit unit, string operation)
    {
        throw new UnsupportedOperationException($"Temperature does not support {operation}");
    }
}
