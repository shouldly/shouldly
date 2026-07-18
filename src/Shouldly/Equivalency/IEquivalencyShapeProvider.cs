namespace Shouldly.Equivalency;

/// <summary>
/// Supplies <see cref="EquivalencyTypeShape"/>s to the equivalency engine.
/// </summary>
internal interface IEquivalencyShapeProvider
{
    EquivalencyTypeShape GetShape(Type type);
}
