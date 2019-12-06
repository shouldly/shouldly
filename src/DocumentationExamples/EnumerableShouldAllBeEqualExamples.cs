using System.Collections.Generic;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class EnumerableShouldAllBeEqualExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public EnumerableShouldAllBeEqualExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldAllBeEqual()
        {
            DocExampleWriter.Document(() =>
            {
                var lisa = new Person() { Name = "Lisa" };
                var bart = new Person() { Name = "Bart" };
                var maggie = new Person() { Name = "Maggie" };
                var simpsonsKids = new List<Person>() { bart, lisa, maggie, maggie };
                simpsonsKids.ShouldAllBeEqual();
            }, _testOutputHelper);
        }
    }
}
