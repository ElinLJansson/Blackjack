
namespace BlackJack.Classes;

public static class RuleEngine
{
    public static List<IHandRule> BlackjackAndBustHandRules => new()
    {
        new BlackjackRule(),
        new BustRule()
    };
    public static List<IHandRule> StaysAndBustHandRules => new()
    {
        new StayRule(),
        new BustRule()
    };
    public static bool Evaulate(this IEnumerable<IHandRule> rules, PlayerBase person)
    {
        person.ChangeResult(Results.Unknown);
        foreach (var rule in rules)
        {
            rule.Evaluate(person);
            if (!person.Result.Equals(Results.Unknown)) break;
        }
        return !person.Result.Equals(Results.Unknown);
    }

}
