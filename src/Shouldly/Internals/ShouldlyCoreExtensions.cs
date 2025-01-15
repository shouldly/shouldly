namespace Shouldly;

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

    public static void AssertAwesomely<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected,
        string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(new ExpectedActualShouldlyMessage(originalExpected, originalActual, customMessage, shouldlyMethod).ToString());
    }

    public static void AssertAwesomelyWithCaseSensitivity<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected,
        Case caseSensitivity, string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        var message = new ExpectedActualWithCaseSensitivityShouldlyMessage(originalExpected, originalActual, caseSensitivity, customMessage, shouldlyMethod);
        throw new ShouldAssertException(message.ToString());
    }

    public static void AssertAwesomelyIgnoringOrder<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected,
        string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(new ExpectedActualIgnoreOrderShouldlyMessage(originalExpected, originalActual, customMessage, shouldlyMethod).ToString());
    }

    public static void AssertAwesomely<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected, object tolerance,
        string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(originalExpected, originalActual, tolerance, customMessage, shouldlyMethod).ToString());
    }

    public static void AssertAwesomely<T>(
        this T actual, Func<T, bool> specifiedConstraint,
        object? originalActual, object? originalExpected, Case caseSensitivity,
        string? customMessage = null,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            if (specifiedConstraint(actual)) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(new ExpectedActualWithCaseSensitivityShouldlyMessage(originalExpected, originalActual, caseSensitivity, customMessage, shouldlyMethod).ToString());
    }
}