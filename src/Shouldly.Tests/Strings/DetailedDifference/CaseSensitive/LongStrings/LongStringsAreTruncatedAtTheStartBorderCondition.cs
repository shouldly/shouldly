namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive.LongStrings;

// On the edge, just before the end of the string gets truncated
public class LongStringsAreTruncatedAtTheStartBorderCondition
{
    [Fact]
    public void LongStringsAreTruncatedAtTheStartBorderConditionShouldFail()
    {
        var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,ss,1t,1u,1v";
        Verify.ShouldFail(() =>
            str.ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,Ss,1t,1u,1v"));
    }

    [Fact]
    public void ShouldPass()
    {
        "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
            .ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v");
    }
}