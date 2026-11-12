namespace Shouldly.Equivalency;

/// <summary>
/// Supplies <see cref="EquivalencyTypeShape"/>s to the equivalency engine.
/// </summary>
internal interface IEquivalencyShapeProvider
{
    /// <summary>
    /// The shape to compare a value of <paramref name="type"/> by, or null when the provider has none.
    /// </summary>
    /// <param name="type">The value's own type: what <see cref="object.GetType"/> returns for it.</param>
    /// <param name="declaredType">
    /// The type the member or collection holding the value declares it as, when the engine knows it. The reflection
    /// provider can shape any type and ignores it. A provider with a closed set of shapes may use it to shape a value
    /// whose own type it can't know ahead of time, when that loses nothing: a LINQ iterator or a compiler-generated
    /// collection, held as an <c>IEnumerable&lt;T&gt;</c>, is no more than its items. It must not use it to compare
    /// a value by fewer members than the value's own type has; for such a type it returns null, and the assertion
    /// throws.
    /// </param>
    EquivalencyTypeShape? GetShape(Type type, Type? declaredType = null);
}
