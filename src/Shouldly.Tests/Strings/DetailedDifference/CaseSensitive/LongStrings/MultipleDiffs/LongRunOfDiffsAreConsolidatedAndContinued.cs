namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive.LongStrings.MultipleDiffs;

public class LongRunOfDiffsAreConsolidatedAndContinued
{
    [Fact]
    public void LongRunOfDiffsAreConsolidatedAndContinuedShouldFail()
    {
        var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v";
        Verify.ShouldFail(() =>
            str.ShouldBe("2A.2B.2C.2D.2E.2F.2G.2H.2I.2J.2K.2L.2M.2N.2O.2P.2Q.2R.2S.2T.2U.2V"));
    }

    [Fact]
    public void ShouldPass()
    {
        "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
            .ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v");
    }
}