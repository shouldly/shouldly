using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ConventionTests;

public class CompareAsObjectTypesDefaults
{
    /// <summary>
    /// The test project registers its own types (see <see cref="ModuleInitializer"/>). Shouldly's shipped
    /// defaults must not name any of them, otherwise every consumer would carry a test-only entry and
    /// this registration would add a duplicate (issue #1410).
    /// </summary>
    [Fact]
    public void TestTypesAreRegisteredByTheTestProjectNotShipped() =>
        ShouldlyConfiguration.CompareAsObjectTypes
            .Where(name => name.StartsWith("Shouldly.Tests.", StringComparison.Ordinal))
            .ShouldBe([typeof(Strange).FullName!, typeof(IdBag).FullName!]);
}
