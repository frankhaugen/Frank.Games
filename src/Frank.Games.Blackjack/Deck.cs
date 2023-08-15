namespace BlackjackGPT;

public class Deck
{
    private readonly Stack<Card> _cards = new();

    public Deck()
    {
        CreateDeck();
    }

    private void CreateDeck()
    {
        var deck = Enum.GetValues<Suit>()
            .SelectMany(s => Enum.GetValues<Rank>(), (s, r) => new Card(s, r))
            .ToList();
        ShuffleDeck(deck);
        deck.ForEach(c => _cards.Push(c));
    }

    private void ShuffleDeck(IList<Card> deck)
    {
        var n = deck.Count;
        while (n > 1)
        {
            n--;
            var k = Random.Shared.Next(n + 1);
            (deck[k], deck[n]) = (deck[n], deck[k]);
        }
    }

    public Card DrawCard()
    {
        return _cards.TryPop(out var card) ? card : throw new InvalidOperationException("Deck is empty");
    }
}