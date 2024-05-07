
namespace BlackJack.Classes;

public class Deck
{
    private Stack<Card> deck = new();
    private string[,] suits =  {{ "🂡", "🂢", "🂣", "🂤", "🂥", "🂦", "🂧", "🂨", "🂩", "🂪" , "🂫", "🂭", "🂮" },
                                { "🂱", "🂲", "🂳", "🂴", "🂵", "🂶", "🂷", "🂸", "🂹", "🂺", "🂻", "🂽", "🂾" },
                                { "🃁", "🃂", "🃃", "🃄", "🃅", "🃆", "🃇", "🃈", "🃉", "🃊", "🃋", "🃍", "🃎" },
                                { "🃑", "🃒", "🃓", "🃔", "🃕", "🃖", "🃗", "🃘", "🃙", "🃚", "🃛", "🃝", "🃞" }};
    //Red Cards
    // { "🂱", "🂲", "🂳", "🂴", "🂵", "🂶", "🂷", "🂸", "🂹", "🂺", "🂻", "🂽", "🂾" }
    // { "🃁", "🃂", "🃃", "🃄", "🃅", "🃆", "🃇", "🃈", "🃉", "🃊", "🃋", "🃍", "🃎" }
    public void NewDeck()
    {
        var cards = CreateDeck().ToList();
        ShuffleDeck(cards);
    }
    public List<Card> DealCard(int takeCards = 1)
    {
        List<Card> cards = new();
        for (int i = 0; i < takeCards; i++)
        {
            try
            {
                cards.Add(deck.Pop());
            }
            catch { }
        }
        return cards;
    }
    private IEnumerable<Card> CreateDeck()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                int value = j > 9 ? 10 : j + 1;
                yield return new Card(suits[i, j], value);
            }
        }
    }
    void ShuffleDeck(List<Card> cards)
    {
        Random random = new();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
            deck.Push(value);
        }
    }

}
