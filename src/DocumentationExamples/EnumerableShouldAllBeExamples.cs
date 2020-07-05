using System.Collections.Generic;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class EnumerableShouldAllBeExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public EnumerableShouldAllBeExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldAllBe()
        {
            DocExampleWriter.Document(() =>
            {
                var mrBurns = new Person { Name = "Mr.Burns", Salary = 3000000 };
                var kentBrockman = new Person { Name = "Homer", Salary = 3000000 };
                var homer = new Person { Name = "Homer", Salary = 30000 };
                var millionares = new List<Person> { mrBurns, kentBrockman, homer };

                millionares.ShouldAllBe(m => m.Salary > 1000000);
            }, _testOutputHelper);
        }

    }
}
