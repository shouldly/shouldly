using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.LongStrings.MultipleDiffs
{
    public class DiffsCloseToEachOtherAreConsolidatedBorderConditionTwo: ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "1A,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
             .ShouldBe(
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v", 
             Case.Insensitive);
        }

        protected override void ShouldThrowAWobbly()
        {
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
             .ShouldBe(
            "1a,2b,1c,1d,1e,1f,1g,2h,1i,1j,1k,1l,1m,1n,1p,1p,1q,1r,1w,1t,1u,1v", 
             Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
                        should be
                    ""1a,2b,1c,1d,1e,1f,1g,2h,1i,1j,1k,1l,1m,1n,1p,1p,1q,1r,1w,1t,1u,1v""
                        but was
                    ""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
                        difference
                    Case Insensitive Comparison
                    Difference     |                 |                                                                                             
                                   |                \|/                                                                                            
                    Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   ...  
                    Expected Value | 1    a    ,    2    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
                    Actual Value   | 1    a    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
                    Expected Code  | 49   97   44   50   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  
                    Actual Code    | 49   97   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  

                    Difference     |                                                         |                                                          
                                   |                                                        \|/                                                         
                    Index          | ...  11   12   13   14   15   16   17   18   19   20   21   22   23   24   25   26   27   28   29   30   31   ...  
                    Expected Value | ...  ,    1    e    ,    1    f    ,    1    g    ,    2    h    ,    1    i    ,    1    j    ,    1    k    ...  
                    Actual Value   | ...  ,    1    e    ,    1    f    ,    1    g    ,    1    h    ,    1    i    ,    1    j    ,    1    k    ...  
                    Expected Code  | ...  44   49   101  44   49   102  44   49   103  44   50   104  44   49   105  44   49   106  44   49   107  ...  
                    Actual Code    | ...  44   49   101  44   49   102  44   49   103  44   49   104  44   49   105  44   49   106  44   49   107  ...  

                    Difference     |                                                         |                                                          
                                   |                                                        \|/                                                         
                    Index          | ...  33   34   35   36   37   38   39   40   41   42   43   44   45   46   47   48   49   50   51   52   53   ...  
                    Expected Value | ...  1    l    ,    1    m    ,    1    n    ,    1    p    ,    1    p    ,    1    q    ,    1    r    ,    ...  
                    Actual Value   | ...  1    l    ,    1    m    ,    1    n    ,    1    o    ,    1    p    ,    1    q    ,    1    r    ,    ...  
                    Expected Code  | ...  49   108  44   49   109  44   49   110  44   49   112  44   49   112  44   49   113  44   49   114  44   ...  
                    Actual Code    | ...  49   108  44   49   109  44   49   110  44   49   111  44   49   112  44   49   113  44   49   114  44   ...  

                    Difference     |                                                              |                                                
                                   |                                                             \|/                                               
                    Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
                    Expected Value | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    w    ,    1    t    ,    1    u    ,    1    v    
                    Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
                    Expected Code  | ...  44   49   112  44   49   113  44   49   114  44   49   119  44   49   116  44   49   117  44   49   118  
                    Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  "
                    ;
                }
        }
    }
}
