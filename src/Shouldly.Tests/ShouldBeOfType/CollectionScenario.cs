using System.Collections.Generic;
using System.Linq;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeOfType
{

    public class CollectionScenario
    {
        [Fact]
        public void CollectionScenarioShouldFail()
        {
            var intArray = Enumerable.Empty<int>();
            Verify.ShouldFail(() =>
                    intArray.ShouldBeOfType<IEnumerable<string>>("Some additional context"),

errorWithSource:
@"intArray
    should be of type
System.Collections.Generic.IEnumerable`1[System.String]
    but was
System.Int32[]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[]
    should be of type
System.Collections.Generic.IEnumerable`1[System.String]
    but was
System.Int32[]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            Enumerable.Empty<string>().ShouldBeOfType<IEnumerable<string>>();
        }
    }
}
