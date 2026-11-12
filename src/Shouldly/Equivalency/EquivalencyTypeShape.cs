namespace Shouldly.Equivalency;

/// <summary>
/// The engine's view of a type: its comparison strategy plus read-only accessors for members,
/// elements, and dictionary entries. The in-box implementation is reflection-based
/// (<see cref="ReflectionShapeProvider"/>); the abstraction exists so a source-generated,
/// AOT-safe provider can be swapped in without touching the engine.
/// </summary>
internal abstract class EquivalencyTypeShape
{
    public abstract Type Type { get; }

    public abstract EquivalencyNodeKind Kind { get; }

    /// <summary>
    /// Public instance fields and readable, non-indexer properties: all of them for a Complex node, and for a
    /// Dictionary, Set or Sequence node only the state it holds beyond its items: the members a type adds to the
    /// framework collection class it derives from, and a grouping's key (#1431).
    /// </summary>
    public abstract IReadOnlyList<EquivalencyMemberShape> Members { get; }

    /// <summary>
    /// Finds the member an expectation asks for on a value of this type. For a collection type this looks beyond
    /// <see cref="Members"/>, at every public member: which of its own members a collection is compared by is a
    /// question for the expectation's type, not for the type of the value it is compared with.
    /// </summary>
    public abstract EquivalencyMemberShape? FindMember(string name);

    /// <summary>
    /// The declared element type of a Sequence or Set, when statically known. Like <see cref="KeyType"/>,
    /// <see cref="ValueType"/> and <see cref="EquivalencyMemberShape.DeclaredType"/>, it doesn't decide how an
    /// element is compared: an element is compared by the shape of its own type, and this is passed to
    /// <see cref="IEquivalencyShapeProvider.GetShape"/> alongside it.
    /// </summary>
    public abstract Type? ElementType { get; }

    /// <summary>The declared key type of a Dictionary, when statically known.</summary>
    public abstract Type? KeyType { get; }

    /// <summary>The declared value type of a Dictionary, when statically known.</summary>
    public abstract Type? ValueType { get; }

    /// <summary>Enumerates the entries of a Dictionary instance of this shape's type.</summary>
    public abstract IEnumerable<KeyValuePair<object?, object?>> GetEntries(object instance);
}

/// <summary>A single readable member (public instance field or property) of a Complex node.</summary>
internal abstract class EquivalencyMemberShape
{
    public abstract string Name { get; }

    /// <summary>The member's declared (compile-time) type (see <see cref="EquivalencyTypeShape.ElementType"/>).</summary>
    public abstract Type DeclaredType { get; }

    public abstract object? GetValue(object instance);
}
