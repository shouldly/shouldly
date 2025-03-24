namespace Shouldly.Tests.ShouldContain;

public class StringContainsStringCaseIsInsensitiveScenario
{
    [Fact]
    public void StringContainsStringCaseIsInsensitiveScenarioShouldFail()
    {
        const string target = "Shouldly is legendary";
        Verify.ShouldFail(() =>
                target.ShouldContain("legend-wait for it-ary"),

            errorWithSource:
            """
            target
                should contain (case insensitive comparison)
            "legend-wait for it-ary"
                but was actually
            "Shouldly is legendary"
            """,

            errorWithoutSource:
            """
            "Shouldly is legendary"
                should contain (case insensitive comparison)
            "legend-wait for it-ary"
                but did not
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        "Shouldly is legendary".ShouldContain("LEGENDARY");
    }
}