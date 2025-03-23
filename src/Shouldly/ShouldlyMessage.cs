using Shouldly.DifferenceHighlighting;
using Shouldly.MessageGenerators;

namespace Shouldly;

/// <summary>
/// Message for assertions that only have an expected value
/// </summary>
public class ExpectedShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with the expected value
    /// </summary>
    public ExpectedShouldlyMessage(object? expected, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that only have an actual value
/// </summary>
public class ActualShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with the actual value
    /// </summary>
    public ActualShouldlyMessage(object? actual, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, actual: actual)
        {
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that have both expected and actual values
/// </summary>
public class ExpectedActualShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with expected and actual values
    /// </summary>
    public ExpectedActualShouldlyMessage(object? expected, object? actual, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected, actual)
        {
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that filter actual values with a predicate
/// </summary>
public class ActualFilteredWithPredicateShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with a filter expression and result
    /// </summary>
    public ActualFilteredWithPredicateShouldlyMessage(Expression filter, object? result, object? actual, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, result, actual)
        {
            HasRelevantActual = true,
            Filter = filter
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that compare expected and actual values with case sensitivity options
/// </summary>
public class ExpectedActualWithCaseSensitivityShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with expected and actual values and case sensitivity setting
    /// </summary>
    public ExpectedActualWithCaseSensitivityShouldlyMessage(object? expected, object? actual,
        Case? caseSensitivity,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected, actual)
        {
            HasRelevantActual = true,
            CaseSensitivity = caseSensitivity
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that compare expected and actual values with a tolerance
/// </summary>
public class ExpectedActualToleranceShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with expected and actual values and a tolerance
    /// </summary>
    public ExpectedActualToleranceShouldlyMessage(object? expected, object? actual, object tolerance,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected, actual)
        {
            Tolerance = tolerance,
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that compare expected and actual values ignoring order
/// </summary>
public class ExpectedActualIgnoreOrderShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with expected and actual values that ignores order
    /// </summary>
    public ExpectedActualIgnoreOrderShouldlyMessage(object? expected, object? actual,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected, actual)
        {
            IgnoreOrder = true,
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that compare expected and actual values with a key
/// </summary>
public class ExpectedActualKeyShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with expected and actual values and a key
    /// </summary>
    public ExpectedActualKeyShouldlyMessage(object? expected, object? actual, object key,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected, actual)
        {
            Key = key,
            HasRelevantActual = true,
            HasRelevantKey = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that check the order of elements
/// </summary>
public class ExpectedOrderShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for order assertions
    /// </summary>
    public ExpectedOrderShouldlyMessage(object? actual, SortDirection expectedDirection, int outOfOrderIndex, object? outOfOrderObject,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, actual: actual)
        {
            SortDirection = expectedDirection,
            OutOfOrderIndex = outOfOrderIndex,
            OutOfOrderObject = outOfOrderObject
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that check object equivalence
/// </summary>
public class ExpectedEquivalenceShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for equivalence assertions
    /// </summary>
    public ExpectedEquivalenceShouldlyMessage(object? expected, object? actual, IEnumerable<string> path, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected, actual)
        {
            Path = path
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that check exceptions
/// </summary>
public class ShouldlyThrowMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for exception assertions with an exception message
    /// </summary>
    public ShouldlyThrowMessage(object? expected, string exceptionMessage, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, null, exceptionMessage, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

    /// <summary>
    /// Creates a new message for exception assertions with expected and actual values
    /// </summary>
    public ShouldlyThrowMessage(object? expected, object? actual, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, actual, shouldlyMethod: shouldlyMethod)
        {
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

    /// <summary>
    /// Creates a new message for exception assertions with only an expected value
    /// </summary>
    public ShouldlyThrowMessage(object? expected, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that check if a collection contains items with a specific count
/// </summary>
public class ShouldContainWithCountShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for containment assertions with a match count
    /// </summary>
    public ShouldContainWithCountShouldlyMessage(object? expected, object? actual, int matchCount, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected, actual)
        {
            HasRelevantActual = true,
            MatchCount = matchCount
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that check exceptions in asynchronous operations
/// </summary>
public class TaskShouldlyThrowMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for task exception assertions with an exception
    /// </summary>
    public TaskShouldlyThrowMessage(object? expected, Exception exception, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, null, exception.Message, isAsync: true, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

    /// <summary>
    /// Creates a new message for task exception assertions with expected and actual values
    /// </summary>
    public TaskShouldlyThrowMessage(object? expected, object? actual, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, actual, isAsync: true, shouldlyMethod: shouldlyMethod)
        {
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

    /// <summary>
    /// Creates a new message for task exception assertions with only an expected value
    /// </summary>
    public TaskShouldlyThrowMessage(object? expected, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, isAsync: true, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that check if an operation completes within a specified time
/// </summary>
public class CompleteInShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for completion time assertions
    /// </summary>
    public CompleteInShouldlyMessage(string what, TimeSpan timeout,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, what)
        {
            Timeout = timeout
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that check exceptions in asynchronous operations
/// </summary>
public class AsyncShouldlyThrowShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for async exception assertions with a stack trace
    /// </summary>
    public AsyncShouldlyThrowShouldlyMessage(Type exception, string? customMessage, StackTrace stackTrace,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(exception, stackTrace: stackTrace, isAsync: true, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

    /// <summary>
    /// Creates a new message for async exception assertions with expected and actual types
    /// </summary>
    public AsyncShouldlyThrowShouldlyMessage(Type expected, Type actual, string? customMessage, StackTrace stackTrace)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, actual, stackTrace: stackTrace, isAsync: true)
        {
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Message for assertions that verify asynchronous operations do not throw exceptions
/// </summary>
public class AsyncShouldlyNotThrowShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for async not-throw assertions
    /// </summary>
    public AsyncShouldlyNotThrowShouldlyMessage(Type exception, string? customMessage, StackTrace stackTrace, string exceptionMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(exception, stackTrace: stackTrace, isAsync: true, shouldlyMethod: shouldlyMethod, actual: null, exceptionMessage: exceptionMessage);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

/// <summary>
/// Base class for all Shouldly assertion messages
/// </summary>
public abstract class ShouldlyMessage
{
    private static readonly IEnumerable<ShouldlyMessageGenerator> ShouldlyMessageGenerators =
    [
        new ShouldHaveFlagMessageGenerator(),
        new ShouldNotHaveFlagMessageGenerator(),
        new ShouldBeNullOrEmptyMessageGenerator(),
        new ShouldBeEmptyMessageGenerator(),
        new ShouldAllBeMessageGenerator(),
        new DynamicShouldMessageGenerator(),
        new ShouldCompleteInMessageGenerator(),
        new ShouldBeNullOrWhiteSpaceMessageGenerator(),
        new DictionaryShouldContainKeyAndValueMessageGenerator(),
        new DictionaryShouldOrNotContainKeyMessageGenerator(),
        new DictionaryShouldNotContainValueForKeyMessageGenerator(),
        new ShouldBeginEndWithMessageGenerator(),
        new ShouldBeWithinRangeMessageGenerator(),
        new ShouldContainWithinRangeMessageGenerator(),
        new ShouldBeUniqueMessageGenerator(),
        new ShouldBeEnumerableCaseSensitiveMessageGenerator(),
        new ShouldContainMessageGenerator(),
        new ShouldContainPredicateMessageGenerator(),
        new ShouldBeIgnoringOrderMessageGenerator(),
        new ShouldSatisfyAllConditionsMessageGenerator(),
        new ShouldBeSubsetOfMessageGenerator(),
        new ShouldHaveSingleItemMessageGenerator(),
        new ShouldBeBooleanMessageGenerator(),
        new ShouldNotThrowMessageGenerator(),
        new ShouldNotMatchMessageGenerator(),
        new ShouldThrowMessageGenerator(),
        new ShouldBeNullMessageGenerator(),
        new ShouldBeMessageGenerator(),
        new ShouldBePositiveMessageGenerator(),
        new ShouldBeNegativeMessageGenerator(),
        new ShouldBeTypeMessageGenerator(),
        new ShouldBeInOrderMessageGenerator(),
        new ShouldBeEquivalentToMessageGenerator()
    ];

    protected IShouldlyAssertionContext ShouldlyAssertionContext { get; set; } = null!;

    /// <summary>
    /// Converts the message to a string representation
    /// </summary>
    public override string ToString()
    {
        var message = GenerateShouldMessage();
        if (ShouldlyAssertionContext.CustomMessage != null)
        {
            message += $"""


                        Additional Info:
                            {ShouldlyAssertionContext.CustomMessage}
                        """;
        }

        return message;
    }

    private string GenerateShouldMessage()
    {
        var messageGenerator = ShouldlyMessageGenerators.FirstOrDefault(x => x.CanProcess(ShouldlyAssertionContext));
        if (messageGenerator != null)
        {
            var message = messageGenerator.GenerateErrorMessage(ShouldlyAssertionContext);
            return message;
        }

        if (ShouldlyAssertionContext.HasRelevantActual)
        {
            return CreateActualVsExpectedMessage(ShouldlyAssertionContext);
        }

        return CreateExpectedErrorMessage();
    }

    private string CreateExpectedErrorMessage()
    {
        var codePart = ShouldlyAssertionContext.CodePart;
        var isNegatedAssertion = ShouldlyAssertionContext.ShouldMethod.Contains("Not");

        var shouldMethod = ShouldlyAssertionContext.ShouldMethod.PascalToSpaced();
        var expected = ShouldlyAssertionContext.Expected.ToStringAwesomely();
        var conditionString = ShouldlyAssertionContext.Expected is BinaryExpression
            ? "an element satisfying the condition"
            : "";
        return
            $"""
             {codePart}
                 {shouldMethod} {conditionString}
             {expected}
                 but does{(isNegatedAssertion ? "" : " not")}
             """;
    }

    private static string CreateActualVsExpectedMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var actual = context.Actual.ToStringAwesomely();
        var actualString = codePart == actual ? context.IsNegatedAssertion ? string.Empty : " not" : $"""

             {actual}
             """;

        var message =
            $"""
             {codePart}
                 {context.ShouldMethod.PascalToSpaced()}
             {context.Expected.ToStringAwesomely()}
                 but was{actualString}
             """;

        if (DifferenceHighlighter.CanHighlightDifferences(context))
        {
            message += $"""
                        
                            difference
                        {DifferenceHighlighter.HighlightDifferences(context)}
                        """;
        }

        return message;
    }
}