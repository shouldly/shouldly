using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples;

public class EnumerableShouldBeExamples
{
    private readonly ITestOutputHelper _testOutputHelper;

    public EnumerableShouldBeExamples(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ShouldBe()
    {
        DocExampleWriter.Document(
            () =>
            {
                var apu = new Person { Name = "Apu" };
                var homer = new Person { Name = "Homer" };
                var skinner = new Person { Name = "Skinner" };
                var barney = new Person { Name = "Barney" };
                var theBeSharps = new List<Person> { homer, skinner, barney };

                theBeSharps.ShouldBe(new[] { apu, homer, skinner, barney });
            },
            _testOutputHelper);
    }
}