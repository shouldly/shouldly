namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive.LongStrings;

public class LongStringsAreTruncatedAtTheStart
{
    [Fact]
    public void LongStringsAreTruncatedAtTheStartShouldFail()
    {
        var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v";
        Verify.ShouldFail(() =>
                str.ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1S,1t,1u,1v"),

            errorWithSource:
            """
            str
                should be
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1S,1t,1u,1v"
                but was
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
                difference
            Difference     |                                                              |                                                
                           |                                                             \|/                                               
            Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
            Expected Value | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    S    ,    1    t    ,    1    u    ,    1    v    
            Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
            Expected Code  | ...  44   49   112  44   49   113  44   49   114  44   49   83   44   49   116  44   49   117  44   49   118  
            Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  
            """,

            errorWithoutSource:
            """
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
                should be
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1S,1t,1u,1v"
                but was not
                difference
            Difference     |                                                              |                                                
                           |                                                             \|/                                               
            Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
            Expected Value | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    S    ,    1    t    ,    1    u    ,    1    v    
            Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
            Expected Code  | ...  44   49   112  44   49   113  44   49   114  44   49   83   44   49   116  44   49   117  44   49   118  
            Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
            .ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v");
    }
}