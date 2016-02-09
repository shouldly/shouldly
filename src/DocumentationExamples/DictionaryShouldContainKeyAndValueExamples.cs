using System.Collections.Generic;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class DictionaryShouldContainKeyAndValueExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public DictionaryShouldContainKeyAndValueExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldContainKeyAndValue()
        {
            DocExampleWriter.Document(() =>
            {
                var websters = new Dictionary<string, string>();
                websters.Add("Cromulent", "I never heard the word before moving to Springfield.");

                websters.ShouldContainKeyAndValue("Cromulent", "Fine, acceptable.");
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldNotContainKeyAndValue()
        {
            DocExampleWriter.Document(() =>
            {
                var websters = new Dictionary<string, string>();
                websters.Add("Chazzwazzers", "What Australians would have called a bull frog.");

                websters.ShouldNotContainValueForKey("Chazzwazzers",  "What Australians would have called a bull frog.");
            }, _testOutputHelper);
        }
    }
}
