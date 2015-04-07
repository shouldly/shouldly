using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive
{
    public class UnsafeStringTabScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "StringOne\tTab".ShouldBe("StringOne\tTab", Case.Sensitive);
        }

        protected override void ShouldThrowAWobbly()
        {
            "StringOne\tTab".ShouldBe("Stringone Tab", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""StringOne\tTab""
                            should be
                        ""Stringone Tab""
                            but was
                            ""StringOne Tab""
                            difference
                          Case Sensitive Comparison
                          Difference     |                                |              |                  
                                         |                               \|/            \|/                 
                          Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   
                          Expected Value | S    t    r    i    n    g    o    n    e    \s   T    a    b    
                          Actual Value   | S    t    r    i    n    g    O    n    e    \t   T    a    b    
                          Expected Code  | 83   116  114  105  110  103  111  110  101  32   84   97   98   
                          Actual Code    | 83   116  114  105  110  103  79   110  101  9    84   97   98   "
                    ;
                }
        }

    }
}
