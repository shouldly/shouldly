using Shouldly;
using Simpsons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class EnumerableShouldHaveValueExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public EnumerableShouldHaveValueExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldHaveValue()
        {
            DocExampleWriter.Document(() =>
            {
                var maggie = new Person() { Name = "Maggie" };
                var homer = new Person() { Name = "Homer" };
                var simpsonsBabies = new List<Person>() { homer, maggie };
                simpsonsBabies.ShouldHaveValue();
            }, _testOutputHelper);
        }
    }
}
