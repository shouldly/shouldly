﻿using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldBe
{
    public class CaseIsSensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "SamplE".ShouldBe("sAMPLe", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            { 
                return @"'SamplE' should be 'sAMPLe' but was 'SamplE'
                          difference
                          Case Sensitive Comparison
                          Difference     |  |    |    |    |    |    |   
                                         | \|/  \|/  \|/  \|/  \|/  \|/  
                          Index          | 0    1    2    3    4    5    
                          Expected Value | s    A    M    P    L    e    
                          Actual Value   | S    a    m    p    l    E    
                          Expected Code  | 115  65   77   80   76   101  
                          Actual Code    | 83   97   109  112  108  69   ";

            }
        }

        protected override void ShouldPass()
        {
            "SamplE".ShouldBe("SamplE", Case.Sensitive);
        }
    }
}