using System.Collections.Generic;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class EnumerableShouldBeSubsetOfExamples
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public EnumerableShouldBeSubsetOfExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldBeSubsetOf()
        {
            DocExampleWriter.Document(() =>
            {
                var lisa = new Person { Name = "Lisa" };
                var bart = new Person { Name = "Bart" };
                var maggie = new Person { Name = "Maggie" };
                var homer = new Person { Name = "Homer" };
                var marge = new Person { Name = "Marge" };
                var ralph = new Person { Name = "Ralph" };
                var simpsonsKids = new List<Person> { bart, lisa, maggie, ralph };
                var simpsonsFamily = new List<Person> { lisa, bart, maggie, homer, marge };

                simpsonsKids.ShouldBeSubsetOf(simpsonsFamily);
            }, _testOutputHelper);
        }
    }
}
