using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Shouldly.DifferenceHighlighting;
using Shouldly.MessageGenerators;

namespace Shouldly;

public class ExpectedShouldlyMessage : ShouldlyMessage
{
    public ExpectedShouldlyMessage(object? expected, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

public class ActualShouldlyMessage : ShouldlyMessage
{
    public ActualShouldlyMessage(object? actual, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, actual: actual)
        {
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

public class ExpectedActualShouldlyMessage : ShouldlyMessage
{
    public ExpectedActualShouldlyMessage(object? expected, object? actual, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected, actual)
        {
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

public class ActualFilteredWithPredicateShouldlyMessage : ShouldlyMessage
{
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

public class ExpectedActualWithCaseSensitivityShouldlyMessage : ShouldlyMessage
{
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

public class ExpectedActualToleranceShouldlyMessage : ShouldlyMessage
{
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

public class ExpectedActualIgnoreOrderShouldlyMessage : ShouldlyMessage
{
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

public class ExpectedActualKeyShouldlyMessage : ShouldlyMessage
{
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

public class ExpectedOrderShouldlyMessage : ShouldlyMessage
{
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

public class ExpectedEquivalenceShouldlyMessage : ShouldlyMessage
{
    public ExpectedEquivalenceShouldlyMessage(object? expected, object? actual, IEnumerable<string> path, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldlyAssertionContext(shouldlyMethod, expected, actual)
        {
            Path = path
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

public class ShouldlyThrowMessage : ShouldlyMessage
{
    public ShouldlyThrowMessage(object? expected, string exceptionMessage, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, null, exceptionMessage, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

    public ShouldlyThrowMessage(object? expected, object? actual, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, actual, shouldlyMethod: shouldlyMethod)
        {
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

    public ShouldlyThrowMessage(object? expected, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

public class ShouldContainWithCountShouldlyMessage : ShouldlyMessage
{
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

public class TaskShouldlyThrowMessage : ShouldlyMessage
{
    public TaskShouldlyThrowMessage(object? expected, Exception exception, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, null, exception.Message, isAsync: true, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

    public TaskShouldlyThrowMessage(object? expected, object? actual, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, actual, isAsync: true, shouldlyMethod: shouldlyMethod)
        {
            HasRelevantActual = true
        };
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

    public TaskShouldlyThrowMessage(object? expected, string? customMessage, [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, isAsync: true, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

public class CompleteInShouldlyMessage : ShouldlyMessage
{
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
/// Async methods need stacktrace before we get asynchronous
/// </summary>
public class AsyncShouldlyThrowShouldlyMessage : ShouldlyMessage
{
    public AsyncShouldlyThrowShouldlyMessage(Type exception, string? customMessage, StackTrace stackTrace,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(exception, stackTrace: stackTrace, isAsync: true, shouldlyMethod: shouldlyMethod);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }

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
/// Async methods need stacktrace before we get asynchronous
/// </summary>
public class AsyncShouldlyNotThrowShouldlyMessage : ShouldlyMessage
{
    public AsyncShouldlyNotThrowShouldlyMessage(Type exception, string? customMessage, StackTrace stackTrace, string exceptionMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        ShouldlyAssertionContext = new ShouldThrowAssertionContext(exception, stackTrace: stackTrace, isAsync: true, shouldlyMethod: shouldlyMethod, actual: null, exceptionMessage: exceptionMessage);
        if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage;
    }
}

public abstract class ShouldlyMessage
{
    private static readonly IEnumerable<ShouldlyMessageGenerator> ShouldlyMessageGenerators = new ShouldlyMessageGenerator[]
    {
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
        new ShouldBeWithinRangeTimeSpanMessageGenerator(),
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
    };

    protected IShouldlyAssertionContext ShouldlyAssertionContext { get; set; } = null!;

    public override string ToString()
    {
        var message = GenerateShouldMessage();
        if (ShouldlyAssertionContext.CustomMessage != null)
        {
            message += $@"

Additional Info:
    {ShouldlyAssertionContext.CustomMessage}";
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
            $@"{codePart}
    {shouldMethod} {conditionString}
{expected}
    but does{(isNegatedAssertion ? "" : " not")}";
    }

    private static string CreateActualVsExpectedMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var actual = context.Actual.ToStringAwesomely();
        var actualString = codePart == actual ? context.IsNegatedAssertion ? string.Empty : " not" : $@"
{actual}";

        var message =
            $@"{codePart}
    {context.ShouldMethod.PascalToSpaced()}
{context.Expected.ToStringAwesomely()}
    but was{actualString}";

        if (DifferenceHighlighter.CanHighlightDifferences(context))
        {
            message += $@"
    difference
{DifferenceHighlighter.HighlightDifferences(context)}";
        }

        return message;
    }
}