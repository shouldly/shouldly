using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldBe
{
    public class InsensitiveToLineEndingsScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "line1\nline2".ShouldBe("line1\r\nLine3", () => "Some additional context", ShouldBeStringOptions.IgnoreLineEndings);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            { 
                return @"'line1\nline2' should be " + "'line1\nLine3' but was 'line1\nline2'" + @"
                          difference
                          Case Sensitive and Line Ending Insensitive Comparison
                          Difference     |                                |                   |   
                                         |                               \|/                 \|/  
                          Index          | 0    1    2    3    4    5    6    7    8    9    10   
                          Expected Value | l    i    n    e    1    \n   L    i    n    e    3    
                          Actual Value   | l    i    n    e    1    \n   l    i    n    e    2    
                          Expected Code  | 108  105  110  101  49   10   76   105  110  101  51  
                          Actual Code    | 108  105  110  101  49   10   108  105  110  101  50  
                          Additional Info:
                          Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "line1\nline2".ShouldBe("line1\r\nline2", ShouldBeStringOptions.IgnoreLineEndings);
        }
    }
}
