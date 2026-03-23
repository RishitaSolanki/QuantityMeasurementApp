using System;
using static System.Console;
using QuantityMeasurementApp.BusinessLayer.Interfaces;
using QuantityMeasurementApp.BusinessLayer.Services;
using QuantityMeasurementApp.Model.DTO;
using QuantityMeasurementApp.RepositoryLayer.Interfaces;
using QuantityMeasurementApp.RepositoryLayer.Repositories;
using QuantityMeasurementApp.RepositoryLayer.Services;
using Microsoft.Extensions.Configuration;

namespace QuantityMeasurementApp.Console.Services;

public class Menu : global::QuantityMeasurementApp.Console.Interfaces.IMenu
{
    private readonly IConfiguration config;
    private IQuantityMeasurementService? service;
    private IQuantityMeasurementRepository? repository;
    private IQuantityMeasurementRepository? databaseRepository;
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
        WriteLine("\n=== Checking Database Connectivity ===");
        
        bool isDatabaseAvailable = CheckDatabaseConnectivity();
        
        if (!isDatabaseAvailable)
        {
            WriteLine("❌ Database is not available. Using Cache Memory until database becomes active.");
            repository = new QuantityMeasurementCacheRepository();
            isUsingCache = true;
            service = new QuantityMeasurementServiceImpl(repository);
            return;
        }
        
        WriteLine("✅ Database is available!");
        CheckAndUploadPendingJsonData();
        
        WriteLine("\n=== Storage Selection ===");
        WriteLine("Choose your preferred storage method:");
        WriteLine();
        WriteLine("1. Cache Memory (Fast, In-Memory Storage with JSON Persistence)");
        WriteLine();
        WriteLine("2. Database (Persistent SQL Server Storage)");
        WriteLine();
        
        while (true)
        {
            Write("Enter your choice (1 for Cache, 2 for Database): ");
            string input = ReadLine() ?? string.Empty;
            input = input.Trim();
            
            switch (input)
            {
                case "1":
                    WriteLine("\n✓ Cache Memory selected - Using in-memory storage with JSON persistence");
                    repository = new QuantityMeasurementCacheRepository();
                    isUsingCache = true;
                    break;
                    
                case "2":
                    WriteLine("\n✓ Database selected - Using SQL Server persistent storage");
                    repository = new QuantityMeasurementDatabaseRepository(config);
                    isUsingCache = false;
                    break;
                    
                default:
                    WriteLine("Invalid choice. Please enter 1 or 2.");
                    continue;
            }

            service = new QuantityMeasurementServiceImpl(repository);
            
            if (isUsingCache)
            {
                if (databaseRepository is null)
                {
                    databaseRepository = new QuantityMeasurementDatabaseRepository(config);
                }
                syncService = new DataSyncService((ICacheRepository)repository, databaseRepository);
            }
            
            break;
        }
    }

    private void CheckAndUploadPendingJsonData()
    {
        try
        {
            var tempCacheRepo = new QuantityMeasurementCacheRepository();
            
            if (tempCacheRepo.HasPendingData())
            {
                WriteLine("\n📝 Found pending data in JSON file from previous session.");
                if (databaseRepository is null)
                {
                    databaseRepository = new QuantityMeasurementDatabaseRepository(config);
                }
                var tempSyncService = new DataSyncService(tempCacheRepo, databaseRepository);
                bool success = tempSyncService.UploadPendingDataToDatabase(silent: false);
                
                if (success)
                    WriteLine("✅ Pending data uploaded to database!\n");
                else
                    WriteLine("⚠️ Failed to upload pending data. Data remains in JSON file.\n");
            }
        }
        catch (Exception ex)
        {
            WriteLine($"⚠️ Error checking pending data: {ex.Message}\n");
        }
    }

    private bool CheckDatabaseConnectivity()
    {
        try
        {
            WriteLine("Testing database connection...");
            if (databaseRepository is null)
            {
                databaseRepository = new QuantityMeasurementDatabaseRepository(config);
            }
            return databaseRepository.TestConnection();
        }
        catch (Exception ex)
        {
            WriteLine($"Database connectivity check failed: {ex.Message}");
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
                if (service != null) { PrintResult(service.Add(firstValue, secondValue)); AutoUploadToDatabase(); }
                break;
            case 2:
                if (service != null) { PrintResult(service.Subtract(firstValue, secondValue)); AutoUploadToDatabase(); }
                break;
            case 3:
                if (service != null) { WriteLine("Result = " + service.Divide(firstValue, secondValue)); AutoUploadToDatabase(); }
                break;
            case 4:
                if (service != null) { WriteLine("Are Equal = " + service.Compare(firstValue, secondValue)); AutoUploadToDatabase(); }
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
            AutoUploadToDatabase();
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

    private void AutoUploadToDatabase()
    {
        if (!isUsingCache || syncService == null) return;
        var cacheRepo = (ICacheRepository)repository!;
        if (!cacheRepo.HasPendingData()) return;

        try
        {
            if (!CheckDatabaseConnectivity()) { WriteLine("⚠️ Database not available"); return; }
            bool success = syncService.UploadPendingDataToDatabase(silent: true);
            if (success) WriteLine("✅ Data automatically uploaded to database");
        }
        catch (Exception ex)
        {
            WriteLine($"⚠️ Auto-upload failed: {ex.Message}");
        }
    }
}
