﻿using Xunit;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive
{
    public class UnsafeStringBackslashVScenario
    {
        [Fact]
        public void UnsafeStringBackslashVScenarioShouldFail()
        {
            var str = "StringOne\vBackslashV";
            Verify.ShouldFail(() =>
str.ShouldBe("Stringone BackslashV"),

errorWithSource:
@"str
    should be
""Stringone BackslashV""
    but was
""StringOne" + "\v" + @"BackslashV""
    difference
Difference     |                                |              |                                                     
               |                               \|/            \|/                                                    
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   
Expected Value | S    t    r    i    n    g    o    n    e    \s   B    a    c    k    s    l    a    s    h    V    
Actual Value   | S    t    r    i    n    g    O    n    e    \v   B    a    c    k    s    l    a    s    h    V    
Expected Code  | 83   116  114  105  110  103  111  110  101  32   66   97   99   107  115  108  97   115  104  86   
Actual Code    | 83   116  114  105  110  103  79   110  101  11   66   97   99   107  115  108  97   115  104  86   ",

    errorWithoutSource:
@"""StringOne" + "\v" + @"BackslashV""
    should be
""Stringone BackslashV""
    but was not
    difference
Difference     |                                |              |                                                     
               |                               \|/            \|/                                                    
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   
Expected Value | S    t    r    i    n    g    o    n    e    \s   B    a    c    k    s    l    a    s    h    V    
Actual Value   | S    t    r    i    n    g    O    n    e    \v   B    a    c    k    s    l    a    s    h    V    
Expected Code  | 83   116  114  105  110  103  111  110  101  32   66   97   99   107  115  108  97   115  104  86   
Actual Code    | 83   116  114  105  110  103  79   110  101  11   66   97   99   107  115  108  97   115  104  86   ");
        }

        [Fact]
        public void ShouldPass()
        {
            "StringOne\vBackslashV".ShouldBe("StringOne\vBackslashV");
        }
    }
}
