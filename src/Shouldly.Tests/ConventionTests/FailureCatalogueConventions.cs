using System.Reflection;
using Shouldly.Internals.Failures;

namespace Shouldly.Tests.ConventionTests;

/// <summary>
/// The failure catalogue has one entry per built-in assertion, named exactly as the assertion. A custom assertion
/// with the same name gets the entry's wording, so the names have to agree.
/// </summary>
public class FailureCatalogueConventions
{
    // Assertions that report through other assertions and so have no failure of their own.
    private static readonly string[] ReportedByOtherAssertions =
    [
        // Checks the element types with ShouldBe.
        "ShouldBeOfTypes",
        // Obsolete name for ShouldSatisfy.
        "ShouldSatisfyAllConditions"
    ];

    [Fact]
    public void EveryEntryIsNamedAfterItsField()
    {
        var misnamed =
            from field in typeof(Fail).GetFields(BindingFlags.Public | BindingFlags.Static)
            let failure = (Failure)field.GetValue(null)!
            where failure.Name != field.Name
            select $"{field.Name} is named {failure.Name}";

        misnamed.ShouldBeEmpty();
    }

    [Fact]
    public void EveryAssertionHasAnEntry()
    {
        var assertionNames =
            from type in typeof(Should).Assembly.GetExportedTypes()
            where type.IsDefined(typeof(ShouldlyMethodsAttribute), inherit: false)
            from method in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            select method.Name;

        var missing = assertionNames
            .Distinct()
            .Where(name => FailureNames.Find(name) == null)
            .Except(ReportedByOtherAssertions)
            .OrderBy(name => name);

        missing.ShouldBeEmpty();
    }

    [Fact]
    public void EveryEntryBelongsToAnAssertion()
    {
        var assertionNames = new HashSet<string>(
            from type in typeof(Should).Assembly.GetExportedTypes()
            where type.IsDefined(typeof(ShouldlyMethodsAttribute), inherit: false)
            from method in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            select method.Name);

        var orphans = typeof(Fail).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(field => field.Name)
            .Where(name => !assertionNames.Contains(name))
            .OrderBy(name => name);

        orphans.ShouldBeEmpty();
    }
}
