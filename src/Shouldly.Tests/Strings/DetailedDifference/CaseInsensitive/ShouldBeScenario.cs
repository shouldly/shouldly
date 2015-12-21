﻿using Xunit;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive
{
    public class CaseInsensitiveShouldBeScenario
    {
        [Fact]
        public void CaseInsensitiveShouldBeScenarioShouldFail()
        {
            var str = "StringOneX";
            Verify.ShouldFail(() =>
str.ShouldBe("Stringone", StringCompareShould.IgnoreCase),

errorWithSource:
@"str
    should be with options: Ignoring case
""Stringone""
    but was
""StringOneX""
    difference
Difference     |                                               |   
               |                                              \|/  
Index          | 0    1    2    3    4    5    6    7    8    9    
Expected Value | S    t    r    i    n    g    o    n    e         
Actual Value   | S    t    r    i    n    g    O    n    e    X    
Expected Code  | 83   116  114  105  110  103  111  110  101       
Actual Code    | 83   116  114  105  110  103  79   110  101  88   ",

errorWithoutSource:
@"""StringOneX""
    should be with options: Ignoring case
""Stringone""
    but was not
    difference
Difference     |                                               |   
               |                                              \|/  
Index          | 0    1    2    3    4    5    6    7    8    9    
Expected Value | S    t    r    i    n    g    o    n    e         
Actual Value   | S    t    r    i    n    g    O    n    e    X    
Expected Code  | 83   116  114  105  110  103  111  110  101       
Actual Code    | 83   116  114  105  110  103  79   110  101  88   ");
        }

        [Fact]
        public void ShouldPass()
        {
            "StringOne".ShouldBe("Stringone", StringCompareShould.IgnoreCase);
        }
    }
}