using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBeEquivalentTo
{
    public class TypeScenario
    {
        [Fact]
        public void ShouldFail()
        {
            const string subject = "Hello";
            Verify.ShouldFail(() =>
subject.ShouldBeEquivalentTo(5, "Some additional context"),

errorWithSource:
@"Comparing object equivalence, at path:
subject

    Expected value to be
System.Int32
    but was
System.String

Additional Info:
    Some additional context",

errorWithoutSource:
@"Comparing object equivalence, at path:
<root>

    Expected value to be
System.Int32
    but was
System.String

Additional Info:
    Some additional context");
        }
    }
}
