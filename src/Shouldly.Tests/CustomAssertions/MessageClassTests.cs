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
}

static class MessageClassAssertionsUnderTest
{
    public static void ShouldBeLucky(this int actual, string? customMessage = null,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        actual.AssertAwesomely(v => v == 7, actual, 7, customMessage, actualExpression: actualExpression);

    public static void ShouldHoldInAnyOrder(this int[] actual, int[] expected, string? customMessage = null,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        actual.AssertAwesomelyIgnoringOrder(v => v.OrderBy(x => x).SequenceEqual(expected.OrderBy(x => x)), actual, expected, customMessage, actualExpression: actualExpression);
}
