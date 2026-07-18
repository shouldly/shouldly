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
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenNameDoesNotMatch()
    {
        var subject = new FakeObject { Id = 5, Name = "Bob" };
        var expected = new FakeObject { Id = 5, Name = "Sally" };
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
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
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
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
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
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
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
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
    public void ShouldPassWhenComplexObjectContainsPropertiesWithDifferentTypesUsingOptionsOverload()
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

        var options = new EquivalencyOptions();

        subject.ShouldBeEquivalentTo(expected, options);
    }

    [Fact]
    public void ShouldPassWhenComplexObjectContainsPropertiesWithDifferentTypes()
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
    public void ShouldSkipIndexersWhenComparing()
    {
        // Indexers cannot be compared (there is no general way to enumerate their values),
        // so they are skipped rather than throwing.
        var subject = new IndexableObject(new List<string> { "foo", "bar" });
        var expected = new IndexableObject(new List<string> { "a", "b" });

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldReportAllDifferences()
    {
        var subject = new FakeObject { Id = 5, Name = "Bob", TitleField = "Mr" };
        var expected = new FakeObject { Id = 6, Name = "Sally", TitleField = "Mr" };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenExpectationIsAnonymousSubset()
    {
        var subject = new FakeObject { Id = 5, Name = "Bob", TitleField = "Mr" };

        subject.ShouldBeEquivalentTo(new { Id = 5, Name = "Bob" });
    }

    [Fact]
    public void ShouldFailWhenExpectationMemberIsMissingOnActual()
    {
        var subject = new FakeObject { Id = 5, Name = "Bob" };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new { Id = 5, Nickname = "Bobby" }, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenComparisonSelectsNoMembers()
    {
        // A comparison that selects zero members would pass without asserting anything;
        // fail loudly instead of silently weakening the assertion.
        var subject = new object();
        var expected = new object();

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }
}