using System.Collections.Generic;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class EnumerableShouldBeEqualExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public EnumerableShouldBeEqualExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldBeEqual()
        {
            DocExampleWriter.Document(() =>
            {
                var lisa = new Person() { Name = "Lisa" };
                var bart = new Person() { Name = "Bart" };
                var maggie = new Person() { Name = "Maggie" };
                var simpsonsKids = new List<Person>() { maggie, maggie, maggie, maggie};

                simpsonsKids.ShouldAllBeEqual();
            }, _testOutputHelper);
        }
    }
}
