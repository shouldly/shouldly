using System.Reflection;
using System.Runtime.CompilerServices;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace Shouldly.Tests.ConventionTests;

/// <summary>
/// Verifies that public Shouldly methods accept an optional
/// <c>string? actualExpression</c> parameter annotated with
/// <see cref="CallerArgumentExpressionAttribute"/> targeting the receiver, so the
/// compiler injects the call-site expression and the assertion message
/// shows the user's literal source text without stack-trace parsing.
///
/// Exempt:
///  - Methods with a <c>params</c> array as the last parameter (the convention is
///    incompatible with params arrays since they must be last; the receiver
///    expression falls back to stack-trace parsing for these overloads).
///  - The private <c>ExecuteAssertion</c> / <c>CompleteInInternal</c> helpers
///    and other non-public methods are out of scope (we operate on public surface).
/// </summary>
public class ShouldlyMethodsShouldHaveActualExpressionParameter : IConvention<Types>
{
    public void Execute(Types data, IConventionResultContext result)
    {
        var failingMethods =
            from type in data
            from method in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            where !ShouldSkip(method)
            where !HasActualExpressionParameter(method)
            orderby method.DeclaringType?.Name, method.Name
            select FormatKey(method);

        result.Is(
            "The following Shouldly methods are missing an [CallerArgumentExpression]-decorated actualExpression parameter",
            failingMethods);
    }

    private static bool ShouldSkip(MethodInfo method)
    {
        if (method.DeclaringType?.Namespace != "Shouldly") return true;
        if (typeof(object).GetMethods().Any(m => m.Name == method.Name)) return true;

        // Methods with `params` arrays cannot be CAE-decorated on the receiver — the optional
        // expression parameter has to come after `params`, which the language forbids.
        var parameters = method.GetParameters();
        if (parameters.Length > 0 && parameters[^1].IsDefined(typeof(ParamArrayAttribute), inherit: false))
            return true;

        // Deliberate exemptions: short forwarding overloads where adding [CallerArgumentExpression]
        // would shadow the customMessage slot at positional call sites (e.g. xs.ShouldBe(ys, false, "msg")
        // would bind "msg" to actualExpression). These forward to a CAE-decorated 4-arg overload, so
        // user-facing expression capture still works via that path or via the stack-walk fallback.
        var key = $"{method.DeclaringType?.Name}.{method.Name}({string.Join(",", parameters.Select(p => p.ParameterType.Name))})";
        return ExemptForwardingOverloads.Contains(key);
    }

    private static readonly HashSet<string> ExemptForwardingOverloads =
    [
        "ShouldBeTestExtensions.ShouldBe(IEnumerable`1,IEnumerable`1,Boolean)",
        "ShouldBeEnumerableTestExtensions.ShouldBeUnique(IEnumerable`1,IEqualityComparer`1)",
    ];

    private static bool HasActualExpressionParameter(MethodInfo method) =>
        method.GetParameters().Any(p =>
            p.Name == "actualExpression" &&
            p.ParameterType == typeof(string) &&
            p.IsDefined(typeof(CallerArgumentExpressionAttribute), inherit: false));

    private static string FormatKey(MethodInfo method) =>
        $"{method.DeclaringType?.Name}.{method.FormatMethod()}";

    public string ConventionReason => "Call-site expression capture must be available on every public Shouldly assertion.";
}
