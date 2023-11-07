
using System.Linq;

namespace Blackjack.Records;

public record Score
{
    private List<Card> PlayerCards = new();
    private List<Card> DealerCards = new();
    public int PlayerScore { get; set; }
    public int DealerScore { get; set; }
    public string Winner {  get; set; } = string.Empty;
    public Score(List<Card> playerCards, List<Card> dealerCards, int playerScore, int dealerScore, string winner) =>
        (PlayerCards, DealerCards, PlayerScore, DealerScore, Winner) = (playerCards, dealerCards, playerScore, dealerScore, winner);

    public string GetPlayerCards() =>
        string.Join(" ", PlayerCards.Select(c => c.Symbol));
    public string GetDealerCards() =>
        string.Join(" ", DealerCards.Select(c => c.Symbol));
}
