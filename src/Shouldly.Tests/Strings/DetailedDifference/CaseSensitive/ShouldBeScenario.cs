﻿using Xunit;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive
{
    public class ShouldBeScenario
    {
        [Fact]
        public void ShouldBeScenarioShouldFail()
        {
            var str = "Stringone";
            Verify.ShouldFail(() =>
str.ShouldBe("StringOne"),

errorWithSource:
@"str
    should be
""StringOne""
    but was
""Stringone""
    difference
Difference     |                                |             
               |                               \|/            
Index          | 0    1    2    3    4    5    6    7    8    
Expected Value | S    t    r    i    n    g    O    n    e    
Actual Value   | S    t    r    i    n    g    o    n    e    
Expected Code  | 83   116  114  105  110  103  79   110  101  
Actual Code    | 83   116  114  105  110  103  111  110  101  ",

errorWithoutSource:
@"""Stringone""
    should be
""StringOne""
    but was not
    difference
Difference     |                                |             
               |                               \|/            
Index          | 0    1    2    3    4    5    6    7    8    
Expected Value | S    t    r    i    n    g    O    n    e    
Actual Value   | S    t    r    i    n    g    o    n    e    
Expected Code  | 83   116  114  105  110  103  79   110  101  
Actual Code    | 83   116  114  105  110  103  111  110  101  ");
        }

        [Fact]
        public void ShouldPass()
        {
            "StringOne".ShouldBe("StringOne");
        }
    }
}