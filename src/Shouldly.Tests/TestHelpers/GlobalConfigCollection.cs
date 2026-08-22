namespace Shouldly.Tests.TestHelpers;

/// <summary>
/// Collection for tests that mutate global <see cref="ShouldlyConfiguration"/> state (for example
/// <see cref="ShouldlyConfiguration.DefaultFloatingPointTolerance"/>). Disabling parallelization keeps
/// the collection from running concurrently with any other test, so a temporary global change cannot
/// leak into an assertion running in a different test class.
/// </summary>
[CollectionDefinition(Name, DisableParallelization = true)]
public class GlobalConfigCollection
{
    public const string Name = "Global configuration";
}
