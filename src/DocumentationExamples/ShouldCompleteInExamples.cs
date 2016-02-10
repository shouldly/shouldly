using System;
using System.Threading;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldCompleteInExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public ShouldCompleteInExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldCompleteIn()
        {
            DocExampleWriter.Document(() =>
            {
                Should.Throw<TimeoutException>(() => 
                    Should.CompleteIn(() => Thread.Sleep(TimeSpan.FromSeconds(2)), TimeSpan.FromSeconds(1), "Some additional context"));
            }, _testOutputHelper);
        }
    }
}
