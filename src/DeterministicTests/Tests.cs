using Shouldly;
using Xunit;

public class Tests
{
    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldMatch(@"C.e{2}s[e]");
    }
}