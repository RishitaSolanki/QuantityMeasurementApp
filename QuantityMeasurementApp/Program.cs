public class Program
    
    {
        public static bool CompareFeet(double value1, double value2)
        {
            QuantityMeasurementApp.Feet f1 = new QuantityMeasurementApp.Feet(value1);
            QuantityMeasurementApp.Feet f2 = new QuantityMeasurementApp.Feet(value2);

            return f1.Equals(f2);
        }

        public static bool CompareInches(double value1, double value2)
        {
            QuantityMeasurementApp.Inches i1 = new QuantityMeasurementApp.Inches(value1);
            QuantityMeasurementApp.Inches i2 = new QuantityMeasurementApp.Inches(value2);

            return i1.Equals(i2);
        }
        static void Main(string[] args)
        {
             bool inchResult =
                Program.CompareInches(1.0, 1.0);

            bool feetResult =
                Program.CompareFeet(1.0, 1.0);

            Console.WriteLine("Input: 1.0 inch and 1.0 inch");
            Console.WriteLine($"Output: Equal ({inchResult})");

            Console.WriteLine();

            Console.WriteLine("Input: 1.0 ft and 1.0 ft");
            Console.WriteLine($"Output: Equal ({feetResult})");

            Console.ReadLine();
        }
    }