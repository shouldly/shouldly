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
            """
            Comparing object equivalence, at path:
            subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Id [System.Int32]
            
                Expected value to be
            6
                but was
            5

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Id [System.Int32]
            
                Expected value to be
            6
                but was
            5

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldFailWhenNameDoesNotMatch()
    {
        var subject = new FakeObject { Id = 5, Name = "Bob" };
        var expected = new FakeObject { Id = 5, Name = "Sally" };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Name [System.String]
            
                Expected value to be
            "Sally"
                but was
            "Bob"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Name [System.String]
            
                Expected value to be
            "Sally"
                but was
            "Bob"

            Additional Info:
                Some additional context
            """);
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
            """
            Comparing object equivalence, at path:
            subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                TitleField [System.String]
            
                Expected value to be
            "Sir"
                but was
            "Mr"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                TitleField [System.String]
            
                Expected value to be
            "Sir"
                but was
            "Mr"

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldFailWhenObjectIsComplex()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = ["red", "blue"],
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "ugly", "intelligent" },
                Colors = ["purple", "orange"]
            }
        };

        var expected = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = ["red", "blue"],
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = ["purple", "orange"]
            }
        };

        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Child [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                    Adjectives [System.Collections.IEnumerable]
                        Element [0] [System.String]
            
                Expected value to be
            "beautiful"
                but was
            "ugly"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Child [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                    Adjectives [System.Collections.IEnumerable]
                        Element [0] [System.String]
            
                Expected value to be
            "beautiful"
                but was
            "ugly"

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldFailWhenObjectContainsInfiniteLoop()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = ["red", "blue"],
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = ["purple", "orange"]
            }
        };
        subject.Child.Child = subject;

        var expected = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = ["red", "blue"],
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "dumb" },
                Colors = ["purple", "orange"]
            }
        };
        expected.Child.Child = expected;

        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Child [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                    Adjectives [System.Collections.IEnumerable]
                        Element [1] [System.String]
            
                Expected value to be
            "dumb"
                but was
            "intelligent"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Child [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                    Adjectives [System.Collections.IEnumerable]
                        Element [1] [System.String]
            
                Expected value to be
            "dumb"
                but was
            "intelligent"

            Additional Info:
                Some additional context
            """);
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
            Colors = ["red", "blue"],
            TitleField = "Mr",
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = ["purple", "orange"]
            }
        };

        var expected = new FakeObject
        {
            Id = 5,
            TitleField = "Mr",
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = ["red", "blue"],
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = ["purple", "orange"]
            }
        };

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldPassWhenComplexObjectContainsPropertiesWithDifferentTypesAndCompareUsingRuntimeIsFalse()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = ["red", "blue"],
            TitleField = "Mr",
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = ["purple", "orange"]
            }
        };

        var expected = new FakeObject
        {
            Id = 5,
            TitleField = "Mr",
            Name = "Bob",
            Adjectives = new List<string> { "funny", "wise" }.Where(_ => true),
            Colors = new [] {"red", "blue"}.AsReadOnly(),
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new List<string> { "beautiful", "intelligent" },
                Colors = ["purple", "orange"]
            }
        };

        var options = new EquivalencyOptions { CompareUsingRuntimeTypes = false };

        subject.ShouldBeEquivalentTo(expected, options);
    }

    [Fact]
    public void ShouldPassWhenComplexObjectContainsPropertiesWithDifferentTypesAndUsingDefaultOptions()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = ["red", "blue"],
            TitleField = "Mr",
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new[] { "beautiful", "intelligent" },
                Colors = ["purple", "orange"]
            }
        };

        var expected = new FakeObject
        {
            Id = 5,
            TitleField = "Mr",
            Name = "Bob",
            Adjectives = new List<string> { "funny", "wise" }.Where(_ => true),
            Colors = new [] {"red", "blue"}.AsReadOnly(),
            Child = new()
            {
                Id = 6,
                Name = "Sally",
                Adjectives = new List<string> { "beautiful", "intelligent" },
                Colors = ["purple", "orange"]
            }
        };

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldFailWhenComplexObjectContainsPropertiesWithDifferentTypesAndCompareUsingRuntimeIsTrue()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" }
        };

        var expected = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new List<string> { "funny", "wise" }
        };

        var options = new EquivalencyOptions { CompareUsingRuntimeTypes = true };

        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, options, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Adjectives

                Expected value to be
            System.Collections.Generic.List`1[System.String]
                but was
            System.String[]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [Shouldly.Tests.ShouldBeEquivalentTo.FakeObject]
                Adjectives

                Expected value to be
            System.Collections.Generic.List`1[System.String]
                but was
            System.String[]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPassWhenObjectContainsInfiniteLoop()
    {
        var subject = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = ["red", "blue"]
        };
        subject.Child = subject;

        var expected = new FakeObject
        {
            Id = 5,
            Name = "Bob",
            Adjectives = new[] { "funny", "wise" },
            Colors = ["red", "blue"]
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