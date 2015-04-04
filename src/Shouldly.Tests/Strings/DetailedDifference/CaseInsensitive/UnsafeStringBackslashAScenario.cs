﻿using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive
{
    public class UnsafeStringBackslashAScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "StringOne\aBackslashA".ShouldBe("Stringone\aBackslashA", Case.Insensitive);
        }

        protected override void ShouldThrowAWobbly()
        {
            "StringOne\aBackslashA".ShouldBe("Stringone BackslashA", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""StringOne\aBackslashA""
                            should be
                        ""Stringone BackslashA""
                            but was" +
                            "\"StringOne\aBackslashA\"" +
                            @"difference
                          Case Insensitive Comparison
                          Difference     |                                               |                  
                                         |                                              \|/                 
                          Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   
                          Expected Value | S    t    r    i    n    g    o    n    e    \s   B    a    c    k    s    l    a    s    h    A    
                          Actual Value   | S    t    r    i    n    g    O    n    e    \a   B    a    c    k    s    l    a    s    h    A    
                          Expected Code  | 83   116  114  105  110  103  111  110  101  32   66   97   99   107  115  108  97   115  104  65   
                          Actual Code    | 83   116  114  105  110  103  79   110  101  7    66   97   99   107  115  108  97   115  104  65   "
                    ;
                }
        }

    }
}
