﻿using Xunit;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive.LongStrings
{
    // On the edge, just before the end of the string gets truncated
    public class LongStringsAreTruncatedAtTheStartBorderCondition
    {
        [Fact]
        public void LongStringsAreTruncatedAtTheStartBorderConditionShouldFail()
        {
            var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v";
            Verify.ShouldFail(() =>
str.ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,2s,1t,1u,1v", StringCompareShould.IgnoreCase),

errorWithSource:
@"str
    should be with options: Ignoring case
""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,2s,1t,1u,1v""
    but was
""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    difference
Difference     |                                                         |                                                     
               |                                                        \|/                                                    
Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
Expected Value | ...  ,    1    p    ,    1    q    ,    1    r    ,    2    s    ,    1    t    ,    1    u    ,    1    v    
Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
Expected Code  | ...  44   49   112  44   49   113  44   49   114  44   50   115  44   49   116  44   49   117  44   49   118  
Actual Code    | ...  44   49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  ",

errorWithoutSource:
@"""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
    should be with options: Ignoring case
""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,2s,1t,1u,1v""
    but was not
    difference
Difference     |                                                         |                                                     
               |                                                        \|/                                                    
Index          | ...  44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   
Expected Value | ...  ,    1    p    ,    1    q    ,    1    r    ,    2    s    ,    1    t    ,    1    u    ,    1    v    
Actual Value   | ...  ,    1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    
Expected Code  | ...  44   49   112  44   49   113  44   49   114  44   50   115  44   49   116  44   49   117  44   49   118  
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
