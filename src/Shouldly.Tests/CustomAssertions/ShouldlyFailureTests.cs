using System.Runtime.CompilerServices;

namespace Shouldly.Tests.CustomAssertions;

/// <summary>
/// The building block for custom assertions: <see cref="ShouldAssertException.For"/> and the controls on the
/// <see cref="ShouldlyFailure"/> it returns.
/// </summary>
public class ShouldlyFailureTests
{
    private static readonly string[] Fruit = ["apple", "pear"];

    [Fact]
    public void ShowsTheSubjectThePhraseAndTheActualValue() =>
        MessageOf(() => 5.ShouldBeAwesome())
            .ShouldBe(
                """
                5
                    should be awesome
                    but was not
                """, StringCompareShould.IgnoreLineEndings);

    [Fact]
    public void TakesThePhraseFromTheNameOfTheAssertion()
    {
        var count = 5;

        MessageOf(() => count.ShouldBeAwesome())
            .ShouldBe(
                """
                count
                    should be awesome
                    but was
                5
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void ExpectedAddsTheExpectedValue()
    {
        var name = "Bob";

        MessageOf(() => ShouldAssertException.For(name, null, nameof(name), "ShouldBeNamed").Expected("Alice").Throw())
            .ShouldBe(
                """
                name
                    should be named
                "Alice"
                    but was
                "Bob"
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void ExpectedAndActualSequencesHaveTheirDifferencesHighlighted()
    {
        string[] basket = ["apple", "plum"];

        MessageOf(() => ShouldAssertException.For(basket, null, nameof(basket), "ShouldHold").Expected(Fruit).Throw())
            .ShouldBe(
                """
                basket
                    should hold
                ["apple", "pear"]
                    but was
                ["apple", "plum"]
                    difference
                ["apple", *"plum"*]
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void PhraseStatesThePhrase()
    {
        var response = "400";

        MessageOf(() => ShouldAssertException.For(response, null, nameof(response), "ShouldHaveError").Phrase("should have an error for").Expected("name").Throw())
            .ShouldBe(
                """
                response
                    should have an error for
                "name"
                    but was
                "400"
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void WithoutActualLeavesTheActualValueOut()
    {
        var type = typeof(string);

        MessageOf(() => ShouldAssertException.For(type, null, nameof(type), "ShouldBeDecoratedWith").Expected("Obsolete").WithoutActual().Throw())
            .ShouldBe(
                """
                type
                    should be decorated with
                "Obsolete"
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void ExpectedInWordsIsShownAsWritten()
    {
        var items = Array.Empty<int>();

        MessageOf(() => ShouldAssertException.For(items.Length, null, "items.Length", "ShouldHaveCount").ExpectedInWords("at least 1 element").Throw())
            .ShouldBe(
                """
                items.Length
                    should have count
                at least 1 element
                    but was
                0
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void ActualInWordsReplacesTheActualValue()
    {
        var json = "{}";

        MessageOf(() => ShouldAssertException.For(json, null, nameof(json), "ShouldHaveArrayCount").Expected(3).ActualInWords("no value at '/items'").Throw())
            .ShouldBe(
                """
                json
                    should have array count
                3
                    but was
                no value at '/items'
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void ValuesDescribedInWordsHaveNoDifferenceHighlighted()
    {
        string[] basket = ["apple", "plum"];

        MessageOf(() => ShouldAssertException.For(basket, null, nameof(basket), "ShouldHold").Expected(Fruit).ActualInWords("something else").Throw())
            .ShouldNotContain("difference");
    }

    [Fact]
    public void ContextAppendsLabelledBlocksAfterTheCustomMessage()
    {
        var status = 404;

        MessageOf(() => ShouldAssertException.For(status, "after creating the party", nameof(status), "ShouldBeStatus")
                .Expected(200)
                .Context("Response", "GET /parties/42\nHTTP/1.1 404")
                .Context("Empty", "")
                .Context("Request id", "abc-123")
                .Throw())
            .ShouldBe(
                """
                status
                    should be status
                200
                    but was
                404

                Additional Info:
                    after creating the party

                Response:
                    GET /parties/42
                    HTTP/1.1 404

                Request id:
                    abc-123
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void APhraseSayingNotIsNegated()
    {
        string[] basket = ["apple", "pear"];

        // A negated assertion fails because the values matched, so no difference is highlighted.
        MessageOf(() => ShouldAssertException.For(basket, null, nameof(basket), "ShouldNotHold").Expected(Fruit).Throw())
            .ShouldBe(
                """
                basket
                    should not hold
                ["apple", "pear"]
                    but was
                ["apple", "pear"]
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void ANameThatMerelyContainsNotIsNotNegated()
    {
        string[] basket = ["apple", "plum"];

        MessageOf(() => ShouldAssertException.For(basket, null, nameof(basket), "ShouldBeNoteworthy").Expected(Fruit).Throw())
            .ShouldContain("difference");
    }

    [Fact]
    public void TheLastStatementOfALineWins()
    {
        var value = 1;

        MessageOf(() => ShouldAssertException.For(value, null, nameof(value), "ShouldBeOdd")
                .WithoutActual().ActualInWords("even").ExpectedInWords("an odd number").Expected(3).Throw())
            .ShouldBe(
                """
                value
                    should be odd
                3
                    but was
                even
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void IsCaughtAsAShouldAssertException()
    {
        var value = 2;

        var exception = Should.Throw<ShouldAssertException>(() => value.ShouldBeAwesome());

        exception.ShouldBeOfType<ShouldlyFailure>();
    }

    [Fact]
    public void TripsTheTripWireWithoutAnExpression()
    {
        // The suite arms the trip-wire, so a custom assertion that loses its captured expression is caught here.
        var exception = Should.Throw<InvalidOperationException>(() => ShouldAssertException.For(1, null, null, "ShouldBeLost"));

        exception.Message.ShouldContain("ShouldBeLost");
    }

    [Fact]
    public void FallsBackToTheValueWithoutAnExpression()
    {
        // Without source to show, the value stands in for the subject. Source is disabled so that the build for
        // older targets doesn't go looking for it on the stack.
        using (ShouldlyConfiguration.DisableSourceInErrors())
        {
            MessageOf(() => ShouldAssertException.For(5, null, null, "ShouldBeSix").Expected(6).Throw())
                .ShouldBe(
                    """
                    5
                        should be six
                    6
                        but was not
                    """, StringCompareShould.IgnoreLineEndings);
        }
    }

    [Fact]
    public void TheMessageIsUnaffectedByWhatHappensToTheValuesAfterwards()
    {
        var actual = new Fickle("as it was");
        var expected = new Fickle("as expected");
        var failure = ShouldAssertException.For(actual, null, nameof(actual), "ShouldBeSettled").Expected(expected);

        // A test framework reads the message after the assertion has thrown, by when a using block may have
        // disposed the values.
        actual.Dispose();
        expected.Dispose();

        failure.Message
            .ShouldBe(
                """
                actual
                    should be settled
                as expected
                    but was
                as it was
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void SinglePassSequencesAreReadOnce()
    {
        var basket = new SinglePass<string>("apple", "plum");
        var expected = new SinglePass<string>("apple", "pear");

        MessageOf(() => ShouldAssertException.For(basket, null, nameof(basket), "ShouldHold").Expected(expected).Throw())
            .ShouldBe(
                """
                basket
                    should hold
                ["apple", "pear"]
                    but was
                ["apple", "plum"]
                    difference
                ["apple", *"plum"*]
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void ASinglePassSequenceStandingInForTheSubjectIsReadOnce()
    {
        var basket = new SinglePass<string>("apple");

        using (ShouldlyConfiguration.DisableSourceInErrors())
        {
            MessageOf(() => ShouldAssertException.For(basket, null, null, "ShouldBeEmpty").Throw())
                .ShouldBe(
                    """
                    ["apple"]
                        should be empty
                        but was not
                    """, StringCompareShould.IgnoreLineEndings);
        }
    }

    [Fact]
    public void AnEndlessSequenceIsReadOnlyAsFarAsItIsShown()
    {
        using (MaxEnumerableItemsScope.Of(3))
        {
            MessageOf(() => ShouldAssertException.For(Endless(), null, "numbers", "ShouldStop").Expected(new[] { 1, 2 }).Throw())
                .ShouldBe(
                    """
                    numbers
                        should stop
                    [1, 2]
                        but was
                    [1, 2, 3, ...]
                        difference
                    [1, 2, *3*, ...]
                    """, StringCompareShould.IgnoreLineEndings);
        }

        static IEnumerable<int> Endless()
        {
            for (var number = 1; ; number++)
                yield return number;
        }
    }

    [Fact]
    public void AnEmptySequenceIsStillDescribedByItsOwnText()
    {
        var nothing = new NamedEmpty();

        MessageOf(() => ShouldAssertException.For(nothing, null, nameof(nothing), "ShouldHaveItems").Throw())
            .ShouldBe(
                """
                nothing
                    should have items
                    but was
                [] (an empty basket)
                """, StringCompareShould.IgnoreLineEndings);
    }

    private sealed class NamedEmpty : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator() => Enumerable.Empty<string>().GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => "an empty basket";
    }

    [Fact]
    public void ALargeContextBlockIsAppendedLineByLine()
    {
        var lines = Enumerable.Range(1, 2000).Select(line => $"line {line}").ToArray();

        var message = ShouldAssertException.For(1, null, "one", "ShouldBeTwo").Context("Log", string.Join("\r\n", lines)).Message;

        message.ShouldEndWith("    line 2000");
        message.Split('\n').Count(line => line.StartsWith("    line ", StringComparison.Ordinal)).ShouldBe(lines.Length);
    }

    private sealed class Fickle(string text) : IDisposable
    {
        private bool _disposed;

        public void Dispose() => _disposed = true;

        public override string ToString() =>
            _disposed ? throw new ObjectDisposedException(nameof(Fickle)) : text;
    }

    private static string MessageOf(Action failingAssertion) =>
        Should.Throw<ShouldlyFailure>(failingAssertion).Message;
}

static class CustomAssertionsUnderTest
{
    public static void ShouldBeAwesome(this int actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (actual != 42)
            throw ShouldAssertException.For(actual, customMessage, actualExpression);
    }

    public static void Throw(this ShouldlyFailure failure) => throw failure;
}
