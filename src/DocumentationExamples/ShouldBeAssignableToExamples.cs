using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples;

public class ShouldBeAssignableToExamples
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ShouldBeAssignableToExamples(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ShouldBeAssignableTo()
    {
        DocExampleWriter.Document(() =>
        {
            var theSimpsonsDog = new Person { Name = "Santas little helper" };
            theSimpsonsDog.ShouldBeAssignableTo<Pet>();
        }, _testOutputHelper);
    }
}