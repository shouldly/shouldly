using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldBe
{
    public class CaseIsInsensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "SamplE".ShouldBe("different", Case.Insensitive, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"'SamplE' should be 'different' but was 'SamplE'
                         Additional Info:
                         Some additional context
                         difference
                         Case Insensitive Comparison
                         Difference     |  |    |    |    |    |    |    |    |    |   
                                        | \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
                         Index          | 0    1    2    3    4    5    6    7    8    
                         Expected Value | d    i    f    f    e    r    e    n    t    
                         Actual Value   | S    a    m    p    l    E                   
                         Expected Code  | 100  105  102  102  101  114  101  110  116  
                         Actual Code    | 83   97   109  112  108  69  ";
        }
        }

        protected override void ShouldPass()
        {
            "SamplE".ShouldBe("sAMPLe", Case.Insensitive);
        }
    }
}
