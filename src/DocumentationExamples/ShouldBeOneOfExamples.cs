using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples;

public class ShouldBeOneOfExamples
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ShouldBeOneOfExamples(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ShouldBeOneOf()
    {
        DocExampleWriter.Document(() =>
        {
            var apu = new Person { Name = "Apu" };
            var homer = new Person { Name = "Homer" };
            var skinner = new Person { Name = "Skinner" };
            var barney = new Person { Name = "Barney" };
            var theBeSharps = new List<Person> { homer, skinner, barney };

            apu.ShouldBeOneOf(theBeSharps.ToArray());
        }, _testOutputHelper);
    }

    [Fact]
    public void ShouldNotBeOneOf()
    {
        DocExampleWriter.Document(() =>
        {
            var apu = new Person { Name = "Apu" };
            var homer = new Person { Name = "Homer" };
            var skinner = new Person { Name = "Skinner" };
            var barney = new Person { Name = "Barney" };
            var wiggum = new Person { Name = "Wiggum" };
            var theBeSharps = new List<Person> { apu, homer, skinner, barney, wiggum };

            wiggum.ShouldNotBeOneOf(theBeSharps.ToArray());
        }, _testOutputHelper);
    }
}