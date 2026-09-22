using System.Linq.Expressions;

namespace Shouldly.Tests.ShouldBeNullOrEmpty;

public class EnumerableScenario
{
    [Fact]
    public void PassesForNull()
    {
        IEnumerable<int>? actual = null;
        actual.ShouldBeNullOrEmpty();
    }

    [Fact]
    public void PassesForEmpty()
    {
        Array.Empty<int>().ShouldBeNullOrEmpty();
    }

    [Fact]
    public void FailsForNonEmpty()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2, 3 }.ShouldBeNullOrEmpty("Some additional context"));
    }

    // string is IEnumerable<char>, so guard that the more specific string overload still wins.
    // Both overloads behave the same at runtime, so check the compiler's binding via an expression tree.
    [Fact]
    public void StringStillBindsToStringOverload()
    {
        Expression<Action> call = () => "".ShouldBeNullOrEmpty(null, null);
        ((MethodCallExpression)call.Body).Method.DeclaringType.ShouldBe(typeof(ShouldBeStringTestExtensions));
    }
}
