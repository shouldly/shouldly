using System.Text.RegularExpressions;

namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class DictionaryScenario
{
    [Fact]
    public void ShouldFailWhenDictionaryIsTooShort()
    {
        var subject = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5
        };
        var expected = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5,
            [4] = 8
        };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [System.Collections.Generic.Dictionary[[System.Int32],[System.Int32]]]
                Keys

                Expected value to be
            [1, 2, 3, 4]
                but was
            [1, 2, 3]

            Additional Info:
                Some additional context; [4] is expected but not found
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.Dictionary[[System.Int32],[System.Int32]]]
                Keys

                Expected value to be
            [1, 2, 3, 4]
                but was
            [1, 2, 3]

            Additional Info:
                Some additional context; [4] is expected but not found
            """,

            MessageScrubber);
    }

     [Fact]
     public void ShouldFailWhenDictionaryIsTooLong()
     {
        var subject = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5,
            [4] = 8,
            [5] = 13
        };
        var expected = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5,
            [4] = 8
        };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [System.Collections.Generic.Dictionary[[System.Int32],[System.Int32]]]
                Keys

                Expected value to be
            [1, 2, 3, 4]
                but was
            [1, 2, 3, 4, 5]

            Additional Info:
                Some additional context; [5] is not expected but found
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.Dictionary[[System.Int32],[System.Int32]]]
                Keys

                Expected value to be
            [1, 2, 3, 4]
                but was
            [1, 2, 3, 4, 5]

            Additional Info:
                Some additional context; [5] is not expected but found
            """,

            MessageScrubber);
     }

     [Fact]
     public void ShouldFailWhenDictionaryDoNotMatchKeys()
     {
        var subject = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [5] = 5,
            [6] = 8
        };
        var expected = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5,
            [4] = 8
        };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [System.Collections.Generic.Dictionary[[System.Int32],[System.Int32]]]
                Keys

                Expected value to be
            [1, 2, 3, 4]
                but was
            [1, 2, 5, 6]

            Additional Info:
                Some additional context; [3, 4] is expected but not found; [5, 6] is not expected but found
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.Dictionary[[System.Int32],[System.Int32]]]
                Keys

                Expected value to be
            [1, 2, 3, 4]
                but was
            [1, 2, 5, 6]

            Additional Info:
                Some additional context; [3, 4] is expected but not found; [5, 6] is not expected but found
            """,

            MessageScrubber);
     }

     [Fact]
     public void ShouldFailWhenDictionaryDoNotMatchValue()
     {
        var subject = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 13,
            [4] = 8
        };
        var expected = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5,
            [4] = 8
        };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(expected, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [System.Collections.Generic.Dictionary[[System.Int32],[System.Int32]]]
                Value [3] [System.Int32]

                Expected value to be
            5
                but was
            13

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.Dictionary[[System.Int32],[System.Int32]]]
                Value [3] [System.Int32]

                Expected value to be
            5
                but was
            13

            Additional Info:
                Some additional context
            """,

            MessageScrubber);
     }

     [Fact]
     public void ShouldPass()
     {
        var subject = new Dictionary<int, int>
        {
            [1] = 2,
            [4] = 8,
            [3] = 5,
            [2] = 3
        };
        var expected = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5,
            [4] = 8
        };
         subject.ShouldBeEquivalentTo(expected);
     }

    private static string MessageScrubber(string original)
    {
        const string pattern1 = @"\[System\.Int32,[^\]]+\]";
        const string replacement1 = "[System.Int32]";
        const string pattern2 = @"Dictionary\`[0-9]+";
        const string replacement2 = "Dictionary";

        return Regex.Replace(
            Regex.Replace(original, pattern1, replacement1),
            pattern2,
            replacement2);
    }
}