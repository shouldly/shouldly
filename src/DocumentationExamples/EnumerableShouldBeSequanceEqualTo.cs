using System.Collections.Generic;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class EnumerableShouldBeSequenceEqualToExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public EnumerableShouldBeSequenceEqualToExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldBe()
        {
            DocExampleWriter.Document(() =>
            {
                var apu = new Person() { Name = "Apu" };
                var homer = new Person() { Name = "Homer" };
                var skinner = new Person() { Name = "Skinner" };
                var barney = new Person() { Name = "Barney" };
                var theBeSharps = new List<Person>() { apu, homer, skinner, barney };

                theBeSharps.ShouldBeSequenceEqualTo(new[] { barney, skinner, homer, apu });
            }, _testOutputHelper);
        }

    }
}
