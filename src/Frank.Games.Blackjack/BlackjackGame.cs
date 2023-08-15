namespace BlackjackGPT;

public class BlackjackGame
{
    private readonly Player _dealer;
    private readonly Player _player;

    public BlackjackGame(Player player, Player dealer)
    {
        _player = player;
        _dealer = dealer;
    }

    public void Deal()
    {
        for (var i = 0; i < 2; i++)
        {
            _dealer.TakeCard();
            _player.TakeCard();
        }
    }

    public void Play()
    {
        Deal();
        
        Console.ForegroundColor = ConsoleColor.Blue;
        _player.PlayTurn();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        _dealer.PlayTurn();
        
        Console.ResetColor();
        PrintResults();
    }

    private void PrintResults()
    {
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Dealer's hand:");
    Console.ResetColor();
    _dealer.PrintHand();

    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Player's hand:");
    Console.ResetColor();
    _player.PrintHand();
    Console.WriteLine();

    if (_player.IsBust)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You busted! 💥");
    }
    else if (_dealer.IsBust)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Dealer busted! You win! 🥳");
    }
    else if (_player.Score > _dealer.Score)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("You win! 🎉");
    }
    else if (_player.Score < _dealer.Score)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You lose! 😞");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("It's a tie! 🙃");
    }
    Console.ResetColor(); // Reset to default color
}
}