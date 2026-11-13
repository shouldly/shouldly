using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;

namespace Shouldly.Internals;

/// <summary>
/// Calls an <see cref="IEquatable{T}"/> or <see cref="IComparable{T}"/> implementation chosen from the runtime types of
/// two values, for comparisons where the static type has been lost: the elements of a collection compared through the
/// object comparer, the items of a tuple, or a <c>ShouldBe&lt;object&gt;</c>. Without it, a type that implements
/// <see cref="IEquatable{T}"/> without overriding <see cref="object.Equals(object)"/> (CA1067, issue #825), or that is
/// equal only through <see cref="IComparable{T}"/> of an interface or a base type, falls back to reference equality
/// there, while the same values compare equal wherever their type is statically known.
/// </summary>
static class RuntimeEquality
{
    private static readonly ConcurrentDictionary<(Type Receiver, Type Argument), MethodInfo?> EqualsMethods = new();
    private static readonly ConcurrentDictionary<(Type Receiver, Type Argument), MethodInfo?> CompareToMethods = new();

    /// <summary>
    /// When <paramref name="x"/>'s runtime type implements <see cref="IEquatable{T}"/> for a <c>T</c> that
    /// <paramref name="y"/> is an instance of, sets <paramref name="result"/> to <c>x.Equals(y)</c> through that
    /// implementation and returns <c>true</c>. When several apply, the most derived <c>T</c> wins.
    /// </summary>
    public static bool TryEquals(object x, object y, out bool result)
    {
        var equals = EqualsMethods.GetOrAdd((x.GetType(), y.GetType()),
            static types => FindMethod(types.Receiver, types.Argument, typeof(IEquatable<>)));
        result = equals is not null && (bool)Invoke(equals, x, y);
        return equals is not null;
    }

    /// <summary>
    /// When <paramref name="x"/>'s runtime type implements <see cref="IComparable{T}"/> for a <c>T</c> that
    /// <paramref name="y"/> is an instance of, sets <paramref name="result"/> to whether <c>x.CompareTo(y)</c> through
    /// that implementation returns 0, and returns <c>true</c>. When several apply, the most derived <c>T</c> wins.
    /// </summary>
    public static bool TryCompareTo(object x, object y, out bool result)
    {
        var compareTo = CompareToMethods.GetOrAdd((x.GetType(), y.GetType()),
            static types => FindMethod(types.Receiver, types.Argument, typeof(IComparable<>)));
        result = compareTo is not null && (int)Invoke(compareTo, x, y) == 0;
        return compareTo is not null;
    }

    private static object Invoke(MethodInfo method, object receiver, object argument)
    {
        try
        {
            return method.Invoke(receiver, [argument])!;
        }
        catch (TargetInvocationException e) when (e.InnerException is not null)
        {
            // Surface the user's own exception, as a direct call would.
            ExceptionDispatchInfo.Capture(e.InnerException).Throw();
            throw;
        }
    }

    // Without these, NativeAOT drops the interfaces' method metadata and GetMethod finds nothing.
#if NET5_0_OR_GREATER
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(IEquatable<>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(IComparable<>))]
#endif
    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Reads the interfaces of a type that has a live instance. If trimming removed an implementation, none is found and the comparison falls back to the steps after it.")]
    [UnconditionalSuppressMessage("Trimming", "IL2075",
        Justification = "IEquatable<T>.Equals and IComparable<T>.CompareTo are rooted by the DynamicDependency attributes above.")]
    private static MethodInfo? FindMethod(Type receiver, Type argument, Type openInterface)
    {
        Type? bestArgument = null;
        MethodInfo? best = null;

        foreach (var @interface in receiver.GetInterfaces())
        {
            if (!@interface.IsGenericType || @interface.GetGenericTypeDefinition() != openInterface)
                continue;

            var to = @interface.GenericTypeArguments[0];
            if (!to.IsAssignableFrom(argument))
                continue;

            if (bestArgument is null || bestArgument.IsAssignableFrom(to))
            {
                bestArgument = to;
                best = @interface.GetMethod(openInterface == typeof(IEquatable<>)
                    ? nameof(IEquatable<object>.Equals)
                    : nameof(IComparable<object>.CompareTo));
            }
        }

        return best;
    }
}
