namespace Shouldly.Tests.ShouldBe;

public class ShouldBeScenarios
{
    [Fact]
    [UseCulture("en-US")]
    public void BoolFailure()
    {
        const bool myValue = false;
        Verify.ShouldFail(() =>
                myValue.ShouldBe(true, "Some additional context"),

            errorWithSource:
            @"myValue
    should be
True
    but was
False

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"False
    should be
True
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    [UseCulture("en-US")]
    public void BoxedComparableFailureScenario()
    {
        object a = 0;
        object b = 0.1;
        Verify.ShouldFail(() =>
                a.ShouldBe(b, "Some additional context"),

            errorWithSource:
            @"a
    should be
0.1d
    but was
0

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"0
    should be
0.1d
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void BoxedIntComparableFailureScenario()
    {
        Verify.ShouldFail(() =>
                ((object)12).ShouldBe("string", "Some additional context"),

            errorWithSource:
            @"(object)12
    should be
""string""
    but was
12

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"12
    should be
""string""
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void BadEquatableClassScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new BadEquatable().ShouldBe(new(), "Some additional context"),

            errorWithSource:
            @"new BadEquatable()
    should be
Shouldly.Tests.ShouldBe.ShouldBeScenarios+BadEquatable (000000)
    but was
Shouldly.Tests.ShouldBe.ShouldBeScenarios+BadEquatable (000000)

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Shouldly.Tests.ShouldBe.ShouldBeScenarios+BadEquatable (000000)
    should be
Shouldly.Tests.ShouldBe.ShouldBeScenarios+BadEquatable (000000)
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ComparingBaseWithDerivedTypeShouldFail()
    {
        Verify.ShouldFail(() =>
                new MyBase().ShouldBe(new MyThing(), "Some additional context"),

            errorWithSource:
            @"new MyBase()
    should be
Shouldly.Tests.TestHelpers.MyThing (000000)
    but was
Shouldly.Tests.TestHelpers.MyBase (000000)

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Shouldly.Tests.TestHelpers.MyBase (000000)
    should be
Shouldly.Tests.TestHelpers.MyThing (000000)
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ComparingObjectWithStringFailure()
    {
        Verify.ShouldFail(() =>
                new object().ShouldBe("this string", "Some additional context"),

            errorWithSource:
            @"new object()
    should be
""this string""
    but was
System.Object (000000)

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"System.Object (000000)
    should be
""this string""
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ActualIsNullScenario()
    {
        string? nullString = null;

        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
                nullString.ShouldBe(string.Empty, "Some additional context"),

            errorWithSource:
            @"nullString
    should be
""""
    but was
null

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"null
    should be
""""
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void EqualsShouldBeCalledOnExpectedScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new BaseClass().ShouldBe(new SubclassWithStubbedEquals { EqualsResult = false }, "Some additional context"),

            errorWithSource:
            @"new BaseClass()
    should be
Shouldly.Tests.ShouldBe.ShouldBeScenarios+SubclassWithStubbedEquals (0)
    but was
Shouldly.Tests.ShouldBe.ShouldBeScenarios+BaseClass (000000)

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Shouldly.Tests.ShouldBe.ShouldBeScenarios+BaseClass (000000)
    should be
Shouldly.Tests.ShouldBe.ShouldBeScenarios+SubclassWithStubbedEquals (0)
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void IntegerShouldFailScenario()
    {
        const int two = 2;
        Verify.ShouldFail(() =>
                two.ShouldBe(1, "Some additional context"),

            errorWithSource:
            @"two
    should be
1
    but was
2

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"2
    should be
1
    but was not

Additional Info:
    Some additional context");
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
                new Strange().ShouldBe("string", "Some additional context"),

            errorWithSource:
            @"new Strange()
    should be
[] (string)
    but was
[] (null)
    difference
[]

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[] (null)
    should be
[] (string)
    but was not
    difference
[]

Additional Info:
    Some additional context");
    }

    [Fact]
    public void NumbersOfDifferentTypesScenarioShouldFail()
    {
        const long aLong = 2L;
        Verify.ShouldFail(() =>
                aLong.ShouldBe(1, "Some additional context"),

            errorWithSource:
            @"aLong
    should be
1L
    but was
2L

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"2L
    should be
1L
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void UncomparableClassScenario()
    {
        Verify.ShouldFail(() =>
                new UncomparableClass("ted").ShouldBe(new("bob"), "Some additional context"),

            errorWithSource:
            @"new UncomparableClass(""ted"")
    should be
bob
    but was
ted

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"ted
    should be
bob
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void StringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                ThisString.ShouldBe(ThisOtherString, "Some additional context"),

            errorWithSource:
            @"ThisString
    should be
""this other string""
    but was
""this string""
    difference
Difference     |                           |         |    |    |    |    |    |    |    |    |    |   
               |                          \|/       \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   
Expected Value | t    h    i    s    \s   o    t    h    e    r    \s   s    t    r    i    n    g    
Actual Value   | t    h    i    s    \s   s    t    r    i    n    g                                  
Expected Code  | 116  104  105  115  32   111  116  104  101  114  32   115  116  114  105  110  103  
Actual Code    | 116  104  105  115  32   115  116  114  105  110  103                                

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"""this string""
    should be
""this other string""
    but was not
    difference
Difference     |                           |         |    |    |    |    |    |    |    |    |    |   
               |                          \|/       \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   
Expected Value | t    h    i    s    \s   o    t    h    e    r    \s   s    t    r    i    n    g    
Actual Value   | t    h    i    s    \s   s    t    r    i    n    g                                  
Expected Code  | 116  104  105  115  32   111  116  104  101  114  32   115  116  114  105  110  103  
Actual Code    | 116  104  105  115  32   115  116  114  105  110  103                                

Additional Info:
    Some additional context");
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
                comparison1.ShouldBe(comparison2, new ComparableClassComparer(), "Some additional context"),

            errorWithSource:
            @"comparison1
    should be
Shouldly.Tests.TestHelpers.ComparableClass (000000)
    but was
Shouldly.Tests.TestHelpers.ComparableClass (000000)

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Shouldly.Tests.TestHelpers.ComparableClass (000000)
    should be
Shouldly.Tests.TestHelpers.ComparableClass (000000)
    but was not

Additional Info:
    Some additional context");
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
        public bool Equals(BadEquatable? other)
        {
            return false;
        }
    }

    public class BaseClass
    {
    }

    public class SubclassWithStubbedEquals : BaseClass
    {
        public bool EqualsResult { private get; set; }

        // ReSharper disable once CSharpWarnings::CS0659
        public override bool Equals(object? obj)
        {
            return EqualsResult;
        }

        public override int GetHashCode()
        {
            // Just to stop build warning
            return 0;
        }
    }

    private const string ThisOtherString = "this other string";
    private const string ThisString = "this string";
}