namespace Shouldly;

public class AggregateShouldlyAssertionException : Exception
{
    public IReadOnlyList<string> Errors { get; }

    public AggregateShouldlyAssertionException(IEnumerable<string> errors)
        : base("Multiple assertion failures occurred:" + Environment.NewLine + string.Join(Environment.NewLine, errors))
        => Errors = new List<string>(errors);
}