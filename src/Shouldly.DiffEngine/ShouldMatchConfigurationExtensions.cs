using DiffEngine;

namespace Shouldly;

public static class ShouldMatchConfigurationExtensions
{
    public static ShouldMatchConfiguration ConfigureDiffEngine(this ShouldMatchConfiguration configuration)
    {
        configuration.DiffEngine = DiffEngine.Instance;
        if (DiffRunner.Disabled)
            configuration.PreventDiff = true;
        return configuration;
    }
}
