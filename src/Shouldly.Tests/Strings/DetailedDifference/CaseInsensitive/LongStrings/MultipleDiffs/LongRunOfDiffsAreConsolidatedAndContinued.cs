using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive.LongStrings.MultipleDiffs
{
    public class LongRunOfDiffsAreConsolidatedAndContinued : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "1A,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
             .ShouldBe(
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v", 
             ShouldBeStringOptions.IgnoreCase);
        }

        protected override void ShouldThrowAWobbly()
        {
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
             .ShouldBe(
            "2v.2u.2t.2s.2r.2q.2p.2o.2n.2m.2l.2k.2j.2i.2h.2g.2f.2e.2d.2c.2b.2a", 
             ShouldBeStringOptions.IgnoreCase);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
                        should be
                    ""2v.2u.2t.2s.2r.2q.2p.2o.2n.2m.2l.2k.2j.2i.2h.2g.2f.2e.2d.2c.2b.2a""
                        but was
                    ""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
                        difference
                    Case Insensitive and Line Ending Sensitive Comparison

                    Difference     |  |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |        
                                   | \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/       
                    Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   ...  
                    Expected Value | 2    v    .    2    u    .    2    t    .    2    s    .    2    r    .    2    q    .    2    p    .    ...  
                    Actual Value   | 1    a    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
                    Expected Code  | 50   118  46   50   117  46   50   116  46   50   115  46   50   114  46   50   113  46   50   112  46   ...  
                    Actual Code    | 49   97   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  

                    Difference     |       |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |        
                                   |      \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/       
                    Index          | ...  21   22   23   24   25   26   27   28   29   30   31   32   33   34   35   36   37   38   39   40   41   ...  
                    Expected Value | ...  2    o    .    2    n    .    2    m    .    2    l    .    2    k    .    2    j    .    2    i    .    ...  
                    Actual Value   | ...  1    h    ,    1    i    ,    1    j    ,    1    k    ,    1    l    ,    1    m    ,    1    n    ,    ...  
                    Expected Code  | ...  50   111  46   50   110  46   50   109  46   50   108  46   50   107  46   50   106  46   50   105  46   ...  
                    Actual Code    | ...  49   104  44   49   105  44   49   106  44   49   107  44   49   108  44   49   109  44   49   110  44   ...  

                    Difference     |       |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |        
                                   |      \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/       
                    Index          | ...  42   43   44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   ...  
                    Expected Value | ...  2    h    .    2    g    .    2    f    .    2    e    .    2    d    .    2    c    .    2    b    .    ...  
                    Actual Value   | ...  1    o    ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    ...  
                    Expected Code  | ...  50   104  46   50   103  46   50   102  46   50   101  46   50   100  46   50   99   46   50   98   46   ...  
                    Actual Code    | ...  49   111  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   ...  

                    Difference     |       |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |   
                                   |      \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
                    Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
                    Expected Value | ...  .    2    g    .    2    f    .    2    e    .    2    d    .    2    c    .    2    b    .    2    a    
                    Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
                    Expected Code  | ...  46   50   103  46   50   102  46   50   101  46   50   100  46   50   99   46   50   98   46   50   97   
                    Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  "
                    ;
            }
        }
    }
}
