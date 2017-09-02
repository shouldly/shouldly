using System;
using System.Threading;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldSatisfyAnyConditionsExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public ShouldSatisfyAnyConditionsExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldSatisfyAnyConditions()
        {
            DocExampleWriter.Document(() =>
            {
                var mrBurns = new Person() { Name = null };
                mrBurns.ShouldSatisfyAnyConditions
                    (
                        () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                        () => mrBurns.Name.ShouldBe("Mr.Burns")
                    );
            }, _testOutputHelper);
        }
    }
}
