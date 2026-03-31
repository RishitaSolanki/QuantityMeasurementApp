
﻿using Microsoft.Extensions.Configuration;
using QuantityMeasurementConsoleApp.Interfaces;
using QuantityMeasurementapp.Console.Services;

class Program
{
    static void Main()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        IMenu menu = new Menu(config);
        menu.Start();
    }
} 
              