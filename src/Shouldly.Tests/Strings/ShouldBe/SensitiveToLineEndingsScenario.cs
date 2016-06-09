﻿using Xunit;

namespace Shouldly.Tests.Strings.ShouldBe
{
    public class SensitiveToLineEndingsScenario
    {
        [Fact]
        public void SensitiveToLineEndingsScenarioShouldFail()
        {
            var str = "line1\nline2";
            Verify.ShouldFail(() =>
str.ShouldBe("line1\r\nline2", () => "Some additional context"),

errorWithSource:
@"str
    should be
""line1" + "\r\n" + @"line2""
    but was
""line1" + "\n" + @"line2""
    difference
Difference     |                           |    |    |    |    |    |    |   
               |                          \|/  \|/  \|/  \|/  \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   
Expected Value | l    i    n    e    1    \r   \n   l    i    n    e    2    
Actual Value   | l    i    n    e    1    \n   l    i    n    e    2         
Expected Code  | 108  105  110  101  49   13   10   108  105  110  101  50   
Actual Code    | 108  105  110  101  49   10   108  105  110  101  50        

Additional Info:
    Some additional context",

errorWithoutSource:
@"""line1" + "\n" + @"line2""
    should be
""line1" + "\r\n" + @"line2""
    but was not
    difference
Difference     |                           |    |    |    |    |    |    |   
               |                          \|/  \|/  \|/  \|/  \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   
Expected Value | l    i    n    e    1    \r   \n   l    i    n    e    2    
Actual Value   | l    i    n    e    1    \n   l    i    n    e    2         
Expected Code  | 108  105  110  101  49   13   10   108  105  110  101  50   
Actual Code    | 108  105  110  101  49   10   108  105  110  101  50        

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "line1\nline2".ShouldBe("line1\nline2");
        }
    }
}
