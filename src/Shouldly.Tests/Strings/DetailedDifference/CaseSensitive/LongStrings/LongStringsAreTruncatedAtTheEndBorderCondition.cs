using Xunit;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive.LongStrings
{
    // On the edge, just before the start of the string gets truncated
    public class LongStringsAreTruncatedAtTheEndBorderCondition
    {
        [Fact]
        public void LongStringsAreTruncatedAtTheEndBorderConditionShouldFail()
        {
            var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v";
            Verify.ShouldFail(() =>
str.ShouldBe("1a,1b,1c,1D,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"),

errorWithSource:
@"str
    should be
""1a,1b,1c,1D,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    but was
""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    difference
Difference     |                                                    |                                                          
               |                                                   \|/                                                         
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   ...  
Expected Value | 1    a    ,    1    b    ,    1    c    ,    1    D    ,    1    e    ,    1    f    ,    1    g    ,    ...  
Actual Value   | 1    a    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
Expected Code  | 49   97   44   49   98   44   49   99   44   49   68   44   49   101  44   49   102  44   49   103  44   ...  
Actual Code    | 49   97   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  ",

errorWithoutSource:
@"""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    should be
""1a,1b,1c,1D,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    but was not
    difference
Difference     |                                                    |                                                          
               |                                                   \|/                                                         
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   ...  
Expected Value | 1    a    ,    1    b    ,    1    c    ,    1    D    ,    1    e    ,    1    f    ,    1    g    ,    ...  
Actual Value   | 1    a    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
Expected Code  | 49   97   44   49   98   44   49   99   44   49   68   44   49   101  44   49   102  44   49   103  44   ...  
Actual Code    | 49   97   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  ");
        }

        [Fact]
        public void ShouldPass()
        {
            "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
                 .ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v");
        }
    }
}
