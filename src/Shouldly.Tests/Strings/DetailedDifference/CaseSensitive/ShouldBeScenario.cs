using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive
{
    public class ShouldBeScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "StringOne".ShouldBe("StringOne", Case.Sensitive);
        }

        protected override void ShouldThrowAWobbly()
        {
            "Stringone".ShouldBe("StringOne", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""Stringone""
                        should be
                    ""StringOne""
                        but was
                    ""Stringone""
                        difference
                    Case Sensitive Comparison
                    Difference     |                                |             
                                   |                               \|/             
                    Index          | 0    1    2    3    4    5    6    7    8    
                    Expected Value | S    t    r    i    n    g    O    n    e    
                    Actual Value   | S    t    r    i    n    g    o    n    e    
                    Expected Code  | 83   116  114  105  110  103  79   110  101  
                    Actual Code    | 83   116  114  105  110  103  111  110  101"
                    ;
                }
        }

    }
}