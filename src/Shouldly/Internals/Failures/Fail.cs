namespace Shouldly.Internals.Failures;

/// <summary>
/// The failures Shouldly's own assertions report, one per assertion and named exactly as the assertion. An
/// assertion that fails throws <c>Fail.Name.With(what the check established).For(actual, customMessage,
/// actualExpression)</c>.
/// </summary>
static class Fail
{
    public static readonly NullFailure ShouldBeNull = new();
    public static readonly NullFailure ShouldNotBeNull = new(negated: true);

    public static readonly BooleanFailure ShouldBeTrue = new(expected: true);
    public static readonly BooleanFailure ShouldBeFalse = new(expected: false);

    public static readonly PositiveFailure ShouldBePositive = new();
    public static readonly NegativeFailure ShouldBeNegative = new();

    public static readonly NullOrEmptyFailure ShouldBeNullOrEmpty = new();
    public static readonly NullOrEmptyFailure ShouldNotBeNullOrEmpty = new(negated: true);
    public static readonly NullOrWhiteSpaceFailure ShouldBeNullOrWhiteSpace = new();
    public static readonly NullOrWhiteSpaceFailure ShouldNotBeNullOrWhiteSpace = new(negated: true);

    public static readonly SameAsFailure ShouldBeSameAs = new();
    public static readonly PlainFailure ShouldNotBeSameAs = new(negated: true);

    public static readonly PlainFailure ShouldMatch = new();
    public static readonly NotMatchFailure ShouldNotMatch = new();

    public static readonly CompletedSuccessfullyFailure ShouldBeCompletedSuccessfully = new();

    public static readonly ContainFailure ShouldContain = new();
    public static readonly ContainFailure ShouldNotContain = new(negated: true);
    public static readonly ContainFailure ShouldContainAll = new();
    public static readonly ContainFailure ShouldNotContainAll = new(negated: true);
    public static readonly ContainFailure ShouldContainAny = new();
    public static readonly ContainFailure ShouldNotContainAny = new(negated: true);
    public static readonly ContainFailure ShouldContainWithoutWhitespace = new();

    public static readonly AllBeFailure ShouldAllBe = new();
    public static readonly EmptyFailure ShouldBeEmpty = new();
    public static readonly EmptyFailure ShouldNotBeEmpty = new(negated: true);
    public static readonly SingleItemFailure ShouldHaveSingleItem = new();
    public static readonly CountFailure ShouldHaveCount = new();
    public static readonly SubsetFailure ShouldBeSubsetOf = new();
    public static readonly UniqueFailure ShouldBeUnique = new();
    public static readonly InOrderFailure ShouldBeInOrder = new();

    public static readonly PlainFailure ShouldBeGreaterThan = new();
    public static readonly PlainFailure ShouldBeGreaterThanOrEqualTo = new();
    public static readonly PlainFailure ShouldBeLessThan = new();
    public static readonly PlainFailure ShouldBeLessThanOrEqualTo = new();
    public static readonly PlainFailure ShouldBeInRange = new();
    public static readonly PlainFailure ShouldNotBeInRange = new(negated: true);
    public static readonly PlainFailure ShouldBeOneOf = new();
    public static readonly PlainFailure ShouldNotBeOneOf = new(negated: true);

    public static readonly TypeFailure ShouldBeOfType = new();
    public static readonly TypeFailure ShouldBeAssignableTo = new();
    // These two get the plain shape, which shows the actual value where ShouldBeOfType shows its type.
    public static readonly PlainFailure ShouldNotBeOfType = new(negated: true);
    public static readonly PlainFailure ShouldNotBeAssignableTo = new(negated: true);

    public static readonly ExpectedOnlyFailure ShouldBeDecoratedWith = new();

    public static readonly FlagFailure ShouldHaveFlag = new();
    public static readonly FlagFailure ShouldNotHaveFlag = new(negated: true);

    // The two StartWith assertions get the plain shape, which says "but was" where EndWith says "but did not".
    public static readonly PlainFailure ShouldStartWith = new();
    public static readonly PlainFailure ShouldNotStartWith = new(negated: true);
    public static readonly StringEndFailure ShouldEndWith = new();
    public static readonly StringEndFailure ShouldNotEndWith = new(negated: true);
}
