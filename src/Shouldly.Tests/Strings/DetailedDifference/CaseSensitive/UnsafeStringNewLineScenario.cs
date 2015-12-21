﻿using Xunit;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive
{
    public class UnsafeStringNewLineScenario
    {
        [Fact]
        public void UnsafeStringNewLineScenarioShouldFail()
        {
            var str = "StringOneNoNewLine";
            Verify.ShouldFail(() =>
str.ShouldBe("Stringone\r\nNewLine"),

errorWithSource:
@"str
    should be
""Stringone
NewLine""
    but was
""StringOneNoNewLine""
    difference
Difference     |                                |              |    |                                      
               |                               \|/            \|/  \|/                                     
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   
Expected Value | S    t    r    i    n    g    o    n    e    \r   \n   N    e    w    L    i    n    e    
Actual Value   | S    t    r    i    n    g    O    n    e    N    o    N    e    w    L    i    n    e    
Expected Code  | 83   116  114  105  110  103  111  110  101  13   10   78   101  119  76   105  110  101  
Actual Code    | 83   116  114  105  110  103  79   110  101  78   111  78   101  119  76   105  110  101  ",

errorWithoutSource:
@"""StringOneNoNewLine""
    should be
""Stringone
NewLine""
    but was not
    difference
Difference     |                                |              |    |                                      
               |                               \|/            \|/  \|/                                     
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   
Expected Value | S    t    r    i    n    g    o    n    e    \r   \n   N    e    w    L    i    n    e    
Actual Value   | S    t    r    i    n    g    O    n    e    N    o    N    e    w    L    i    n    e    
Expected Code  | 83   116  114  105  110  103  111  110  101  13   10   78   101  119  76   105  110  101  
Actual Code    | 83   116  114  105  110  103  79   110  101  78   111  78   101  119  76   105  110  101  ");
        }

        [Fact]
        public void ShouldPass()
        {
            "StringOne\r\nNewLine".ShouldBe("StringOne\r\nNewLine");
        }
    }
}
