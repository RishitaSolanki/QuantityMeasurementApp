using QuantityMeasurementApp.Model.Enums;

public static class VolumeUnitExtensions
{
    public static double ToBaseUnit(this VolumeUnit unit)
    {
        switch (unit)
        {
            case VolumeUnit.LITRE:      return 1.0;
            case VolumeUnit.MILLILITRE: return 0.001;
            case VolumeUnit.GALLON:     return 3.78541;
            default: throw new ArgumentException($"Unsupported VolumeUnit: {unit}");
        }
    }

    public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
    {
        return value * unit.ToBaseUnit();
    }

    public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
    {
        return baseValue / unit.ToBaseUnit();
    }
}
