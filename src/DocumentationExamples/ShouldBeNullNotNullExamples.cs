using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldBeNullNotNullExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public ShouldBeNullNotNullExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldBeNull()
        {
            DocExampleWriter.Document(() =>
            {
                var myRef = "Hello World";
                myRef.ShouldBeNull();
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldNotBeNull()
        {
            DocExampleWriter.Document(() =>
            {
                string? myRef = null;
                myRef.ShouldNotBeNull();
            }, _testOutputHelper);
        }
    }
}