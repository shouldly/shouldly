using System;
using System.Threading;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldCompleteInExamples
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ShouldCompleteInExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldCompleteIn()
        {
            DocExampleWriter.Document(() =>
            {
                Should.CompleteIn(
                    action: () => { Thread.Sleep(TimeSpan.FromSeconds(2)); },
                    timeout: TimeSpan.FromSeconds(1),
                    customMessage: "Some additional context");
            }, _testOutputHelper);
        }
    }
}