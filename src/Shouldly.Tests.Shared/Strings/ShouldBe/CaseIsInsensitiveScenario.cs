using Xunit;

namespace Shouldly.Tests.Strings.ShouldBe
{
    public class CaseIsInsensitiveScenario
    {
        [Fact]
        public void CaseIsInsensitiveScenarioShouldFail()
        {
            var str = "SamplE";
            Verify.ShouldFail(() =>
str.ShouldBe("different", "Some additional context", StringCompareShould.IgnoreCase),

errorWithSource:
@"str
    should be with options: Ignoring case
""different""
    but was
""SamplE""
    difference
Difference     |  |    |    |    |    |    |    |    |    |   
               | \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    6    7    8    
Expected Value | d    i    f    f    e    r    e    n    t    
Actual Value   | S    a    m    p    l    E                   
Expected Code  | 100  105  102  102  101  114  101  110  116  
Actual Code    | 83   97   109  112  108  69                  

Additional Info:
    Some additional context",

errorWithoutSource:
@"""SamplE""
    should be with options: Ignoring case
""different""
    but was not
    difference
Difference     |  |    |    |    |    |    |    |    |    |   
               | \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    6    7    8    
Expected Value | d    i    f    f    e    r    e    n    t    
Actual Value   | S    a    m    p    l    E                   
Expected Code  | 100  105  102  102  101  114  101  110  116  
Actual Code    | 83   97   109  112  108  69                  

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "SamplE".ShouldBe("sAMPLe", StringCompareShould.IgnoreCase);
        }
    }
}