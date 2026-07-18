namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive.LongStrings.MultipleDiffs;

public class LongRunOfDiffsAreConsolidatedAndContinued
{
    [Fact]
    public void LongRunOfDiffsAreConsolidatedAndContinuedShouldFail()
    {
        var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v";
        Verify.ShouldFail(() =>
            str.ShouldBe("2v.2u.2t.2s.2r.2q.2p.2o.2n.2m.2l.2k.2j.2i.2h.2g.2f.2e.2d.2c.2b.2a", StringCompareShould.IgnoreCase));
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