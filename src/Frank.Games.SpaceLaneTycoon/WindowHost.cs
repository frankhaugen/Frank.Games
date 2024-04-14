using Frank.Games.SpaceLaneTycoon;

using Microsoft.Extensions.Hosting;

public class WindowHost : BackgroundService
{
    private readonly MainWindow _mainWindow;

    public WindowHost(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Starting...");
        await _mainWindow.RunAsync(stoppingToken);
    }
}