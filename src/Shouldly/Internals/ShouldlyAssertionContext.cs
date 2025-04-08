using Shouldly.Internals;

namespace Shouldly;

/// <summary>
/// Implementation of the Shouldly assertion context
/// </summary>
public class ShouldlyAssertionContext : IShouldlyAssertionContext
{
    /// <summary>
    /// The name of the should method being called
    /// </summary>
    public string ShouldMethod { get; set; }
    
    /// <summary>
    /// The code part being evaluated
    /// </summary>
    public string? CodePart { get; set; }
    
    /// <summary>
    /// The file name where the assertion is made
    /// </summary>
    public string? FileName { get; set; }
    
    /// <summary>
    /// The line number where the assertion is made
    /// </summary>
    public int? LineNumber { get; set; }

    /// <summary>
    /// The key being used in the assertion
    /// </summary>
    public object? Key { get; set; }
    
    /// <summary>
    /// The expected value in the assertion
    /// </summary>
    public object? Expected { get; set; }
    
    /// <summary>
    /// The actual value in the assertion
    /// </summary>
    public object? Actual { get; set; }
    
    /// <summary>
    /// The tolerance for numeric comparisons
    /// </summary>
    public object? Tolerance { get; set; }
    
    /// <summary>
    /// Case sensitivity setting for string comparisons
    /// </summary>
    public Case? CaseSensitivity { get; set; }
    
    /// <summary>
    /// Whether the code part matches the actual value
    /// </summary>
    public bool CodePartMatchesActual => CodePart == Actual.ToStringAwesomely();

    /// <summary>
    /// The timeout for assertions that wait for a condition
    /// </summary>
    public TimeSpan? Timeout { get; set; }

    /// <summary>
    /// Whether to ignore the order when comparing collections
    /// </summary>
    public bool IgnoreOrder { get; set; }

    // For now, this property cannot just check to see if "Actual != null". The term is overloaded.
    // In some cases it means the "Actual" value is not relevant (eg: "dictionary.ContainsKey(key)") and in some
    // cases it means that the value is relevant, but during execution we got a null. (eg: Foo.ShouldBe(bar) where
    // Foo is null). So for now, it is a flag needs to be set externally to determine whether or not the "Actual" value
    // is relevant.
    /// <summary>
    /// Whether the actual value is relevant for the assertion
    /// </summary>
    public bool HasRelevantActual { get; set; }
    
    /// <summary>
    /// Whether the key is relevant for the assertion
    /// </summary>
    public bool HasRelevantKey { get; set; }

    /// <summary>
    /// Whether the assertion is negated (e.g., ShouldNotBe instead of ShouldBe)
    /// </summary>
    public bool IsNegatedAssertion => ShouldMethod.Contains("Not");
    
    /// <summary>
    /// Custom message to display when the assertion fails
    /// </summary>
    public string? CustomMessage { get; set; }
    
    /// <summary>
    /// Filter expression for collection assertions
    /// </summary>
    public Expression? Filter { get; set; }
    
    /// <summary>
    /// The expected match count for collection assertions
    /// </summary>
    public int? MatchCount { get; set; }
    
    /// <summary>
    /// The sort direction for ordered collection assertions
    /// </summary>
    public SortDirection SortDirection { get; set; }
    
    /// <summary>
    /// The index of the out-of-order element in collection assertions
    /// </summary>
    public int OutOfOrderIndex { get; set; }
    
    /// <summary>
    /// The out-of-order object in collection assertions
    /// </summary>
    public object? OutOfOrderObject { get; set; }
    
    /// <summary>
    /// The path to the property being asserted
    /// </summary>
    public IEnumerable<string>? Path { get; set; }

    /// <summary>
    /// Initializes a new instance of the ShouldlyAssertionContext class
    /// </summary>
    /// <param name="shouldlyMethod">The name of the should method being called</param>
    /// <param name="expected">The expected value</param>
    /// <param name="actual">The actual value</param>
    /// <param name="stackTrace">The stack trace</param>
    public ShouldlyAssertionContext(
        string shouldlyMethod,
        object? expected = null,
        object? actual = null,
        StackTrace? stackTrace = null)
    {
        var actualCodeGetter = new ActualCodeTextGetter();
        Expected = expected;
        Actual = actual;
        ShouldMethod = shouldlyMethod;

        CodePart = actualCodeGetter.GetCodeText(actual, stackTrace);
        FileName = actualCodeGetter.FileName;
        LineNumber = actualCodeGetter.LineNumber;
    }
}