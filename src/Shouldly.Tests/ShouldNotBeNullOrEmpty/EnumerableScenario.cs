using System.Linq.Expressions;

namespace Shouldly.Tests.ShouldNotBeNullOrEmpty;

public class EnumerableScenario
{
    [Fact]
    public void PassesForNonEmpty()
    {
        new[] { 1, 2, 3 }.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void FailsForNull()
    {
        IEnumerable<int>? actual = null;
        Verify.ShouldFail(() =>
            actual.ShouldNotBeNullOrEmpty("Some additional context"));
    }

    [Fact]
    public void FailsForEmpty()
    {
        Verify.ShouldFail(() =>
            Array.Empty<int>().ShouldNotBeNullOrEmpty("Some additional context"));
    }

    // string is IEnumerable<char>, so guard that the more specific string overload still wins. Both overloads behave the same at runtime, so check the compiler's binding via an expression tree.
    [Fact]
    public void StringStillBindsToStringOverload()
    {
        Expression<Action> call = () => "a".ShouldNotBeNullOrEmpty(null, null);
        ((MethodCallExpression)call.Body).Method.DeclaringType.ShouldBe(typeof(ShouldBeStringTestExtensions));
    }
}
