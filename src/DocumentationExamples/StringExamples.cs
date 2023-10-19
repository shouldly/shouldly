using Shouldly;
using Xunit;
using Xunit.Abstractions;

public class StringExamples
{
    private readonly ITestOutputHelper _testOutputHelper;

    public StringExamples(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ShouldBe()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer";
                target.ShouldBe("Bart");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotBe()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Bart";
                target.ShouldNotBe("Bart");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldEndWith()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer";
                target.ShouldEndWith("Bart");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotEndWith()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer Simpson";
                target.ShouldNotEndWith("Simpson");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldStartWith()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer";
                target.ShouldStartWith("Bart");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotStartWith()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer Simpson";
                target.ShouldNotStartWith("Homer");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldContain()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer";
                target.ShouldContain("Bart");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotBeNullOrEmpty()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "";
                target.ShouldNotBeNullOrEmpty();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotBeEmpty()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "";
                target.ShouldNotBeEmpty();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotBeNull()
    {
        DocExampleWriter.Document(
            () =>
            {
                string? target = null;
                target.ShouldNotBeNull();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldBeNull()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer";
                target.ShouldBeNull();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotMatch()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer Simpson";
                target.ShouldNotMatch("Homer .*");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldMatch()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer Simpson";
                target.ShouldMatch("Bart .*");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldBeNullOrEmpty()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer";
                target.ShouldBeNullOrEmpty();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldBeEmpty()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer";
                target.ShouldBeEmpty();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldContainWithoutWhitespace()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer Simpson";
                target.ShouldContainWithoutWhitespace(" Bart Simpson ");
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotContain()
    {
        DocExampleWriter.Document(
            () =>
            {
                var target = "Homer";
                target.ShouldNotContain("Home");
            },
            _testOutputHelper);
    }
}