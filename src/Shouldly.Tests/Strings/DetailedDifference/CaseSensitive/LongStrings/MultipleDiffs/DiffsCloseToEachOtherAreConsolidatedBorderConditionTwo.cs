namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive.LongStrings.MultipleDiffs;

// Just after the edge case for consolidation. 2 differences are exactly one more than the required length apart for consolidation. So they will not be consolidated to their previous diff.
public class DiffsCloseToEachOtherAreConsolidatedBorderConditionTwo
{
    [Fact]
    public void DiffsCloseToEachOtherAreConsolidatedBorderConditionTwoShouldFail()
    {
        var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v";
        Verify.ShouldFail(() =>
                str.ShouldBe("1a,1b.1c,1d,1e,1f,1g,1h,1i.1j,1k,1l,1m,1n,1O,1p,1q,1r,1s,1t,1u,1w"),

            errorWithSource:
            """
            str
                should be
            "1a,1b.1c,1d,1e,1f,1g,1h,1i.1j,1k,1l,1m,1n,1O,1p,1q,1r,1s,1t,1u,1w"
                but was
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
                difference
            Difference     |                           |                                                                                   
                           |                          \|/                                                                                  
            Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   ...  
            Expected Value | 1    a    ,    1    b    .    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
            Actual Value   | 1    a    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
            Expected Code  | 49   97   44   49   98   46   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  
            Actual Code    | 49   97   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  

            Difference     |                 |                                                                                    |             
                           |                \|/                                                                                  \|/            
            Index          | ...  24   25   26   27   28   29   30   31   32   33   34   35   36   37   38   39   40   41   42   43   44   ...  
            Expected Value | ...  1    i    .    1    j    ,    1    k    ,    1    l    ,    1    m    ,    1    n    ,    1    O    ,    ...  
            Actual Value   | ...  1    i    ,    1    j    ,    1    k    ,    1    l    ,    1    m    ,    1    n    ,    1    o    ,    ...  
            Expected Code  | ...  49   105  46   49   106  44   49   107  44   49   108  44   49   109  44   49   110  44   49   79   44   ...  
            Actual Code    | ...  49   105  44   49   106  44   49   107  44   49   108  44   49   109  44   49   110  44   49   111  44   ...  

            Difference     |                                                                                                           |   
                           |                                                                                                          \|/  
            Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
            Expected Value | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    w    
            Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
            Expected Code  | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   119  
            Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  
            """,

            errorWithoutSource:
            """
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
                should be
            "1a,1b.1c,1d,1e,1f,1g,1h,1i.1j,1k,1l,1m,1n,1O,1p,1q,1r,1s,1t,1u,1w"
                but was not
                difference
            Difference     |                           |                                                                                   
                           |                          \|/                                                                                  
            Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   ...  
            Expected Value | 1    a    ,    1    b    .    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
            Actual Value   | 1    a    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
            Expected Code  | 49   97   44   49   98   46   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  
            Actual Code    | 49   97   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  

            Difference     |                 |                                                                                    |             
                           |                \|/                                                                                  \|/            
            Index          | ...  24   25   26   27   28   29   30   31   32   33   34   35   36   37   38   39   40   41   42   43   44   ...  
            Expected Value | ...  1    i    .    1    j    ,    1    k    ,    1    l    ,    1    m    ,    1    n    ,    1    O    ,    ...  
            Actual Value   | ...  1    i    ,    1    j    ,    1    k    ,    1    l    ,    1    m    ,    1    n    ,    1    o    ,    ...  
            Expected Code  | ...  49   105  46   49   106  44   49   107  44   49   108  44   49   109  44   49   110  44   49   79   44   ...  
            Actual Code    | ...  49   105  44   49   106  44   49   107  44   49   108  44   49   109  44   49   110  44   49   111  44   ...  

            Difference     |                                                                                                           |   
                           |                                                                                                          \|/  
            Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
            Expected Value | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    w    
            Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
            Expected Code  | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   119  
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