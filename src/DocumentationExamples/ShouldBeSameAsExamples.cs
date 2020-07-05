using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldBeSameAsExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public ShouldBeSameAsExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldBeSameAs()
        {
            DocExampleWriter.Document(() =>
            {
                var principleSkinner = new Person { Name = "Armin Tamzarian"};
                var symourSkinner = new Person { Name = "Seymour Skinner" };

                principleSkinner.ShouldBeSameAs(symourSkinner);
            }, _testOutputHelper);
        }

    }
}
