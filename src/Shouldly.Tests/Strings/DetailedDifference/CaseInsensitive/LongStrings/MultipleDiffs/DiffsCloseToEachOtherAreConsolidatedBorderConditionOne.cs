namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive.LongStrings.MultipleDiffs;

// Just before the edge case for consolidation. 2 differences are exactly the required length apart to be consolidated into one diff
public class DiffsCloseToEachOtherAreConsolidatedBorderConditionOne
{
    [Fact]
    public void DiffsCloseToEachOtherAreConsolidatedBorderConditionOneShouldFail()
    {
        var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v";
        Verify.ShouldFail(() =>
                str.ShouldBe("1a,1b.1c,1d,1e,1f,1g,1h,1j,1j,1k,1l,1m,1n,1o.1p,1q,1r,1s,1t,1u,1w", StringCompareShould.IgnoreCase),

            errorWithSource:
            @"str
    should be with options: Ignoring case
""1a,1b.1c,1d,1e,1f,1g,1h,1j,1j,1k,1l,1m,1n,1o.1p,1q,1r,1s,1t,1u,1w""
    but was
""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    difference
Difference     |       |                                                                                                   |        
               |      \|/                                                                                                 \|/       
Index          | ...  5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   21   22   23   24   25   ...  
Expected Value | ...  .    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    1    h    ,    1    j    ...  
Actual Value   | ...  ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    1    h    ,    1    i    ...  
Expected Code  | ...  46   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   49   104  44   49   106  ...  
Actual Code    | ...  44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   49   104  44   49   105  ...  

Difference     |       |                                                                                                   |   
               |      \|/                                                                                                 \|/  
Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
Expected Value | ...  .    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    w    
Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
Expected Code  | ...  46   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   119  
Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  ",

            errorWithoutSource:
            @"""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    should be with options: Ignoring case
""1a,1b.1c,1d,1e,1f,1g,1h,1j,1j,1k,1l,1m,1n,1o.1p,1q,1r,1s,1t,1u,1w""
    but was not
    difference
Difference     |       |                                                                                                   |        
               |      \|/                                                                                                 \|/       
Index          | ...  5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   21   22   23   24   25   ...  
Expected Value | ...  .    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    1    h    ,    1    j    ...  
Actual Value   | ...  ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    1    h    ,    1    i    ...  
Expected Code  | ...  46   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   49   104  44   49   106  ...  
Actual Code    | ...  44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   49   104  44   49   105  ...  

Difference     |       |                                                                                                   |   
               |      \|/                                                                                                 \|/  
Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
Expected Value | ...  .    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    w    
Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
Expected Code  | ...  46   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   119  
Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  ");
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