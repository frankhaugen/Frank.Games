namespace BlackjackGPT;

public class Card
{
    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public Suit Suit { get; }
    public Rank Rank { get; }

    public int Value => Rank switch
    {
        > Rank.Ten => 10,
        Rank.Ace => 11,
        _ => (int)Rank
    };
}