
using Blackjack.Records;

namespace BlackJack.Classes;

public class Blackjack
{
    private Player player;
    private Dealer dealer;
    private Deck deck = new();
    private EventHandler<Score>? PublishScore;
    public bool Stays { get => player.Stays; }
    public string Winner { get; private set; } = string.Empty;
    public Blackjack()
    {
        player = new(Stay);
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
        Winner = RuleEngine.DetermineWinnerRules.Evaluate(player, dealer);
        Publish();
    }
    public void NewGame()
    {
        Winner = string.Empty;
        deck.NewDeck();

        player = new(Stay);
        dealer = new();

        DealDealerCard(2, true);
        DealPlayerCard(2);
    }
    public void Subscribe(EventHandler<Score> subscriptionMethod) =>
        PublishScore += subscriptionMethod;
    public void Unsubscribe(EventHandler<Score> subscriptionMethod) =>
        PublishScore -= subscriptionMethod;
    private void Publish()
    {
        if (PublishScore is not null) 
        {
            Score score = new(player.Cards, dealer.Cards, player.Score, dealer.Score, Winner);
            PublishScore(this, score);
        }
    }

}
