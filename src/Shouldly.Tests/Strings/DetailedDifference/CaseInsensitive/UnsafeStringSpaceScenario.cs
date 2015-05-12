using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive
{
    public class UnsafeStringSpaceScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "StringOne Space".ShouldBe("Stringone Space", Case.Insensitive);
        }

        protected override void ShouldThrowAWobbly()
        {
            "StringOne Space".ShouldBe("Stringone\tSpace", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""StringOne Space""
                            should be" +
                        "\"Stringone\tSpace\"" +
                            @"but was
                            ""StringOne Space""
                            difference
                          Case Insensitive Comparison
                          Difference     |                                               |                            
                                         |                                              \|/                           
                          Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   
                          Expected Value | S    t    r    i    n    g    o    n    e    \t   S    p    a    c    e    
                          Actual Value   | S    t    r    i    n    g    O    n    e    \s   S    p    a    c    e    
                          Expected Code  | 83   116  114  105  110  103  111  110  101  9    83   112  97   99   101  
                          Actual Code    | 83   116  114  105  110  103  79   110  101  32   83   112  97   99   101  "
                    ;
                }
        }

    }
}
