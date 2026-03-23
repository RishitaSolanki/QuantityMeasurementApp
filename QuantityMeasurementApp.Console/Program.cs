﻿﻿using Microsoft.Extensions.Configuration;
using IMenu = QuantityMeasurementApp.Console.Interfaces.IMenu;
using Menu = QuantityMeasurementApp.Console.Services.Menu;

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