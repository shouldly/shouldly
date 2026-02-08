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