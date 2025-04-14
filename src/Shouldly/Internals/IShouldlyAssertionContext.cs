namespace Shouldly;

/// <summary>
/// Interface for the context of a Shouldly assertion
/// </summary>
public interface IShouldlyAssertionContext
{
    /// <summary>
    /// The name of the should method being called
    /// </summary>
    string ShouldMethod { get; set; }

    /// <summary>
    /// The code part being evaluated
    /// </summary>
    string? CodePart { get; set; }

    /// <summary>
    /// The file name where the assertion is made
    /// </summary>
    string? FileName { get; set; }

    /// <summary>
    /// The line number where the assertion is made
    /// </summary>
    int? LineNumber { get; set; }

    /// <summary>
    /// The key being used in the assertion
    /// </summary>
    object? Key { get; set; }

    /// <summary>
    /// The expected value in the assertion
    /// </summary>
    object? Expected { get; set; }

    /// <summary>
    /// The actual value in the assertion
    /// </summary>
    object? Actual { get; set; }

    /// <summary>
    /// The tolerance for numeric comparisons
    /// </summary>
    object? Tolerance { get; set; }

    /// <summary>
    /// The timeout for assertions that wait for a condition
    /// </summary>
    TimeSpan? Timeout { get; set; }

    /// <summary>
    /// Whether to ignore the order when comparing collections
    /// </summary>
    bool IgnoreOrder { get; set; }

    // For now, this property cannot just check to see if "Actual != null". The term is overloaded.
    // In some cases it means the "Actual" value is not relevant (eg: "dictionary.ContainsKey(key)") and in some
    // cases it means that the value is relevant, but during execution we got a null. (eg: Foo.ShouldBe(bar) where
    // Foo is null). So for now, it is a flag needs to be set externally to determine whether or not the "Actual" value
    // is relevant.
    /// <summary>
    /// Whether the actual value is relevant for the assertion
    /// </summary>
    bool HasRelevantActual { get; set; }

    /// <summary>
    /// Whether the key is relevant for the assertion
    /// </summary>
    bool HasRelevantKey { get; set; }

    /// <summary>
    /// Whether the assertion is negated (e.g., ShouldNotBe instead of ShouldBe)
    /// </summary>
    bool IsNegatedAssertion { get; }

    /// <summary>
    /// Custom message to display when the assertion fails
    /// </summary>
    string? CustomMessage { get; set; }

    /// <summary>
    /// Case sensitivity setting for string comparisons
    /// </summary>
    Case? CaseSensitivity { get; set; }

    /// <summary>
    /// Whether the code part matches the actual value
    /// </summary>
    bool CodePartMatchesActual { get; }

    /// <summary>
    /// Filter expression for collection assertions
    /// </summary>
    Expression? Filter { get; set; }

    /// <summary>
    /// The expected match count for collection assertions
    /// </summary>
    int? MatchCount { get; set; }

    /// <summary>
    /// The sort direction for ordered collection assertions
    /// </summary>
    SortDirection SortDirection { get; set; }

    /// <summary>
    /// The index of the out-of-order element in collection assertions
    /// </summary>
    int OutOfOrderIndex { get; set; }

    /// <summary>
    /// The out-of-order object in collection assertions
    /// </summary>
    object? OutOfOrderObject { get; set; }

    /// <summary>
    /// The path to the property being asserted
    /// </summary>
    IEnumerable<string>? Path { get; set; }
}