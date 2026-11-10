using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;

namespace Shouldly.Internals;

/// <summary>
/// Calls an <see cref="IEquatable{T}"/> implementation chosen from the runtime types of two values, for comparisons
/// where the static type has been lost — the elements of a collection compared through the object comparer, or a
/// <c>ShouldBe&lt;object&gt;</c>. Without it, an element type that implements <see cref="IEquatable{T}"/> without
/// overriding <see cref="object.Equals(object)"/> (CA1067) falls back to reference equality there, while the same
/// elements compare equal wherever the element type is statically known (issue #825).
/// </summary>
static class RuntimeEquatable
{
    private static readonly ConcurrentDictionary<(Type Receiver, Type Argument), MethodInfo?> EqualsMethods = new();

    /// <summary>
    /// When <paramref name="x"/>'s runtime type implements <see cref="IEquatable{T}"/> for a <c>T</c> that
    /// <paramref name="y"/> is an instance of, sets <paramref name="result"/> to <c>x.Equals(y)</c> through that
    /// implementation and returns <c>true</c>. When several apply, the most derived <c>T</c> wins.
    /// </summary>
    public static bool TryEquals(object x, object y, out bool result)
    {
        var equals = EqualsMethods.GetOrAdd((x.GetType(), y.GetType()), static types => FindEquals(types.Receiver, types.Argument));
        if (equals is null)
        {
            result = false;
            return false;
        }

        try
        {
            result = (bool)equals.Invoke(x, [y])!;
        }
        catch (TargetInvocationException e) when (e.InnerException is not null)
        {
            // Surface the user's own exception, as a direct call to Equals would.
            ExceptionDispatchInfo.Capture(e.InnerException).Throw();
            throw;
        }

        return true;
    }

    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Reads the interfaces of a type that has a live instance. If trimming removed an IEquatable<T> implementation, no Equals is found and the comparison falls back to object.Equals, as it did before.")]
    [UnconditionalSuppressMessage("Trimming", "IL2075",
        Justification = "IEquatable<T>.Equals(T) is the interface's only member; an implementation the trimmer kept keeps it.")]
    private static MethodInfo? FindEquals(Type receiver, Type argument)
    {
        Type? bestArgument = null;
        MethodInfo? best = null;

        foreach (var @interface in receiver.GetInterfaces())
        {
            if (!@interface.IsGenericType || @interface.GetGenericTypeDefinition() != typeof(IEquatable<>))
                continue;

            var equatableTo = @interface.GenericTypeArguments[0];
            if (!equatableTo.IsAssignableFrom(argument))
                continue;

            if (bestArgument is null || bestArgument.IsAssignableFrom(equatableTo))
            {
                bestArgument = equatableTo;
                best = @interface.GetMethod(nameof(IEquatable<object>.Equals));
            }
        }

        return best;
    }
}
