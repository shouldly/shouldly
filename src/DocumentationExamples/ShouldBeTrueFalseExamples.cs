using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldBeTrueFalseExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public ShouldBeTrueFalseExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldBeTrue()
        {
            DocExampleWriter.Document(() =>
            {
                bool myValue = false;
                myValue.ShouldBeTrue();
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldBeFalse()
        {
            DocExampleWriter.Document(() =>
            {
                bool myValue = true;
                myValue.ShouldBeFalse();
            }, _testOutputHelper);
        }

    }
}