using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeEquivalentTo
{
    public class NullScenario
    {
        [Fact]
        public void ShouldFailWhenActualIsNull()
        {
            string? subject = null;
            Verify.ShouldFail(() =>
subject.ShouldBeEquivalentTo("Hello", "Some additional context"),

errorWithSource:
@"Comparing object equivalence, at path:
subject

    Expected value to be
""Hello""
    but was
null

Additional Info:
    Some additional context",

errorWithoutSource:
@"Comparing object equivalence, at path:
<root>

    Expected value to be
""Hello""
    but was
null

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldFailWhenExpectedIsNull()
        {
            const string subject = "Hello";
            Verify.ShouldFail(() =>
subject.ShouldBeEquivalentTo(null, "Some additional context"),

errorWithSource:
@"Comparing object equivalence, at path:
subject

    Expected value to be
null
    but was
""Hello""

Additional Info:
    Some additional context",

errorWithoutSource:
@"Comparing object equivalence, at path:
<root>

    Expected value to be
null
    but was
""Hello""

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPassWhenBothAreNull()
        {
            string? subject = null;
            subject.ShouldBeEquivalentTo(null);
        }
    }
}
