namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive.LongStrings;

// Just after the edge case for the end of string truncation. Now, both start and end should be truncated.
public class LongStringsAreTruncatedAtTheStartBorderConditionBorderConditionTwo
{
    [Fact]
    public void LongStringsAreTruncatedAtTheStartBorderConditionBorderConditionTwoShouldFail()
    {
        var str = "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1rr1s,1t,1u,1v";
        Verify.ShouldFail(() =>
                str.ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1rR1s,1t,1u,1v"),

            errorWithSource:
            @"str
    should be
""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1rR1s,1t,1u,1v""
    but was
""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1rr1s,1t,1u,1v""
    difference
Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  43   44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   ...  
Expected Value | ...  o    ,    1    p    ,    1    q    ,    1    r    R    1    s    ,    1    t    ,    1    u    ,    1    ...  
Actual Value   | ...  o    ,    1    p    ,    1    q    ,    1    r    r    1    s    ,    1    t    ,    1    u    ,    1    ...  
Expected Code  | ...  111  44   49   112  44   49   113  44   49   114  82   49   115  44   49   116  44   49   117  44   49   ...  
Actual Code    | ...  111  44   49   112  44   49   113  44   49   114  114  49   115  44   49   116  44   49   117  44   49   ...  ",

            errorWithoutSource:
            @"""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1rr1s,1t,1u,1v""
    should be
""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1rR1s,1t,1u,1v""
    but was not
    difference
Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  43   44   45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   ...  
Expected Value | ...  o    ,    1    p    ,    1    q    ,    1    r    R    1    s    ,    1    t    ,    1    u    ,    1    ...  
Actual Value   | ...  o    ,    1    p    ,    1    q    ,    1    r    r    1    s    ,    1    t    ,    1    u    ,    1    ...  
Expected Code  | ...  111  44   49   112  44   49   113  44   49   114  82   49   115  44   49   116  44   49   117  44   49   ...  
Actual Code    | ...  111  44   49   112  44   49   113  44   49   114  114  49   115  44   49   116  44   49   117  44   49   ...  ");
    }

    [Fact]
    public void ShouldPass()
    {
        "1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v"
            .ShouldBe("1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v");
    }
}