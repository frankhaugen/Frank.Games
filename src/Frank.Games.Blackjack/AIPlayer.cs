namespace BlackjackGPT;

public class AIPlayer : Player
{
    public AIPlayer(Deck deck) : base(deck)
    {
    }

    public override bool WantsToHit()
    {
        // implementation for checking if AI player wants to hit

        return false;
    }
}