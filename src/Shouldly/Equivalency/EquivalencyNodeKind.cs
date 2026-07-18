namespace Shouldly.Equivalency;

/// <summary>
/// The comparison strategy for a node in the object graph, selected from the node's type.
/// </summary>
internal enum EquivalencyNodeKind
{
    /// <summary>Compared with <see cref="object.Equals(object)"/> (plus lossless cross-numeric equality).</summary>
    Leaf,

    /// <summary>Matched by key; values compared recursively.</summary>
    Dictionary,

    /// <summary>Order-insensitive element matching.</summary>
    Set,

    /// <summary>Order-sensitive element-by-element comparison; container type ignored.</summary>
    Sequence,

    /// <summary>Member-wise recursive comparison over public properties and fields.</summary>
    Complex,
}
