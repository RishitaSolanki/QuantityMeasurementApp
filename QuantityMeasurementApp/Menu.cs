
using QuantityMeasurementApp;

using QuantityMeasurementApp.Model;


namespace QuantityMeasurementApp;
    internal class Menu
    {
        public static void StartApp()
        {
         try
            {
                Console.WriteLine("Select measurement type (LENGTH / WEIGHT):");
                string type = Console.ReadLine().ToUpper();

                if (type == "LENGTH")
                {
                    ProcessLengthMeasurement();
                }
                else if (type == "WEIGHT")
                {
                    ProcessWeightMeasurement();
                }
                else
                {
                    Console.WriteLine("Invalid measurement type.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadLine();
        }

        static void ProcessLengthMeasurement()
        {
            Console.WriteLine("Enter first value:");
            double value1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Enter first unit ({GetUnitOptions<Unit>()}):");
            Unit unit1 = (Unit)Enum.Parse(typeof(Unit), Console.ReadLine().ToUpper());

            Console.WriteLine("Enter second value:");
            double value2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Enter second unit ({GetUnitOptions<Unit>()}):");
            Unit unit2 = (Unit)Enum.Parse(typeof(Unit), Console.ReadLine().ToUpper());

            Quantity q1 = new Quantity(value1, unit1);
            Quantity q2 = new Quantity(value2, unit2);
            Quantity result = q1.Add(q2);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        static void ProcessWeightMeasurement()
        {
            Console.WriteLine("Enter first value:");
            double value1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Enter first unit ({GetUnitOptions<WeightUnit>()}):");
            WeightUnit unit1 = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine().ToUpper());

            Console.WriteLine("Enter second value:");
            double value2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Enter second unit ({GetUnitOptions<WeightUnit>()}):");
            WeightUnit unit2 = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine().ToUpper());

            QuantityWeight q1 = new QuantityWeight(value1, unit1);
            QuantityWeight q2 = new QuantityWeight(value2, unit2);
            QuantityWeight result = q1.Add(q2);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        static string GetUnitOptions<U>() where U : Enum
        {
            return string.Join(" / ", Enum.GetNames(typeof(U)));
        }
    }

        
    
