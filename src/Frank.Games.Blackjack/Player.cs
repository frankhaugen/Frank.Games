namespace BlackjackGPT;

public abstract class Player
{
    protected readonly Deck _deck;

    protected Player(Deck deck)
    {
        _deck = deck;
    }

    public List<Card> Hand { get; } = new();

    public bool IsBust => Score > 21;

    public bool IsBlackjack => Score == 21;

    public int Score => Hand.Sum(c => c.Value);

    public void TakeCard()
    {
        Hand.Add(_deck.DrawCard());
    }

    public abstract bool WantsToHit();

    public virtual void PlayTurn()
    {
        while (WantsToHit())
        {
            TakeCard();
            if (IsBust)
                // Bust
                break;
            if (IsBlackjack)
                // Blackjack
                break;
        }
    }

    public void Reset()
    {
        Hand.Clear();
    }

    public void PrintHand()
    {
        Console.WriteLine($"Score: {Score}");
        foreach (var card in Hand) Console.WriteLine($"{card.Rank} of {card.Suit}");
    }
}