using DiffEngine;

namespace Shouldly;

/// <summary>
/// Extension methods for ShouldMatchConfigurationBuilder to configure DiffEngine integration
/// </summary>
public static class ShouldMatchConfigurationBuilderExtensions
{
    /// <summary>
    /// Configures the ShouldMatchConfiguration to use DiffEngine for file comparison
    /// </summary>
    /// <param name="builder">The configuration builder to extend</param>
    /// <returns>The configuration builder for method chaining</returns>
    public static ShouldMatchConfigurationBuilder ConfigureDiffEngine(this ShouldMatchConfigurationBuilder builder)
        => builder.Configure(configuration =>
        {
            configuration.DiffViewer = DiffEngineDiffViewer.Instance;
            if (DiffRunner.Disabled)
                configuration.PreventDiff = true;
        });

    /// <summary>
    /// Configures the ShouldMatchConfiguration to use DiffEngine and ensures diff is enabled
    /// </summary>
    /// <param name="builder">The configuration builder to extend</param>
    /// <returns>The configuration builder for method chaining</returns>
    public static ShouldMatchConfigurationBuilder Diff(this ShouldMatchConfigurationBuilder builder)
        => builder.ConfigureDiffEngine().Configure(c => c.PreventDiff = false);
}
