using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldBe
{
    public class SensitiveToLineEndingsScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "line1\nline2".ShouldBe("line1\r\nline2", () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            { 
                return @"'line1\nline2' should be " + "'line1\r\nline2' but was 'line1\nline2'" + @"
                          difference
                          Case and Line Ending Sensitive Comparison
                          Difference     |                           |    |    |    |    |    |    |   
                                         |                          \|/  \|/  \|/  \|/  \|/  \|/  \|/  
                          Index          | 0    1    2    3    4    5    6    7    8    9    10   11   
                          Expected Value | l    i    n    e    1    \r   \n   l    i    n    e    2    
                          Actual Value   | l    i    n    e    1    \n   l    i    n    e    2         
                          Expected Code  | 108  105  110  101  49   13   10   108  105  110  101  50   
                          Actual Code    | 108  105  110  101  49   10   108  105  110  101  50  
                          Additional Info:
                          Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "line1\nline2".ShouldBe("line1\nline2");
        }
    }
}
