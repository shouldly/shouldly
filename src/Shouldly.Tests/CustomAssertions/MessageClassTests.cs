namespace Shouldly.Tests.CustomAssertions;

/// <summary>
/// The message classes custom assertions construct, the v4 pattern. A method named exactly like a built-in
/// assertion gets its wording; any other gets the plain shape of the class constructed.
/// </summary>
public class MessageClassTests
{
    [Fact]
    public void ExpectedShouldlyMessageShowsTheExpectedValue() =>
        new ExpectedShouldlyMessage("an attribute", null, "ShouldBeDecorated", "type").ToString()
            .ShouldBe("type\n    should be decorated \n\"an attribute\"\n    but does not", StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void ActualShouldlyMessageShowsTheActualValue() =>
        new ActualShouldlyMessage(5, null, "ShouldBeAwesome", "count").ToString()
            .ShouldBe(
                """
                count
                    should be awesome
                null
                    but was
                5
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void ExpectedActualShouldlyMessageShowsBoth() =>
        new ExpectedActualShouldlyMessage(1, 2, "more to say", "ShouldBeAwesome", "count").ToString()
            .ShouldBe(
                """
                count
                    should be awesome
                1
                    but was
                2

                Additional Info:
                    more to say
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void AMethodNamedLikeABuiltInAssertionGetsItsWording() =>
        new ExpectedActualShouldlyMessage(1, new[] { 2, 3 }, null, "ShouldContain", "numbers").ToString()
            .ShouldBe(
                """
                numbers
                    should contain
                1
                    but was actually
                [2, 3]
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void AMethodWhoseNameOnlyResemblesABuiltInAssertionGetsThePlainShape() =>
        new ExpectedActualShouldlyMessage(1, new[] { 2, 3 }, null, "ShouldContainExactly", "numbers").ToString()
            .ShouldBe(
                """
                numbers
                    should contain exactly
                1
                    but was
                [2, 3]
                """, StringCompareShould.IgnoreLineEndings);

    [Theory]
    [InlineData("ShouldBeCloseTo")]
    [InlineData("ShouldRoughlyEqual")]
    public void ToleranceMessageAlwaysShowsItsTolerance(string shouldlyMethod) =>
        new ExpectedActualToleranceShouldlyMessage(1.0, 2.0, 0.5, null, shouldlyMethod, "reading").ToString()
            .ShouldBe(
                """
                reading
                    should be within
                0.5d
                    of
                1d
                    but was
                2d
                """, StringCompareShould.IgnoreLineEndings);

    // A message class states its tolerance but not how its assertion compared with it, so it is taken to have
    // compared as ShouldBe does with that tolerance: only the item outside it is marked.
    [Fact]
    public void ToleranceMessageMarksOnlyItemsOutsideItsTolerance() =>
        new ExpectedActualToleranceShouldlyMessage(new[] { 1.0, 2.0 }, new[] { 1.005, 2.5 }, 0.01, null, "ShouldBeCloseTo", "readings").ToString()
            .ShouldBe(
                """
                readings
                    should be within
                0.01d
                    of
                [1d, 2d]
                    but was
                [1.005d, 2.5d]
                    difference
                [1.005d, *2.5d*]
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void CaseSensitivityMessageAlwaysShowsItsCaseOption() =>
        new ExpectedActualWithCaseSensitivityShouldlyMessage("a", "B", Case.Insensitive, null, "ShouldResemble", "letter").ToString()
            .ShouldBe(
                """
                letter
                    should resemble (case insensitive comparison)
                "a"
                    but was
                "B"
                """, StringCompareShould.IgnoreLineEndings);

    // ShouldBe's own wording only mentions case for a sequence of strings, so for two strings the class's plain
    // shape is used, which shows the option.
    [Fact]
    public void CaseSensitivityMessageNamedLikeABuiltInAssertionKeepsItsCaseOption() =>
        new ExpectedActualWithCaseSensitivityShouldlyMessage("a", "B", Case.Insensitive, null, "ShouldBe", "letter").ToString()
            .ShouldBe(
                """
                letter
                    should be (case insensitive comparison)
                "a"
                    but was
                "B"
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void CaseSensitivityMessageGetsTheBuiltInWordingWhereItShowsTheCaseOption() =>
        new ExpectedActualWithCaseSensitivityShouldlyMessage(new[] { "a" }, new[] { "B" }, Case.Insensitive, null, "ShouldBe", "letters").ToString()
            .ShouldBe(
                """
                letters
                    should be
                ["a"]
                    but was (case insensitive comparison)
                ["B"]
                    difference
                [*"B"*]
                """, StringCompareShould.IgnoreLineEndings);

    [Theory]
    [InlineData(Case.Insensitive, "should contain (case insensitive comparison)\n\"a\"\n    but was actually\n\"B\"")]
    [InlineData(Case.Sensitive, "should contain (case sensitive comparison)\n\"a\"\n    but was\n\"B\"")]
    public void CaseSensitivityMessageNamedShouldContainShowsEitherCaseOption(Case caseSensitivity, string expectedAfterSubject) =>
        new ExpectedActualWithCaseSensitivityShouldlyMessage("a", "B", caseSensitivity, null, "ShouldContain", "letter").ToString()
            .ShouldBe("letter\n    " + expectedAfterSubject, StringCompareShould.IgnoreLineEndings);

    // ShouldStartWith's own wording has no place for a tolerance.
    [Theory]
    [InlineData("ShouldStartWith")]
    [InlineData("ShouldBeNull")]
    [InlineData("ShouldBe")]
    public void ToleranceMessageNamedLikeABuiltInAssertionKeepsItsTolerance(string shouldlyMethod) =>
        new ExpectedActualToleranceShouldlyMessage(1.0, 2.0, 0.5, null, shouldlyMethod, "reading").ToString()
            .ShouldBe(
                """
                reading
                    should be within
                0.5d
                    of
                1d
                    but was
                2d
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void ThrowMessageNamedLikeTheExtensionShowsTheDelegateInvoked() =>
        new ShouldlyThrowMessage(typeof(ArgumentException), typeof(InvalidOperationException), null, "ShouldThrow", "action").ToString()
            .ShouldBe(
                """
                `action()`
                    should throw
                System.ArgumentException
                    but threw
                System.InvalidOperationException
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void ThrowMessageUnderAnotherNameShowsTheDelegateAsWritten() =>
        new ShouldlyThrowMessage(typeof(ArgumentException), null, "ShouldBlowUp", "action").ToString()
            .ShouldBe(
                """
                `action`
                    should throw
                System.ArgumentException
                    but did not
                """, StringCompareShould.IgnoreLineEndings);

    // The first segment of a path continues the subject's line, as Shouldly's own paths do with the root's type.
    [Fact]
    public void EquivalenceMessageShowsThePath() =>
        new ExpectedEquivalenceShouldlyMessage(1, 2, [" [Order]", "Total [System.Int32]"], null, "ShouldMirror", "order").ToString()
            .ShouldBe(
                """
                Comparing object equivalence, at path:
                order [Order]
                    Total [System.Int32]

                    Expected value to be
                1
                    but was
                2
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void APhraseSayingNotIsNegated() =>
        // The subject already shows the value, so a negated failure has nothing to add after "but was".
        new ExpectedActualShouldlyMessage(1, 1, null, "ShouldNotBeAwesome", "1").ToString()
            .ShouldBe(
                """
                1
                    should not be awesome
                1
                    but was
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void ANameThatMerelyContainsNotIsNotNegated() =>
        new ExpectedActualShouldlyMessage(2, 1, null, "ShouldBeNoteworthy", "1").ToString()
            .ShouldEndWith("but was not");

    [Fact]
    public void AssertAwesomelyUsesThePlainShape()
    {
        var count = 5;

        Should.Throw<ShouldAssertException>(() => count.ShouldBeLucky()).Message
            .ShouldBe(
                """
                count
                    should be lucky
                7
                    but was
                5
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void AssertAwesomelyIgnoringOrderSaysWhatIsMissing()
    {
        int[] numbers = [1, 2];

        Should.Throw<ShouldAssertException>(() => numbers.ShouldHoldInAnyOrder([2, 3])).Message
            .ShouldContain("is missing");
    }

    [Fact]
    public void SinglePassSequencesAreReadOnce()
    {
        var onArrays = new ExpectedActualShouldlyMessage(new[] { "apple", "pear" }, new[] { "apple", "plum" }, null, "ShouldHold", "basket").ToString();

        var onSinglePass = new ExpectedActualShouldlyMessage(new SinglePass<string>("apple", "pear"), new SinglePass<string>("apple", "plum"), null, "ShouldHold", "basket").ToString();

        onSinglePass.ShouldBe(onArrays);
    }

    [Fact]
    public void ACountReadsTheWholeSequenceNotJustWhatTheMessageShows()
    {
        using (MaxEnumerableItemsScope.Of(3))
        {
            var onArray = new ExpectedActualShouldlyMessage(2, new[] { 1, 2, 3, 4, 5, 6 }, null, "ShouldHaveCount", "numbers").ToString();

            var onSinglePass = new ExpectedActualShouldlyMessage(2, new SinglePass<int>(1, 2, 3, 4, 5, 6), null, "ShouldHaveCount", "numbers").ToString();

            onSinglePass.ShouldBe(onArray);
        }
    }

    [Fact]
    public void ASubsetIsJudgedAgainstTheWholeExpectedSequence()
    {
        using (MaxEnumerableItemsScope.Of(3))
        {
            var onArrays = new ExpectedActualShouldlyMessage(new[] { 1, 2, 3, 4, 5 }, new[] { 5, 6 }, null, "ShouldBeSubsetOf", "numbers").ToString();

            var onSinglePass = new ExpectedActualShouldlyMessage(new SinglePass<int>(1, 2, 3, 4, 5), new SinglePass<int>(5, 6), null, "ShouldBeSubsetOf", "numbers").ToString();

            onSinglePass.ShouldBe(onArrays);
        }
    }

    [Fact]
    public void ACaseOptionIsShownForASinglePassSequenceOfStrings()
    {
        var onArrays = new ExpectedActualWithCaseSensitivityShouldlyMessage(new[] { "a", "b" }, new[] { "a", "c" }, Case.Insensitive, null, "ShouldBe", "letters").ToString();

        var onSinglePass = new ExpectedActualWithCaseSensitivityShouldlyMessage(new SinglePass<string>("a", "b"), new SinglePass<string>("a", "c"), Case.Insensitive, null, "ShouldBe", "letters").ToString();

        onSinglePass.ShouldBe(onArrays);
    }

    [Fact]
    public void ATypeMessageNamesTheSequencesOwnType() =>
        new ExpectedActualShouldlyMessage(typeof(int[]), new SinglePass<int>(1), null, "ShouldBeOfType", "numbers").ToString()
            .ShouldContain(typeof(SinglePass<int>).FullName!);

    [Fact]
    public void AssertAwesomelyReadsASinglePassSequenceOnlyOnceMore()
    {
        // The assertion's own check reads the sequence first; the message then reads it once, not again for each
        // place it is shown.
        var numbers = new ReadTwice<int>(1, 2);

        Should.Throw<ShouldAssertException>(() => numbers.ShouldHoldOnlyThree()).Message
            .ShouldBe(
                """
                numbers
                    should hold only three
                [3]
                    but was
                [1, 2]
                    difference
                [*1*, *2*]
                """, StringCompareShould.IgnoreLineEndings);
    }

    /// <summary>
    /// A sequence that throws if it is enumerated a third time.
    /// </summary>
    private sealed class ReadTwice<T>(params T[] items) : IEnumerable<T>
    {
        private int _reads;

        public IEnumerator<T> GetEnumerator()
        {
            if (++_reads > 2)
                throw new InvalidOperationException("This sequence can only be enumerated twice.");

            return ((IEnumerable<T>)items).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

static class MessageClassAssertionsUnderTest
{
    public static void ShouldBeLucky(this int actual, string? customMessage = null,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        actual.AssertAwesomely(v => v == 7, actual, 7, customMessage, actualExpression: actualExpression);

    public static void ShouldHoldInAnyOrder(this int[] actual, int[] expected, string? customMessage = null,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        actual.AssertAwesomelyIgnoringOrder(v => v.OrderBy(x => x).SequenceEqual(expected.OrderBy(x => x)), actual, expected, customMessage, actualExpression: actualExpression);

    public static void ShouldHoldOnlyThree(this IEnumerable<int> actual, string? customMessage = null,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        actual.AssertAwesomely(v => v.SequenceEqual([3]), actual, new[] { 3 }, customMessage, actualExpression: actualExpression);
}
