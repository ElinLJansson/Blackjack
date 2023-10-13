
namespace BlackJack.Classes;

public class Player : PlayerBase
{
    Blackjack Game { set; get; }
    public Player(Blackjack game) => Game = game;
    public override void AddCard(List<Card> cards)
    {
        Cards.AddRange(cards);
        CalculateScore();
        if (RuleEngine.BlackjackAndBustHandRules.Evaulate(this)) Game.Stay();
    }
}
