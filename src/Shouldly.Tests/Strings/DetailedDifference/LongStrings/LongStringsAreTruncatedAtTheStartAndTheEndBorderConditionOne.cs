﻿using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.LongStrings
{
    // Just after the edge case for the start of string truncation. Now, both start and end should be truncated.
    public class LongStringsAreTruncatedAtTheStartAndTheEndBorderConditionOne: ShouldlyShouldTestScenario
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
            "1a,1b,1c,1d.1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v", 
             Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
                        should be
                    ""1a,1b,1c,1d.1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
                        but was
                    ""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v""
                        difference
                    Case Insensitive Comparison
                    Difference     |                                                         |                                                                                                                                                                                                                                                             
                                   |                                                        \|/                                                                                                                                                                                                                                                            
                    Index          | ...  1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   21   ...
                    Expected Value | ...  a    ,    1    b    ,    1    c    ,    1    d    .    1    e    ,    1    f    ,    1    g    ,    1    ...
                    Actual Value   | ...  a    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    1    ...
                    Expected Code  | ...  97   44   49   98   44   49   99   44   49   100  46   49   101  44   49   102  44   49   103  44   49   ...
                    Actual Code    | ...  97   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   49   ..."
                    ;
                }
        }
    }
}
