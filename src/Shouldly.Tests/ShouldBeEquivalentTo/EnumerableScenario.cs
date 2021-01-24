using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeEquivalentTo
{
    public class EnumerableScenario
    {
        [Fact]
        public void ShouldFailWhenListIsTooShort()
        {
            var subject = new[] { 1, 2, 3, 4 };
            Verify.ShouldFail(() =>
subject.ShouldBeEquivalentTo(new[] { 1, 2, 3, 4, 5 }, "Some additional context"),

errorWithSource:
@"Comparing object equivalence, at path:
subject [System.Int32[]]
    Count

    Expected value to be
5
    but was
4

Additional Info:
    Some additional context",

errorWithoutSource:
@"Comparing object equivalence, at path:
<root> [System.Int32[]]
    Count

    Expected value to be
5
    but was
4

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldFailWhenListIsTooLong()
        {
            var subject = new[] { 1, 2, 3, 4, 5, 6 };
            Verify.ShouldFail(() =>
subject.ShouldBeEquivalentTo(new[] { 1, 2, 3, 4, 5 }, "Some additional context"),

errorWithSource:
@"Comparing object equivalence, at path:
subject [System.Int32[]]
    Count

    Expected value to be
5
    but was
6

Additional Info:
    Some additional context",

errorWithoutSource:
@"Comparing object equivalence, at path:
<root> [System.Int32[]]
    Count

    Expected value to be
5
    but was
6

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldFailWhenListsDoNotMatch()
        {
            var subject = new[] { 1, 2, 6, 4, 5 };

            Verify.ShouldFail(() =>
subject.ShouldBeEquivalentTo(new[] { 1, 2, 3, 4, 5 }, "Some additional context"),

errorWithSource:
@"Comparing object equivalence, at path:
subject [System.Int32[]]
    Element 3 not found

    Expected value to be
[1, 2, 3, 4, 5]
    but was
[1, 2, 6, 4, 5]

Additional Info:
    Some additional context",

errorWithoutSource:
@"Comparing object equivalence, at path:
<root> [System.Int32[]]
    Element 3 not found

    Expected value to be
[1, 2, 3, 4, 5]
    but was
[1, 2, 6, 4, 5]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var subject = new[] { 1, 2, 6, 4, 5 };
            subject.ShouldBeEquivalentTo(new[] { 1, 2, 6, 4, 5 });
        }


        [Fact]
        public void ShouldPassIfOutOfOrder()
        {
            var subject = new[] { 1, 2, 4, 5, 6 };
            subject.ShouldBeEquivalentTo(new[] { 1, 2, 6, 4, 5 });
        }
    }
}
