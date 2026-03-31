using System;
using static System.Console;
using QuantityMeasurementApp.BusinessLayer.Interfaces;
using QuantityMeasurementApp.BusinessLayer.Services;
using QuantityMeasurementApp.Model.DTO;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementRepositoryLayer.Services;
using QuantityMeasurementConsoleApp.Interfaces;
using Microsoft.Extensions.Configuration;

namespace QuantityMeasurementapp.Console.Services;

public class Menu : IMenu
{
    private readonly IConfiguration config;
    private IQuantityMeasurementService? service;
    private IQuantityMeasurementRepository? repository;
    private DataSyncService? syncService;
    private bool isUsingCache;

    public Menu(IConfiguration config)
    {
        this.config = config;
    }

    public void Start()
    {
        SelectStorage();

        while (true)
        {
            try
            {
                WriteLine("\n===== Quantity Measurement System =====");
                WriteLine("1 Length");
                WriteLine("2 Volume");
                WriteLine("3 Weight");
                WriteLine("4 Temperature");
                WriteLine("5 Show Records");
                WriteLine("6 Exit");

                Write("Enter choice: ");
                int type = Convert.ToInt32(ReadLine());

                if (type == 6) return;

                if (type == 5)
                {
                    ShowAllData();
                    continue;
                }

                OperationMenu(type);
            }
            catch (Exception ex)
            {
                WriteLine("Error: " + ex.Message);
            }
        }
    }

    private void SelectStorage()
    {
        WriteLine("\n=== Storage Selection ===");
        WriteLine("1. Cache Memory");
        WriteLine("2. Database");

        while (true)
        {
            Write("Enter your choice (1 for Cache, 2 for Database): ");
            string input = (ReadLine() ?? string.Empty).Trim();

            switch (input)
            {
                case "1":
                    WriteLine("\n✓ Cache Memory selected");
                    isUsingCache = true;
                    break;
                case "2":
                    WriteLine("\n✓ Database selected");
                    isUsingCache = false;
                    break;
                default:
                    WriteLine("Invalid choice. Please enter 1 or 2.");
                    continue;
            }
            break;
        }
    }

    private bool CheckDatabaseConnectivity()
    {
        try
        {
            return repository?.TestConnection() ?? false;
        }
        catch
        {
            return false;
        }
    }

    private void OperationMenu(int type)
    {
        WriteLine("\nSelect Operation");
        WriteLine("1 Add");
        WriteLine("2 Subtract");
        WriteLine("3 Divide");
        WriteLine("4 Compare");
        WriteLine("5 Convert");

        Write("Enter Operation: ");
        int operation = Convert.ToInt32(ReadLine());

        QuantityDTO firstValue = GetQuantity(type);

        if (operation == 5)
        {
            ConvertUnit(firstValue, type);
            return;
        }

        QuantityDTO secondValue = GetQuantity(type);

        switch (operation)
        {
            case 1:
                if (service != null) { PrintResult(service.Add(firstValue, secondValue)); }
                break;
            case 2:
                if (service != null) { PrintResult(service.Subtract(firstValue, secondValue)); }
                break;
            case 3:
                if (service != null) { WriteLine("Result = " + service.Divide(firstValue, secondValue)); }
                break;
            case 4:
                if (service != null) { WriteLine("Are Equal = " + service.Compare(firstValue, secondValue)); }
                break;
            default:
                WriteLine("Invalid Operation");
                break;
        }
    }

    private QuantityDTO GetQuantity(int type)
    {
        Write("\nEnter Value: ");
        double value = Convert.ToDouble(ReadLine());
        string unit = SelectUnit(type);
        return new QuantityDTO(value, unit);
    }

    private string SelectUnit(int type)
    {
        WriteLine("Select Unit");

        if (type == 1)
        {
            WriteLine("1 FEET"); WriteLine("2 INCHES"); WriteLine("3 YARDS"); WriteLine("4 CENTIMETERS");
            int choice = Convert.ToInt32(ReadLine());
            return choice switch { 1 => "FEET", 2 => "INCHES", 3 => "YARDS", 4 => "CENTIMETERS", _ => "FEET" };
        }
        else if (type == 2)
        {
            WriteLine("1 LITRE"); WriteLine("2 MILLILITRE"); WriteLine("3 GALLON");
            int choice = Convert.ToInt32(ReadLine());
            return choice switch { 1 => "LITRE", 2 => "MILLILITRE", 3 => "GALLON", _ => "LITRE" };
        }
        else if (type == 3)
        {
            WriteLine("1 KILOGRAM"); WriteLine("2 GRAM"); WriteLine("3 POUND");
            int choice = Convert.ToInt32(ReadLine());
            return choice switch { 1 => "KILOGRAM", 2 => "GRAM", 3 => "POUND", _ => "KILOGRAM" };
        }
        else
        {
            WriteLine("1 CELSIUS"); WriteLine("2 FAHRENHEIT");
            int choice = Convert.ToInt32(ReadLine());
            return choice switch { 1 => "CELSIUS", 2 => "FAHRENHEIT", _ => "CELSIUS" };
        }
    }

    private void ConvertUnit(QuantityDTO firstValue, int type)
    {
        WriteLine("\nSelect Target Unit");
        string targetUnit = SelectUnit(type);
        if (service != null)
        {
            PrintResult(service.Convert(firstValue, targetUnit));
        }
    }

    private void PrintResult(QuantityDTO result)
    {
        WriteLine($"\nResult = {result.Value} {result.Unit}");
    }

    private void ShowAllData()
    {
        if (repository != null)
        {
            var list = repository.GetAll();
            WriteLine("\n===== STORED RECORDS =====");
            foreach (var item in list)
            {
                WriteLine($"Id: {item.Id} | FirstValue: {item.FirstValue} {item.FirstUnit} | SecondValue: {item.SecondValue} {item.SecondUnit} | Operation: {item.Operation} | Result: {item.Result} | Type: {item.MeasurementType}");
            }
            WriteLine("==========================\n");
        }
    }
}
