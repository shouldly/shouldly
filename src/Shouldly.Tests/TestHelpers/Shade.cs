namespace Shouldly.Tests.TestHelpers;

/// <summary>
/// A value type with an equality rule (registered in <see cref="ModuleInitializer"/>) under which
/// <see cref="Red"/> and <see cref="Crimson"/> are equal. An enum, so the membership assertions would hash its values
/// if they ignored the rule.
/// </summary>
public enum Shade
{
    Red,
    Crimson,
    Blue
}

public static class ShadeEquality
{
    public static bool Equal(Shade expected, Shade actual) => Family(expected) == Family(actual);

    private static Shade Family(Shade shade) => shade == Shade.Crimson ? Shade.Red : shade;
}
