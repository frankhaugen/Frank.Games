namespace BlackjackGPT;

public static class Program
{
    private static void Main(params string[]? args)
    {
        var run = true;
        while (run)
        {
            Console.Clear();

            // You would initialize the game with something like
            var deck = new Deck();
            Player player = new HumanPlayer(deck);
            Player dealer = new LasVegasDealer(deck);
            var game = new BlackjackGame(player, dealer);
            game.Play();

            run = Continue();
        }
    }

    private static bool Continue()
    {
        while (true)
        {
            Console.WriteLine("Game over! Hit ENTER to play again or ESCAPE to quit...\n");
            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape) return false;
            if (key.Key == ConsoleKey.Enter) return true;
        }
    }
}