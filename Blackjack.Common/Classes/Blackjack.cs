
namespace BlackJack.Classes;

public class Blackjack
{
    private Player player;
    private Dealer dealer;
    private Deck deck = new();
    public bool Stays { get => player.Stays; }
    public string Winner { get; private set; } = string.Empty;
    public Blackjack()
    {
        player = new(this);
        dealer = new();
    }
    public List<Card> GetPlayerCards() => player.Cards;
    public int GetPlayerScore() => player.Score;
    public List<Card> GetDealerCards() => dealer.Cards;
    public int GetDealerScore() => dealer.Score;
    public void DealPlayerCard(int takeCards = 1) => player.AddCard(deck.DealCard(takeCards));
    public void DealDealerCard(int takeCards = 1, bool firstCards = false)
    {
        List<Card> cards = deck.DealCard(takeCards);
        if (firstCards)
        {
            cards[0].IsHidden = true;
        }
        dealer.AddCard(cards);
    }
    public void Stay()
    {
        player.Stays = true;
        dealer.Stays = true;

        if (!RuleEngine.BlackjackAndBustHandRules.Evaulate(player))
        {
            dealer.Cards.First().IsHidden = false;
            dealer.CalculateScore();
            while (dealer.Score < 17)
            {
                DealDealerCard();
            }
        }

        DetermineWinner();
    }
    void DetermineWinner()
    {
        if (RuleEngine.BlackjackAndBustHandRules.Evaulate(player))
        {
            dealer.Cards.First().IsHidden = true;
            dealer.Score = dealer.Cards[1].Value;
        }

        if (player.Result.Equals(Results.Blackjack))
        {
            Winner = "Player wins with Blackjack";
        }
        else if (player.Result.Equals(Results.Bust))
        {
            Winner = "Dealer wins";
        }
        else if (dealer.Result.Equals(Results.Bust))
        {
            Winner = "Player wins";
        }
        else if (dealer.Score > player.Score)
        {
            Winner = "Dealer wins";
        }
        else if (player.Score > dealer.Score)
        {
            Winner = "Player wins";
        }
        else
            Winner = "Draw";
    }
    public void NewGame()
    {
        Winner = string.Empty;
        deck.NewDeck();

        player = new(this);
        dealer = new();

        DealDealerCard(2, true);
        DealPlayerCard(2);
    }
}
