using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModelLayer.Entities
{
    public class QuantityMeasurementEntity
    {

        public int Id { get; set; }

        [Required]
        public double FirstValue { get; set; }
        [Required]
        public string FirstUnit { get; set; } = null!;
        [Required]
        public double SecondValue { get; set; }
        [Required]
        public string SecondUnit { get; set; } = null!;
        [Required]
        public string Operation { get; set; } = null!;
        
        public double Result { get; set; }

        public string MeasurementType { get; set; } = null!;

        public QuantityMeasurementEntity() { }

        public QuantityMeasurementEntity(
            double firstValue,
            string firstUnit,
            double secondValue,
            string secondUnit,
            string operation,
            double result,
            string measurementType)
        {
            FirstValue = firstValue;
            FirstUnit = firstUnit;
            SecondValue = secondValue;
            SecondUnit = secondUnit;
            Operation = operation;
            Result = result;
            MeasurementType = measurementType;
        }
    }
}