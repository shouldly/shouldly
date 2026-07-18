using Shouldly;
using TUnit.Core;

public class Tests
{
    [Test]
    public void SourceExpression_ShouldBeCaptured()
    {
        const bool myValue = false;
        var ex = Should.Throw<ShouldAssertException>(() =>
            myValue.ShouldBe(true));

        // The error message should contain "myValue" as the source expression
        // If source expressions work, it will say "myValue should be True but was False"
        // If they don't work, it will say "False should be True but was not"
        ex.Message.ShouldContain("myValue");
    }

    [Test]
    public void SourceExpression_ShouldBeCaptured_String()
    {
        var cheese = "Cheddar";
        var ex = Should.Throw<ShouldAssertException>(() =>
            cheese.ShouldBe("Brie"));

        // Should contain the variable name "cheese" not just the value "Cheddar"
        ex.Message.ShouldContain("cheese");
    }

    [Test]
    public void ShouldPass()
    {
        "Cheese".ShouldMatch("C.e{2}s[e]");
    }
}
