using Xunit;

namespace Shouldly.Tests.Strings.ShouldBe
{
    public class CaseIsSensitiveScenario
    {
        [Fact]
        public void CaseIsSensitiveScenarioShouldFail()
        {
            var str = "SamplE";
            Verify.ShouldFail(() =>
str.ShouldBe("sAMPLe", "Some additional context"),

errorWithSource:
@"str
    should be
""sAMPLe""
    but was
""SamplE""
    difference
Difference     |  |    |    |    |    |    |   
               | \|/  \|/  \|/  \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    
Expected Value | s    A    M    P    L    e    
Actual Value   | S    a    m    p    l    E    
Expected Code  | 115  65   77   80   76   101  
Actual Code    | 83   97   109  112  108  69   

Additional Info:
    Some additional context",

errorWithoutSource:
@"""SamplE""
    should be
""sAMPLe""
    but was not
    difference
Difference     |  |    |    |    |    |    |   
               | \|/  \|/  \|/  \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    
Expected Value | s    A    M    P    L    e    
Actual Value   | S    a    m    p    l    E    
Expected Code  | 115  65   77   80   76   101  
Actual Code    | 83   97   109  112  108  69   

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "SamplE".ShouldBe("SamplE");
        }
    }
}
