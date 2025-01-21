namespace Shouldly.Tests.Strings.ShouldBe;

public class CaseAndLineEndingInsensitiveScenario
{
    private const string Lf = "\n";
    
    [Fact]
    public void CaseAndLineEndingInsensitiveScenarioShouldFail()
    {
        var str = "line1\nline2";
        Verify.ShouldFail(() =>
                str.ShouldBe("line1\r\nLine3", "Some additional context", StringCompareShould.IgnoreLineEndings | StringCompareShould.IgnoreCase),

            errorWithSource:
            $"""
             str
                 should be with options: Ignoring line endings, Ignoring case
             "line1{Lf}Line3"
                 but was
             "line1{Lf}line2"
                 difference
             Difference     |                                                    |   
                            |                                                   \|/  
             Index          | 0    1    2    3    4    5    6    7    8    9    10   
             Expected Value | l    i    n    e    1    \n   L    i    n    e    3    
             Actual Value   | l    i    n    e    1    \n   l    i    n    e    2    
             Expected Code  | 108  105  110  101  49   10   76   105  110  101  51   
             Actual Code    | 108  105  110  101  49   10   108  105  110  101  50   

             Additional Info:
                 Some additional context
             """,

            errorWithoutSource:
            $"""
             "line1{Lf}line2"
                 should be with options: Ignoring line endings, Ignoring case
             "line1{Lf}Line3"
                 but was not
                 difference
             Difference     |                                                    |   
                            |                                                   \|/  
             Index          | 0    1    2    3    4    5    6    7    8    9    10   
             Expected Value | l    i    n    e    1    \n   L    i    n    e    3    
             Actual Value   | l    i    n    e    1    \n   l    i    n    e    2    
             Expected Code  | 108  105  110  101  49   10   76   105  110  101  51   
             Actual Code    | 108  105  110  101  49   10   108  105  110  101  50   

             Additional Info:
                 Some additional context
             """);
    }

    [Fact]
    public void ShouldPass()
    {
        "line1\nline2".ShouldBe("line1\r\nLine2",
            StringCompareShould.IgnoreLineEndings | StringCompareShould.IgnoreCase);
    }
}