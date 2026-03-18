﻿using Microsoft.Extensions.DependencyInjection;
using QuantityMeasurementApp.BusinessLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementApp.Console.Interfaces;

class Program
{
    static void Main()
    {
        IMenu menu = new Menu();
        menu.Start();
    }
}