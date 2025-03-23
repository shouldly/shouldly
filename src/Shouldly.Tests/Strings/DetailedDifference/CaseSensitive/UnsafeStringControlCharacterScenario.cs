namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnsafeStringControlCharacterScenario
{
    private const char NullChar = '\0';
    private const char StartOfHeading = '\u0001';

    [Fact]
    public void UnsafeStringControlCharacterScenarioShouldFail()
    {
        var str = "StringOne\u0000ControlChar";
        Verify.ShouldFail(() =>
                str.ShouldBe("Stringone\u0001ControlChar"),

            errorWithSource:
            $"""
             str
                 should be
             "Stringone{StartOfHeading}ControlChar"
                 but was
             "StringOne{NullChar}ControlChar"
                 difference
             Difference     |                                |              |                                                          
                            |                               \|/            \|/                                                         
             Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   
             Expected Value | S    t    r    i    n    g    o    n    e    \u1; C    o    n    t    r    o    l    C    h    a    r    
             Actual Value   | S    t    r    i    n    g    O    n    e    \u0; C    o    n    t    r    o    l    C    h    a    r    
             Expected Code  | 83   116  114  105  110  103  111  110  101  1    67   111  110  116  114  111  108  67   104  97   114  
             Actual Code    | 83   116  114  105  110  103  79   110  101  0    67   111  110  116  114  111  108  67   104  97   114  
             """,

            errorWithoutSource:
            $"""
             "StringOne{NullChar}ControlChar"
                 should be
             "Stringone{StartOfHeading}ControlChar"
                 but was not
                 difference
             Difference     |                                |              |                                                          
                            |                               \|/            \|/                                                         
             Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   
             Expected Value | S    t    r    i    n    g    o    n    e    \u1; C    o    n    t    r    o    l    C    h    a    r    
             Actual Value   | S    t    r    i    n    g    O    n    e    \u0; C    o    n    t    r    o    l    C    h    a    r    
             Expected Code  | 83   116  114  105  110  103  111  110  101  1    67   111  110  116  114  111  108  67   104  97   114  
             Actual Code    | 83   116  114  105  110  103  79   110  101  0    67   111  110  116  114  111  108  67   104  97   114  
             """);
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\u0000ControlChar".ShouldBe("StringOne\u0000ControlChar");
    }
}