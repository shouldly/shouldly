using DiffEngine;

namespace Shouldly;

public static class ShouldMatchConfigurationBuilderExtensions
{
    public static ShouldMatchConfigurationBuilder ConfigureDiffEngine(this ShouldMatchConfigurationBuilder builder)
        => builder.Configure(configuration =>
        {
            configuration.DiffViewer = DiffEngineDiffViewer.Instance;
            if (DiffRunner.Disabled)
                configuration.PreventDiff = true;
        });

    public static ShouldMatchConfigurationBuilder Diff(this ShouldMatchConfigurationBuilder builder)
        => builder.ConfigureDiffEngine().Configure(c => c.PreventDiff = false);
}
