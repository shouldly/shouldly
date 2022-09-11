namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class ObjectScenario
{
    [Fact]
    public void ShouldPassWhenReferencesAreEqual()
    {
        var subject = new FakeObject();
        var expected = subject;
        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldFailWhenIdDoesNotMatch()
    {
        var subject = new FakeObject { Id = 5, Name = "Bob" };
        var expected = new FakeObject { Id = 6, Name = "Bob" };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            @"Comparing object equivalence, at path:
subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    Id [System.Int32]

    Expected value to be
6
    but was
5

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Comparing object equivalence, at path:
<root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    Id [System.Int32]

    Expected value to be
6
    but was
5

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldFailWhenNameDoesNotMatch()
    {
        var subject = new FakeObject { Id = 5, Name = "Bob" };
        var expected = new FakeObject { Id = 5, Name = "Sally" };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            @"Comparing object equivalence, at path:
subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    Name [System.String]

    Expected value to be
""Sally""
    but was
""Bob""

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Comparing object equivalence, at path:
<root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    Name [System.String]

    Expected value to be
""Sally""
    but was
""Bob""

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldFailWhenFieldDoesNotMatch()
    {
        var subject = new FakeObject
        {
            Id = 5,
            TitleField = "Mr",
            Name = "Bob",
        };
        var expected = new FakeObject
        {
            Id = 5,
            TitleField = "Sir",
            Name = "Bob",
        };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            @"Comparing object equivalence, at path:
subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    TitleField [System.String]

    Expected value to be
""Sir""
    but was
""Mr""

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Comparing object equivalence, at path:
<root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    TitleField [System.String]

    Expected value to be
""Sir""
    but was
""Mr""

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldFailWhenObjectIsComplex()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = new[] { "red", "blue" },
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "ugly", "intelligent" },
                Colors = new[] { "purple", "orange" }
            }
        };

        var expected = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = new[] { "red", "blue" },
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = new[] { "purple", "orange" }
            }
        };

        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            @"Comparing object equivalence, at path:
subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    Child [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
        Adjectives [System.String[]]
            Element [0] [System.String]

    Expected value to be
""beautiful""
    but was
""ugly""

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Comparing object equivalence, at path:
<root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    Child [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
        Adjectives [System.String[]]
            Element [0] [System.String]

    Expected value to be
""beautiful""
    but was
""ugly""

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldFailWhenObjectContainsInfiniteLoop()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = new[] { "red", "blue" },
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = new[] { "purple", "orange" }
            }
        };
        subject.Child.Child = subject;

        var expected = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = new[] { "red", "blue" },
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "dumb" },
                Colors = new[] { "purple", "orange" }
            }
        };
        expected.Child.Child = expected;

        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            @"Comparing object equivalence, at path:
subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    Child [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
        Adjectives [System.String[]]
            Element [1] [System.String]

    Expected value to be
""dumb""
    but was
""intelligent""

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Comparing object equivalence, at path:
<root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
    Child [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
        Adjectives [System.String[]]
            Element [1] [System.String]

    Expected value to be
""dumb""
    but was
""intelligent""

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        const string subject = "Hello";
        subject.ShouldBeEquivalentTo("Hello");
    }

    [Fact]
    public void ShouldPassWhenObjectIsComplex()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = new[] { "red", "blue" },
            TitleField = "Mr",
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = new[] { "purple", "orange" }
            }
        };

        var expected = new FakeObject
        {
            Id = 5,
            TitleField = "Mr",
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = new[] { "red", "blue" },
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = new[] { "purple", "orange" }
            }
        };

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldPassWhenObjectContainsInfiniteLoop()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = new[] { "red", "blue" }
        };
        subject.Child = subject;

        var expected = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = new[] { "red", "blue" }
        };
        expected.Child = expected;

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldThrowSensibleErrorWhenIndexersUsed()
    {
        var subject = new IndexableObject(new List<string> { "foo", "bar" });
        var expected = new IndexableObject(new List<string> { "a", "b" });
        var indexableObjectComparison = () => subject.ShouldBeEquivalentTo(expected);

        indexableObjectComparison
            .ShouldThrow<NotSupportedException>()
            .Message
            .ShouldBe("Comparing types that have indexers is not supported.");
    }
}