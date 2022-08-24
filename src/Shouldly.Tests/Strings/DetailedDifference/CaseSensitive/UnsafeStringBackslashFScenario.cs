namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnsafeStringBackslashFScenario
{
    [Fact]
    public void UnsafeStringBackslashFScenarioShouldFail()
    {
        var str = "StringOne\fBackslashF";
        Verify.ShouldFail(() =>
                str.ShouldBe("Stringone BackslashF"),

            errorWithSource:
            @"str
    should be
""Stringone BackslashF""
    but was
""StringOne" + "\f" + @"BackslashF""
    difference
Difference     |                                |              |                                                     
               |                               \|/            \|/                                                    
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   
Expected Value | S    t    r    i    n    g    o    n    e    \s   B    a    c    k    s    l    a    s    h    F    
Actual Value   | S    t    r    i    n    g    O    n    e    \f   B    a    c    k    s    l    a    s    h    F    
Expected Code  | 83   116  114  105  110  103  111  110  101  32   66   97   99   107  115  108  97   115  104  70   
Actual Code    | 83   116  114  105  110  103  79   110  101  12   66   97   99   107  115  108  97   115  104  70   ",

            errorWithoutSource:
            @"""StringOne" + "\f" + @"BackslashF""
    should be
""Stringone BackslashF""
    but was not
    difference
Difference     |                                |              |                                                     
               |                               \|/            \|/                                                    
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   
Expected Value | S    t    r    i    n    g    o    n    e    \s   B    a    c    k    s    l    a    s    h    F    
Actual Value   | S    t    r    i    n    g    O    n    e    \f   B    a    c    k    s    l    a    s    h    F    
Expected Code  | 83   116  114  105  110  103  111  110  101  32   66   97   99   107  115  108  97   115  104  70   
Actual Code    | 83   116  114  105  110  103  79   110  101  12   66   97   99   107  115  108  97   115  104  70   ");
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\fBackslashF".ShouldBe("StringOne\fBackslashF");
    }
}