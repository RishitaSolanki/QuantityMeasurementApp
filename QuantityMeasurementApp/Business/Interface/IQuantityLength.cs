using QuantityMeasurementApp.Model;
namespace QuantityMeasurementApp.Business.Interfaces;

    public interface IQuantityLength
    {
        bool CheckEquality(double value1, Unit unit1, double value2, Unit unit2);
    }