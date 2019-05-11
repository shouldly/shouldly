using System;
using System.Threading;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldSatisfyAllConditionsExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public ShouldSatisfyAllConditionsExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldSatisfyAllConditions()
        {
            DocExampleWriter.Document(() =>
            {
                var mrBurns = new Person() { Name = null };
                mrBurns.ShouldSatisfyAllConditions
                    (
                        () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                        () => mrBurns.Name.ShouldBe("Mr.Burns")
                    );
            }, _testOutputHelper);
        }

        [Fact]
        public void GenericShouldSatisfyAllConditions()
        {
            DocExampleWriter.Document(() =>
            {
              var mrBurns = new Person() { Name = null };
              mrBurns.ShouldSatisfyAllConditions
                  (
                      mrB => mrB.Name.ShouldNotBeNullOrEmpty(),
                      mrB => mrB.Name.ShouldBe("Mr.Burns")
                  );
            },
            _testOutputHelper);
        }
    }
}
