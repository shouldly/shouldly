namespace Shouldly.Equivalency;

/// <summary>
/// Resolves the shape provider the equivalency engine uses. In the box this is the reflection
/// provider, gated behind the "Shouldly.Equivalency.IsReflectionEnabledByDefault" feature switch
/// (the same pattern as System.Text.Json's reflection default): trimmed/AOT publishes can disable
/// the switch so the reflection path is removed entirely, and a companion source-generation
/// package can register an AOT-safe provider instead.
/// </summary>
internal static class EquivalencyShapes
{
    private const string ReflectionSwitchName = "Shouldly.Equivalency.IsReflectionEnabledByDefault";

    private static IEquivalencyShapeProvider? _customProvider;

    internal static bool IsReflectionEnabledByDefault =>
        !AppContext.TryGetSwitch(ReflectionSwitchName, out var enabled) || enabled;

    /// <summary>Registers a replacement provider (used by source-generated backends).</summary>
    internal static void SetProvider(IEquivalencyShapeProvider? provider) =>
        _customProvider = provider;

    internal static IEquivalencyShapeProvider Current =>
        _customProvider
        ?? (IsReflectionEnabledByDefault
            ? GetReflectionProvider()
            : throw new NotSupportedException(
                $"Reflection-based equivalency comparison is disabled (the \"{ReflectionSwitchName}\" feature switch is off) " +
                "and no equivalency shape provider is registered. Reference the Shouldly equivalency source-generation " +
                "package and register shapes for the compared types, or re-enable the feature switch."));

    [UnconditionalSuppressMessage("Trimming", "IL2026",
        Justification = "Guarded by the Shouldly.Equivalency.IsReflectionEnabledByDefault feature switch. " +
                        "Trimmed publishes that disable the switch stub this property to false (ILLink.Substitutions.xml), " +
                        "removing the reflection provider entirely; comparisons then require a registered provider.")]
    private static IEquivalencyShapeProvider GetReflectionProvider() =>
        ReflectionShapeProvider.Instance;
}
