// See https://aka.ms/new-console-template for more information

using Community.Extensions.Spectre.Cli.Hosting;

using Frank.Games.Common;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;

using Spectre.Console;
using Spectre.Console.Cli;

AnsiConsole.Write((new FigletText("Checkers").Color(Color.Red)).Justify(Justify.Left));

var builder = Host.CreateApplicationBuilder();

builder.Services.AddSingleton<CheckersGame>();
builder.UseSpectreConsole<StartGameCommand>();

var host = builder.Build();
await host.StartAsync();

public class StartGameCommand : Command<StartGameCommand.Settings>
{
    private readonly CheckersGame _game;

    public StartGameCommand(CheckersGame game)
    {
        _game = game;
    }

    public class Settings : CommandSettings
    {
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        _game.MovePiece(0, 0, 1, 1);
        
        return 0;
    }
}
