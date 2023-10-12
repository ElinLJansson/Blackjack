
using Blackjack.Common.Enums;
using Blackjack.Common.Records;

namespace Blackjack.Common.Classes;

abstract class PlayerBase
{
    public bool Stays { get; set; }
    public List<Card> Cards { get; private set; } = new();
    public int Score { get; set; } = default;
    public Results Result { get; private set; } = Results.Unknown;
    public virtual void AddCard(List<Card> cards)
    {
        Cards.AddRange(cards);
        CalculateScore();
        if (Score > 21) Result = Results.DealerLost;
    }
    public void CalculateScore()
    {
        Score = Cards.Sum(c => c.Value);
        var aces = Cards.Where(c => c.Value.Equals(1) && !c.IsHidden);

        foreach (var ace in aces)
        {
            if (Score <= 11)
            {
                Score += 10;
            }
        }
    }
    protected void ChangeResult(Results results) => Result = results;
}
