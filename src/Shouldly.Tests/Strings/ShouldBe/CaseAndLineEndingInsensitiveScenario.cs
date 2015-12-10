using Xunit;

namespace Shouldly.Tests.Strings.ShouldBe
{
    public class CaseAndLineEndingInsensitiveScenario
    {
        [Fact]
        public void CaseAndLineEndingInsensitiveScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
"line1\nline2".ShouldBe("line1\r\nLine3", () => "Some additional context",
                StringCompareShould.IgnoreLineEndings | StringCompareShould.IgnoreCase),

errorWithSource:
@"'line1\nline2'
    should be
'line1\nLine3'
    but was
'line1\nline2'

    difference
    Case and Line Ending Insensitive Comparison
    Difference     |                                                    |   
                   |                                                   \|/  
    Index          | 0    1    2    3    4    5    6    7    8    9    10   
    Expected Value | l    i    n    e    1    \n   L    i    n    e    3    
    Actual Value   | l    i    n    e    1    \n   l    i    n    e    2    
    Expected Code  | 108  105  110  101  49   10   76   105  110  101  51  
    Actual Code    | 108  105  110  101  49   10   108  105  110  101  50  
    Additional Info:
    Some additional context",

    errorWithoutSource:
@"'line1\nline2'
    should be
'line1\nLine3'
    but was
'line1\nline2'

    difference
    Case and Line Ending Insensitive Comparison
    Difference     |                                                    |   
                   |                                                   \|/  
    Index          | 0    1    2    3    4    5    6    7    8    9    10   
    Expected Value | l    i    n    e    1    \n   L    i    n    e    3    
    Actual Value   | l    i    n    e    1    \n   l    i    n    e    2    
    Expected Code  | 108  105  110  101  49   10   76   105  110  101  51  
    Actual Code    | 108  105  110  101  49   10   108  105  110  101  50  
    Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "line1\nline2".ShouldBe("line1\r\nLine2",
                    StringCompareShould.IgnoreLineEndings | StringCompareShould.IgnoreCase);
        }
    }
}
