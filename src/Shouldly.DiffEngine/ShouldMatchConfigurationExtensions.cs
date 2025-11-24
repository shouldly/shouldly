using DiffEngine;

namespace Shouldly;

/// <summary>
/// Extension methods for ShouldMatchConfigurationBuilder to configure DiffEngine integration
/// </summary>
public static class ShouldMatchConfigurationBuilderExtensions
{
    /// <param name="builder">The configuration builder to extend</param>
    extension(ShouldMatchConfigurationBuilder builder)
    {
        /// <summary>
        /// Configures the ShouldMatchConfiguration to use DiffEngine for file comparison
        /// </summary>
        /// <returns>The configuration builder for method chaining</returns>
        public ShouldMatchConfigurationBuilder ConfigureDiffEngine()
            => builder.Configure(configuration =>
            {
                configuration.DiffViewer = DiffEngineDiffViewer.Instance;
                if (DiffRunner.Disabled)
                    configuration.PreventDiff = true;
            });

        /// <summary>
        /// Configures the ShouldMatchConfiguration to use DiffEngine and ensures diff is enabled
        /// </summary>
        /// <returns>The configuration builder for method chaining</returns>
        public ShouldMatchConfigurationBuilder Diff()
            => builder.ConfigureDiffEngine().Configure(c => c.PreventDiff = false);
    }
}
