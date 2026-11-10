using Shouldly.Internals;

namespace Shouldly;

/// <summary>
/// Core extension methods for Shouldly assertions
/// </summary>
public static class ShouldlyCoreExtensions
{
    internal static bool IsShouldlyMethod(this MethodBase method)
    {
        if (method.DeclaringType == null)
            return false;

        return method.DeclaringType.GetCustomAttributes(typeof(ShouldlyMethodsAttribute), true).Any()
               || method.DeclaringType.DeclaringType != null && method.DeclaringType.DeclaringType.GetCustomAttributes(typeof(ShouldlyMethodsAttribute), true).Any();
    }

    /// <summary>
    /// Required to support the <see cref="DynamicShould.HaveProperty"/> method that takes
    /// in a <see langword="dynamic"/> as a parameter. Having a method that takes a dynamic really stuffs up the
    /// stack trace because the runtime binder has to inject a whole heap of methods. Our normal way of just taking
    /// the next frame doesn't work. The following two lines seem to work for now, but this feels like a hack. The
    /// conditions to be able to walk up stack trace until we get to the calling method might have to be updated
    /// regularly as we find more scenarios. Alternately, it could be replaced with a more robust implementation.
    /// </summary>
    internal static bool IsSystemDynamicMachinery(this MethodBase method)
    {
        var declaringType = method.DeclaringType;
        return declaringType is null ||
               (declaringType.FullName?.StartsWith("System.Dynamic", StringComparison.Ordinal) ?? false);
    }

    /// <summary>
    /// Asserts that the specified constraint is satisfied
    /// </summary>
    /// <typeparam name="T">The type of the actual value</typeparam>
    /// <param name="actual">The actual value</param>
    /// <param name="specifiedConstraint">The constraint to check</param>
    /// <param name="originalActual">The original actual value</param>
    /// <param name="originalExpected">The original expected value</param>
    /// <param name="customMessage">A custom message to display if the assertion fails</param>
    /// <param name="shouldlyMethod">The name of the shouldly method being called</param>
    /// <param name="actualExpression">The source-level expression of the actual argument captured at the call site via <see cref="CallerArgumentExpressionAttribute"/>.</param>
    public static void AssertAwesomely<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected,
        string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(new ExpectedActualShouldlyMessage(originalExpected, originalActual, customMessage, shouldlyMethod, actualExpression).ToString());
    }

    /// <summary>
    /// Asserts that the specified constraint is satisfied with case sensitivity
    /// </summary>
    /// <typeparam name="T">The type of the actual value</typeparam>
    /// <param name="actual">The actual value</param>
    /// <param name="specifiedConstraint">The constraint to check</param>
    /// <param name="originalActual">The original actual value</param>
    /// <param name="originalExpected">The original expected value</param>
    /// <param name="caseSensitivity">The case sensitivity to use for string comparisons</param>
    /// <param name="customMessage">A custom message to display if the assertion fails</param>
    /// <param name="shouldlyMethod">The name of the shouldly method being called</param>
    /// <param name="actualExpression">The source-level expression of the actual argument captured at the call site via <see cref="CallerArgumentExpressionAttribute"/>.</param>
    public static void AssertAwesomelyWithCaseSensitivity<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected,
        Case caseSensitivity, string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        var message = new ExpectedActualWithCaseSensitivityShouldlyMessage(originalExpected, originalActual, caseSensitivity, customMessage, shouldlyMethod, actualExpression);
        throw new ShouldAssertException(message.ToString());
    }

    /// <summary>
    /// Asserts that the specified constraint is satisfied ignoring the order of elements
    /// </summary>
    /// <typeparam name="T">The type of the actual value</typeparam>
    /// <param name="actual">The actual value</param>
    /// <param name="specifiedConstraint">The constraint to check</param>
    /// <param name="originalActual">The original actual value</param>
    /// <param name="originalExpected">The original expected value</param>
    /// <param name="customMessage">A custom message to display if the assertion fails</param>
    /// <param name="shouldlyMethod">The name of the shouldly method being called</param>
    /// <param name="actualExpression">The source-level expression of the actual argument captured at the call site via <see cref="CallerArgumentExpressionAttribute"/>.</param>
    public static void AssertAwesomelyIgnoringOrder<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected,
        string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(new ExpectedActualIgnoreOrderShouldlyMessage(originalExpected, originalActual, customMessage, shouldlyMethod, actualExpression).ToString());
    }

    /// <summary>
    /// Asserts that two enumerables are equal ignoring order, comparing elements with <paramref name="comparer"/>;
    /// the failure message reports missing elements using the same comparer. Pass <paramref name="isCustomComparer"/>
    /// as <c>true</c> when the caller supplied <paramref name="comparer"/>.
    /// </summary>
    internal static void AssertAwesomelyIgnoringOrder<T>(
        this IEnumerable<T>? actual, IEnumerable<T>? expected, IEqualityComparer<T> comparer, bool isCustomComparer,
        string? customMessage, string shouldlyMethod, string? actualExpression)
    {
        try
        {
            if (Is.EqualIgnoreOrder(actual, expected, comparer)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        var message = new ExpectedActualIgnoreOrderShouldlyMessage(
            ComparisonRecord.ComparedElementsWith(comparer, isCustomComparer), expected, actual, customMessage, shouldlyMethod, actualExpression);
        throw new ShouldAssertException(message.ToString());
    }

    /// <summary>
    /// Asserts that two enumerables are equal in order, comparing elements with a caller-supplied
    /// <paramref name="comparer"/>; the failure message highlights differences using the same comparer.
    /// </summary>
    internal static void AssertAwesomelyWithComparer<T>(
        this IEnumerable<T>? actual, IEnumerable<T>? expected, IEqualityComparer<T> comparer,
        string? customMessage, string shouldlyMethod, string? actualExpression)
    {
        try
        {
            if (Is.Equal(actual, expected, comparer)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        var message = new ExpectedActualShouldlyMessage(
            ComparisonRecord.ComparedElementsWith(comparer, isCustom: true), expected, actual, customMessage, shouldlyMethod, actualExpression);
        throw new ShouldAssertException(message.ToString());
    }

    /// <summary>
    /// Asserts that the specified constraint is satisfied with a tolerance
    /// </summary>
    /// <typeparam name="T">The type of the actual value</typeparam>
    /// <param name="actual">The actual value</param>
    /// <param name="specifiedConstraint">The constraint to check</param>
    /// <param name="originalActual">The original actual value</param>
    /// <param name="originalExpected">The original expected value</param>
    /// <param name="tolerance">The tolerance for numeric comparisons</param>
    /// <param name="customMessage">A custom message to display if the assertion fails</param>
    /// <param name="shouldlyMethod">The name of the shouldly method being called</param>
    /// <param name="actualExpression">The source-level expression of the actual argument captured at the call site via <see cref="CallerArgumentExpressionAttribute"/>.</param>
    public static void AssertAwesomely<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected, object tolerance,
        string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(originalExpected, originalActual, tolerance, customMessage, shouldlyMethod, actualExpression).ToString());
    }

    /// <summary>
    /// Asserts that the specified constraint is satisfied with case sensitivity
    /// </summary>
    /// <typeparam name="T">The type of the actual value</typeparam>
    /// <param name="actual">The actual value</param>
    /// <param name="specifiedConstraint">The constraint to check</param>
    /// <param name="originalActual">The original actual value</param>
    /// <param name="originalExpected">The original expected value</param>
    /// <param name="caseSensitivity">The case sensitivity to use for string comparisons</param>
    /// <param name="customMessage">A custom message to display if the assertion fails</param>
    /// <param name="shouldlyMethod">The name of the shouldly method being called</param>
    /// <param name="actualExpression">The source-level expression of the actual argument captured at the call site via <see cref="CallerArgumentExpressionAttribute"/>.</param>
    public static void AssertAwesomely<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected, Case caseSensitivity,
        string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(new ExpectedActualWithCaseSensitivityShouldlyMessage(originalExpected, originalActual, caseSensitivity, customMessage, shouldlyMethod, actualExpression).ToString());
    }
}