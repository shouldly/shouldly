using System.Text.RegularExpressions;

namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class SetScenario
{
    [Fact]
    public void ShouldFailWhenSetIsTooShort()
    {
        var subject = new HashSet<int>{ 1, 2, 3, 4 };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3, 4, 5 }, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            [1, 2, 3, 4] [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 3, 4]

            Additional Info:
                Some additional context; [5] is expected but not found
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 3, 4]

            Additional Info:
                Some additional context; [5] is expected but not found
            """,

            MessageScrubber);
    }

    [Fact]
    public void ShouldFailWhenSetIsTooLong()
    {
        var subject = new HashSet<int>{ 1, 2, 3, 4, 5, 6 };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3, 4, 5 }, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            [1, 2, 3, 4, 5, 6] [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 3, 4, 5, 6]

            Additional Info:
                Some additional context; [6] is not expected but found
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 3, 4, 5, 6]

            Additional Info:
                Some additional context; [6] is not expected but found
            """,

            MessageScrubber);
    }

    [Fact]
    public void ShouldFailWhenSetDoNotMatch()
    {
        var subject = new HashSet<int>{ 1, 2, 6, 4, 3 };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3, 4, 5 }, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            [1, 2, 6, 4, 3] [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 6, 4, 3]

            Additional Info:
                Some additional context; [5] is expected but not found; [6] is not expected but found
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 6, 4, 3]

            Additional Info:
                Some additional context; [5] is expected but not found; [6] is not expected but found
            """,

            MessageScrubber);
    }

    [Fact]
    public void ShouldPass()
    {
        var subject = new HashSet<int> { 1, 2, 6, 4, 5 };
        subject.ShouldBeEquivalentTo(new HashSet<int> { 6, 2, 1, 5, 4 });
    }

    private static string MessageScrubber(string original)
    {
        const string pattern1 = @"\[System\.Int32,[^\]]+\]";
        const string replacement1 = "[System.Int32]";
        const string pattern2 = @"HashSet\`[0-9]";
        const string replacement2 = "HashSet";

        return Regex.Replace(
            Regex.Replace(original, pattern1, replacement1),
            pattern2,
            replacement2);
    }
}