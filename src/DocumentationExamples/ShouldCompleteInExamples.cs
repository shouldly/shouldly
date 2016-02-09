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
                var homer = new Person() { Name = "Homer", Salary = 30000 };
                var denominator = 1;
                Should.CompleteIn(() =>
                {
                    Thread.Sleep(2000);
                    var y = homer.Salary / denominator;
                }, TimeSpan.FromSeconds(1));
            }, _testOutputHelper);
        }
    }
}
