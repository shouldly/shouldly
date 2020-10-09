using System.Collections.Generic;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class DictionaryShouldContainKeyExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public DictionaryShouldContainKeyExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldContainKey()
        {
            DocExampleWriter.Document(() =>
            {
                var websters = new Dictionary<string, string> { { "Embiggen", "To empower or embolden." } };
                websters.ShouldContainKey("Cromulent");
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldNotContainKey()
        {
            DocExampleWriter.Document(() =>
            {
                var websters = new Dictionary<string, string> { { "Chazzwazzers", "What Australians would have called a bull frog." } };

                websters.ShouldNotContainKey("Chazzwazzers");
            }, _testOutputHelper);
        }
    }
}
