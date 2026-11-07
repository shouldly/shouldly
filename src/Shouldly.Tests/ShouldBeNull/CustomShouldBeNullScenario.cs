namespace Shouldly.Tests.ShouldBeNull;

public class CustomShouldBeNullScenario
{
    [Fact]
    public void ShouldFailShowingActualValue()
    {
        var value = new OptionalValue(42);
        Verify.ShouldFail(() =>
            value.ShouldBeNull("Some additional context"));
    }

    [Fact]
    public void ShouldPassForNullValue()
    {
        OptionalValue.Null.ShouldBeNull();
    }
}

public readonly struct OptionalValue(int? value)
{
    public static OptionalValue Null => new(null);

    public bool IsNull => value == null;

    public override string ToString() => value == null ? "OptionalValue(null)" : $"OptionalValue({value})";
}

[ShouldlyMethods]
public static class OptionalValueShouldExtensions
{
    public static void ShouldBeNull(this OptionalValue actual, string? customMessage = null,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => v.IsNull, actual, OptionalValue.Null, customMessage, actualExpression: actualExpression);
    }
}
