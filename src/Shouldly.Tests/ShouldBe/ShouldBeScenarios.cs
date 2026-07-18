namespace Shouldly.Tests.ShouldBe;

public class ShouldBeScenarios
{
    [Fact]
    [UseCulture("en-US")]
    public void BoolFailure()
    {
        const bool myValue = false;
        Verify.ShouldFail(() =>
            myValue.ShouldBe(true, "Some additional context"));
    }

    [Fact]
    [UseCulture("en-US")]
    public void BoxedComparableFailureScenario()
    {
        object a = 0;
        object b = 0.1;
        Verify.ShouldFail(() =>
            a.ShouldBe(b, "Some additional context"));
    }

    [Fact]
    public void BoxedIntComparableFailureScenario()
    {
        Verify.ShouldFail(() =>
            ((object)12).ShouldBe("string", "Some additional context"));
    }

    [Fact]
    public void BadEquatableClassScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new BadEquatable().ShouldBe(new(), "Some additional context"));
    }

    [Fact]
    public void ComparingBaseWithDerivedTypeShouldFail()
    {
        Verify.ShouldFail(() =>
            new MyBase().ShouldBe(new MyThing(), "Some additional context"));
    }

    [Fact]
    public void ComparingObjectWithStringFailure()
    {
        Verify.ShouldFail(() =>
            new object().ShouldBe("this string", "Some additional context"));
    }

    [Fact]
    public void ActualIsNullScenario()
    {
        string? nullString = null;

        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
            nullString.ShouldBe(string.Empty, "Some additional context"));
    }

    [Fact]
    public void EqualsShouldBeCalledOnExpectedScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new BaseClass().ShouldBe(new SubclassWithStubbedEquals { EqualsResult = false }, "Some additional context"));
    }

    [Fact]
    public void IntegerShouldFailScenario()
    {
        const int two = 2;
        Verify.ShouldFail(() =>
            two.ShouldBe(1, "Some additional context"));
    }

    /// <summary>
    /// Strange emulates JToken, a class which can be implicitly cast to from a string which is IEnumerable,
    /// but enumerable can be empty which means we get a false pass.
    ///
    /// To make this test pass, for types like JToken and Strange we have to use .Equals not compare them as Enumerables
    /// </summary>
    [Fact]
    public void JTokenScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new Strange().ShouldBe("string", "Some additional context"));
    }

    [Fact]
    public void NumbersOfDifferentTypesScenarioShouldFail()
    {
        const long aLong = 2L;
        Verify.ShouldFail(() =>
            aLong.ShouldBe(1, "Some additional context"));
    }

    [Fact]
    public void UncomparableClassScenario()
    {
        Verify.ShouldFail(() =>
            new UncomparableClass("ted").ShouldBe(new("bob"), "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            ThisString.ShouldBe(ThisOtherString, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        true.ShouldBe(true);
        var instance = new BadEquatable();
        instance.ShouldBe(instance);

        new BaseClass().ShouldBe(new SubclassWithStubbedEquals { EqualsResult = true });

        1.ShouldBe(1);

        ((Strange)"string").ShouldBe("string");

        1L.ShouldBe(1);
    }

    [Fact]
    public void ShouldBeBoxedPass()
    {
        object a = 0;
        object b = 0.0;
        a.ShouldBe(b);
    }

    [Fact]
    public void ComparisonEqualsFalseShouldFail()
    {
        var comparison1 = new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" };
        var comparison2 = new ComparableClass { Property = "Cat", IgnoredProperty = "Ant" };

        Verify.ShouldFail(() =>
            comparison1.ShouldBe(comparison2, new ComparableClassComparer(), "Some additional context"));
    }

    [Fact]
    public void ComparisonEqualsTrueShouldPass()
    {
        var comparison1 = new ComparableClass { Property = "Elephant", IgnoredProperty = "Duck" };
        var comparison2 = new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" };

        comparison1.ShouldBe(comparison2, new ComparableClassComparer());
    }

    public class BadEquatable : IEquatable<BadEquatable>
    {
        public bool Equals(BadEquatable? other) => false;
    }

    public class BaseClass
    {
    }

    public class SubclassWithStubbedEquals : BaseClass
    {
        public bool EqualsResult { private get; set; }

        // ReSharper disable once CSharpWarnings::CS0659
        public override bool Equals(object? obj) => EqualsResult;

        public override int GetHashCode() =>
            // Just to stop build warning
            0;
    }

    private const string ThisOtherString = "this other string";
    private const string ThisString = "this string";
}