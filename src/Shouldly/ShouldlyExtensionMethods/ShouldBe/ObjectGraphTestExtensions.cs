using System.ComponentModel;
using Shouldly.Equivalency;

namespace Shouldly;

/// <summary>
/// Extension methods for object graph comparison assertions
/// </summary>
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ObjectGraphTestExtensions
{
    /// <summary>
    /// Asserts that an object is equivalent to another object: leaf values (primitives, strings,
    /// dates, and other value-semantic types) are compared with Equals, with float and double values honouring
    /// <see cref="ShouldlyConfiguration.DefaultFloatingPointTolerance"/> as ShouldBe does; dictionaries are matched
    /// by key, sets order-insensitively, other collections element-by-element in order (or in any order against a
    /// dictionary or set that keeps no order of its own), and
    /// everything else member-wise over the expectation's public properties and fields, recursively.
    /// Each expected value is compared as the type it is, not as the type its member or collection declares.
    /// Two values covered by an equality rule (<see cref="ShouldlyConfiguration.UseEquality{T}(Func{T, T, bool})"/>)
    /// are compared by the rule, as one value.
    /// All differences are collected and reported with their paths.
    /// </summary>
    public static void ShouldBeEquivalentTo(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        EquivalencyComparison.Assert(actual, expected, new(), customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that an object is equivalent to another object, using the supplied
    /// <paramref name="options"/> to customize the comparison.
    /// </summary>
    public static void ShouldBeEquivalentTo(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        EquivalencyOptions options,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        EquivalencyComparison.Assert(actual, expected, options, customMessage, actualExpression);
    }
}
