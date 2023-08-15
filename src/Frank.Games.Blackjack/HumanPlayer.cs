namespace BlackjackGPT;

public class HumanPlayer : Player
{
    public HumanPlayer(Deck deck) : base(deck)
    {
    }

    public override bool WantsToHit()
    {
        // implementation for checking if human player wants to hit
        PrintHand();
        Console.WriteLine("Do you want to hit? (y/n)");
        var key = Console.ReadKey();
        Console.WriteLine();

        return key.Key switch
        {
            ConsoleKey.Y => true,
            ConsoleKey.Enter => true,
            ConsoleKey.N => false,
            _ => false
        };
    }
}