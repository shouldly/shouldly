namespace Shouldly.Equivalency;

/// <summary>
/// The comparison strategy for a node in the object graph, selected from the node's type.
/// </summary>
internal enum EquivalencyNodeKind
{
    /// <summary>
    /// Compared with <see cref="object.Equals(object)"/> on the expected value, plus lossless cross-numeric equality;
    /// <see cref="float"/>/<see cref="double"/> values honour <see cref="ShouldlyConfiguration.DefaultFloatingPointTolerance"/>.
    /// Two <c>System.Text.Json</c> values are compared as JSON.
    /// </summary>
    Leaf,

    /// <summary>Matched by key; values compared recursively.</summary>
    Dictionary,

    /// <summary>
    /// Order-insensitive element matching, except against a Sequence when the set keeps an order of its own
    /// (see <see cref="EquivalencyTypeShape.KeepsAnOrder"/>).
    /// </summary>
    Set,

    /// <summary>
    /// Order-sensitive element-by-element comparison; container type ignored. Against a Set or Dictionary that
    /// keeps no order of its own, order-insensitive.
    /// </summary>
    Sequence,

    /// <summary>Member-wise recursive comparison over public properties and fields.</summary>
    Complex,
}
