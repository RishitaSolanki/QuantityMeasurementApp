using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Model;
namespace QuantityMeasurementApp.Business.Services;
    public class QuantityLengthUtility: IQuantityLength
    {
        public bool CheckEquality(double value1, Unit unit1,double value2, Unit unit2)
        {
            Quantity q1 = new Quantity(value1, unit1);
            Quantity q2 = new Quantity(value2, unit2);

            return q1.Equals(q2);
        }
    }