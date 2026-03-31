using QuantityMeasurementApp.Model.Enums;

public static class VolumeUnitExtensions
{
    // Base unit: LITRE
    public static double ConvertToBaseUnit(this VolumeUnit unit, double value) => value * ToBaseUnit(unit);

    public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue) => baseValue / ToBaseUnit(unit);

    public static double ToBaseUnit(this VolumeUnit unit) => unit switch
    {
        VolumeUnit.LITRE      => 1.0,
        VolumeUnit.MILLILITRE => 0.001,
        VolumeUnit.GALLON     => 3.78541,
        _ => throw new ArgumentException($"Unsupported VolumeUnit: {unit}")
    };
}
