﻿using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        const string ThisOtherString = "this other string";
        const string ThisString = "this string";

        protected override void ShouldPass()
        {
            ThisString.ShouldBe(ThisString);
        }

        protected override void ShouldThrowAWobbly()
        {
            ThisString.ShouldBe(ThisOtherString, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"ThisString should be ""this other string"" but was ""this string""
                            difference
                            Case and Line Ending Sensitive Comparison
                            Difference     |                           |         |    |    |    |    |    |    |    |    |    |   
                                           |                          \|/       \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
                            Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   
                            Expected Value | t    h    i    s    \s   o    t    h    e    r    \s   s    t    r    i    n    g    
                            Actual Value   | t    h    i    s    \s   s    t    r    i    n    g                                  
                            Expected Code  | 116  104  105  115  32   111  116  104  101  114  32   115  116  114  105  110  103  
                            Actual Code    | 116  104  105  115  32   115  116  114  105  110  103   
                            Additional Info:
                            Some additional context ";
            }
        }
    }
}