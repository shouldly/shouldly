using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples;

public class ShouldBeTrueFalseExamples
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ShouldBeTrueFalseExamples(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ShouldBeTrue()
    {
        DocExampleWriter.Document(
            () =>
            {
                var myValue = false;
                myValue.ShouldBeTrue();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldBeFalse()
    {
        DocExampleWriter.Document(
            () =>
            {
                var myValue = true;
                myValue.ShouldBeFalse();
            },
            _testOutputHelper);
    }
}