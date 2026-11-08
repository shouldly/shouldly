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

    [Fact]
    public void FailsForSingleItem()
    {
        var single = new List<string> { "a" };
        Verify.ShouldFail(() =>
            single.ShouldBeNullOrEmpty("Some additional context"));
    }

    [Fact]
    public void FailsForOneShotSequenceWithAllItemsInMessage()
    {
        var oneShot = new OneShotEnumerable<int>(1, 2, 3);
        Verify.ShouldFail(() =>
            oneShot.ShouldBeNullOrEmpty("Some additional context"));
    }

    // string is IEnumerable<char>, so guard that the more specific string overload still wins. Both overloads behave the same at runtime, so check the compiler's binding via an expression tree.
    [Fact]
    public void StringStillBindsToStringOverload()
    {
        Expression<Action> call = () => "".ShouldBeNullOrEmpty(null, null);
        ((MethodCallExpression)call.Body).Method.DeclaringType.ShouldBe(typeof(ShouldBeStringTestExtensions));
    }
}

// Throws if enumerated twice, to prove the assertion reads the source only once.
file sealed class OneShotEnumerable<T>(params T[] items) : IEnumerable<T>
{
    bool _enumerated;

    public IEnumerator<T> GetEnumerator()
    {
        if (_enumerated)
            throw new InvalidOperationException("Already enumerated");
        _enumerated = true;
        return ((IEnumerable<T>)items).GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => nameof(OneShotEnumerable<T>);
}
