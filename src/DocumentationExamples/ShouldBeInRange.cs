using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldBeInRangeExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public ShouldBeInRangeExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldBeInRange()
        {
            DocExampleWriter.Document(() =>
            {
                var homer = new Person() { Name = "Homer", Salary = 300000000 };
                homer.Salary.ShouldBeInRange(30000, 40000);
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldNotBeInRange()
        {
            DocExampleWriter.Document(() =>
            {
                var mrBurns = new Person() { Name = "Mr. Burns", Salary = 30000 };
                mrBurns.Salary.ShouldNotBeInRange(30000, 40000);
            }, _testOutputHelper);
        }

    }
}
