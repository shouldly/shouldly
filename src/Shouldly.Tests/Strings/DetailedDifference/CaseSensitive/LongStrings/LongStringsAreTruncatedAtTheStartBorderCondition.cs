using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive.LongStrings
{
    // On the edge, just before the end of the string gets truncated
    public class LongStringsAreTruncatedAtTheStartBorderCondition : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
             .ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v");
        }

        protected override void ShouldThrowAWobbly()
        {
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,ss,1t,1u,1v"
             .ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,Ss,1t,1u,1v");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,ss,1t,1u,1v""
                        should be
                    ""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,Ss,1t,1u,1v""
                        but was
                    ""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,ss,1t,1u,1v""
                        difference
                    Case and Line Ending Sensitive Comparison
                    Difference     |                                                         |                                                   
                                   |                                                        \|/                                                  
                    Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
                    Expected Value | ...  ,    1    p    ,    1    q    ,    1    r    ,    S    s    ,    1    t    ,    1    u    ,    1    v    
                    Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    s    s    ,    1    t    ,    1    u    ,    1    v    
                    Expected Code  | ...  44   49   112  44   49   113  44   49   114  44   83   115  44   49   116  44   49   117  44   49   118  
                    Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   115  115  44   49   116  44   49   117  44   49   118  "
                    ;
                }
        }
    }
}
