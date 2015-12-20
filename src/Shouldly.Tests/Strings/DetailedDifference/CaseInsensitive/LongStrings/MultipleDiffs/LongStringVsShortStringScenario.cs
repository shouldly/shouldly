using Xunit;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive.LongStrings.MultipleDiffs
{
    public class LongStringVsShortStringScenario
    {


        [Fact]
        public void LongStringVsShortStringScenarioShouldFail()
        {
            var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v";
            Verify.ShouldFail(() =>
str.ShouldBe("1a", StringCompareShould.IgnoreCase),

errorWithSource:
@"str
    should be with options: Ignoring case
""1a""
    but was
""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    difference
Difference     |       |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |        
               |      \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/       
Index          | ...  2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   21   22   ...  
Expected Value | ...                                                                                                           ...  
Actual Value   | ...  ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    1    h    ...  
Expected Code  | ...                                                                                                           ...  
Actual Code    | ...  44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   49   104  ...  

Difference     |       |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |        
               |      \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/       
Index          | ...  23   24   25   26   27   28   29   30   31   32   33   34   35   36   37   38   39   40   41   42   43   ...  
Expected Value | ...                                                                                                           ...  
Actual Value   | ...  ,    1    i    ,    1    j    ,    1    k    ,    1    l    ,    1    m    ,    1    n    ,    1    o    ...  
Expected Code  | ...                                                                                                           ...  
Actual Code    | ...  44   49   105  44   49   106  44   49   107  44   49   108  44   49   109  44   49   110  44   49   111  ...  

Difference     |       |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |   
               |      \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
Expected Value | ...                                                                                                           
Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
Expected Code  | ...                                                                                                           
Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  ",

errorWithoutSource:
@"""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    should be with options: Ignoring case
""1a""
    but was not
    difference
Difference     |       |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |        
               |      \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/       
Index          | ...  2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   21   22   ...  
Expected Value | ...                                                                                                           ...  
Actual Value   | ...  ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    1    h    ...  
Expected Code  | ...                                                                                                           ...  
Actual Code    | ...  44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   49   104  ...  

Difference     |       |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |        
               |      \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/       
Index          | ...  23   24   25   26   27   28   29   30   31   32   33   34   35   36   37   38   39   40   41   42   43   ...  
Expected Value | ...                                                                                                           ...  
Actual Value   | ...  ,    1    i    ,    1    j    ,    1    k    ,    1    l    ,    1    m    ,    1    n    ,    1    o    ...  
Expected Code  | ...                                                                                                           ...  
Actual Code    | ...  44   49   105  44   49   106  44   49   107  44   49   108  44   49   109  44   49   110  44   49   111  ...  

Difference     |       |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |   
               |      \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
Expected Value | ...                                                                                                           
Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
Expected Code  | ...                                                                                                           
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
}
