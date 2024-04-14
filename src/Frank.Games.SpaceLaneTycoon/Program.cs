// See https://aka.ms/new-console-template for more information

using System.Globalization;

using Frank.Games.SpaceLaneTycoon;
using Frank.Games.SpaceLaneTycoon.Mapping;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMapping();
        services.AddSingleton<Assets>();
        services.AddSingleton<StarCache>();
        
        services.AddSingleton<MainWindow>();
        services.AddHostedService<WindowHost>();
    });

await builder.RunConsoleAsync();