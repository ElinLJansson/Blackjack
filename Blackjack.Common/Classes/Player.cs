
using Blackjack.Common.Enums;
using Blackjack.Common.Records;
using static System.Formats.Asn1.AsnWriter;

namespace Blackjack.Common.Classes;

class Player : PlayerBase
{
    Blackjack Game { set; get; }
    public Player(Blackjack game) => Game = game;
    public override void AddCard(List<Card> cards)
    {
        Cards.AddRange(cards);
        CalculateScore();
        if (Score == 21 && cards.Count().Equals(2))
        {
            ChangeResult(Results.Blackjack);
            Game.Stay();
        }
        if (Score > 21)
        {
            ChangeResult(Results.PlayerLost);
            Game.Stay();
        }
    }
}
