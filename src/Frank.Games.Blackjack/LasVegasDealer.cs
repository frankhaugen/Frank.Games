namespace BlackjackGPT;

public class LasVegasDealer : Player
{
    public LasVegasDealer(Deck deck) : base(deck)
    {
    }

    public override bool WantsToHit()
    {
        if (Score >= 17) return false;
        
        Console.WriteLine("Dealer hits.");
        return true;
    }
}