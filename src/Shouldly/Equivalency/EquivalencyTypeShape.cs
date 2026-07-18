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

    /// <summary>Public instance fields and readable, non-indexer properties (Complex nodes).</summary>
    public abstract IReadOnlyList<EquivalencyMemberShape> Members { get; }

    public abstract EquivalencyMemberShape? FindMember(string name);

    /// <summary>The declared element type of a Sequence or Set, when statically known.</summary>
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

    /// <summary>The member's declared (compile-time) type, which drives the child node's comparison strategy.</summary>
    public abstract Type DeclaredType { get; }

    public abstract object? GetValue(object instance);
}
