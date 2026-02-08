namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive.LongStrings.MultipleDiffs;

// Just before the edge case for consolidation. 2 differences are exactly the required length apart to be consolidated into one diff
public class DiffsCloseToEachOtherAreConsolidatedBorderConditionOne
{
    [Fact]
    public void DiffsCloseToEachOtherAreConsolidatedBorderConditionOneShouldFail()
    {
        var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v";
        Verify.ShouldFail(() =>
            str.ShouldBe("1a,1b.1c,1d,1e,1f,1g,1h,1j,1j,1k,1l,1m,1n,1o.1p,1q,1r,1s,1t,1u,1w", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "1A,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
            .ShouldBe(
                "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v",
                StringCompareShould.IgnoreCase);
    }
}