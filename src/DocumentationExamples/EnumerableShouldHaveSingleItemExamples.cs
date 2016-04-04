using System.Collections.Generic;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class EnumerableShouldHaveSingleItemExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public EnumerableShouldHaveSingleItemExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldHaveSingleItem()
        {
            DocExampleWriter.Document(() =>
            {
                var maggie = new Person() { Name = "Maggie" };
                var homer = new Person() { Name = "Homer" };
                var simpsonsBabies = new List<Person>() { homer, maggie };
                simpsonsBabies.ShouldHaveSingleItem();
            }, _testOutputHelper);
        }
    }
}
