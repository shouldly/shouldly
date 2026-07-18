namespace Shouldly.Equivalency;

internal enum EquivalencyDifferenceKind
{
    /// <summary>The values at the path are not equal.</summary>
    ValueMismatch,

    /// <summary>A member present on the expectation does not exist on the actual object.</summary>
    MissingMember,

    /// <summary>A key present in the expected dictionary was not found in the actual dictionary.</summary>
    MissingKey,

    /// <summary>The actual dictionary contains keys the expectation does not.</summary>
    UnexpectedKeys,

    /// <summary>No element in the actual collection is equivalent to an expected element (order-insensitive).</summary>
    MissingElement,

    /// <summary>The comparison selected zero members, which would make the assertion vacuously pass.</summary>
    VacuousComparison,
}

/// <summary>
/// One difference found while comparing two object graphs, anchored to a path from the root.
/// </summary>
internal sealed class EquivalencyDifference
{
    public EquivalencyDifference(EquivalencyDifferenceKind kind, IReadOnlyList<string> pathSegments, object? expected, object? actual, string? detail = null)
    {
        Kind = kind;
        PathSegments = pathSegments;
        Expected = expected;
        Actual = actual;
        Detail = detail;
    }

    public EquivalencyDifferenceKind Kind { get; }

    /// <summary>Path segments from the root, each annotated with the compared type where known.</summary>
    public IReadOnlyList<string> PathSegments { get; }

    public object? Expected { get; }

    public object? Actual { get; }

    /// <summary>Kind-specific detail, e.g. the type name a member was missing on.</summary>
    public string? Detail { get; }
}
