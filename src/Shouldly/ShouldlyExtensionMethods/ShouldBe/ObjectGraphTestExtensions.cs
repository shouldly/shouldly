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
    /// dates, and other value-semantic types) are compared with Equals, dictionaries are matched
    /// by key, sets order-insensitively, other collections element-by-element in order, and
    /// everything else member-wise over the expectation's public properties and fields, recursively.
    /// All differences are collected and reported with their paths.
    /// </summary>
    public static void ShouldBeEquivalentTo(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        EquivalencyComparison.Assert(actual, expected, new(), customMessage, "ShouldBeEquivalentTo", actualExpression);
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
        EquivalencyComparison.Assert(actual, expected, options, customMessage, "ShouldBeEquivalentTo", actualExpression);
    }
}
