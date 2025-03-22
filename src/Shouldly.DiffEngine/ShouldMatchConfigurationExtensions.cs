namespace Shouldly;

public static class ShouldMatchConfigurationExtensions
{
    public static ShouldMatchConfiguration ConfigureDiffEngine(this ShouldMatchConfiguration configuration)
    {
        configuration.DiffEngine = DiffEngine.Instance;
        return configuration;
    }
}
