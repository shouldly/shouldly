using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive
{
    public class UnsafeStringNewLineScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "StringOne\r\nNewline".ShouldBe("Stringone\r\nNewline", StringCompareShould.IgnoreCase);
        }

        protected override void ShouldThrowAWobbly()
        {
            "StringOneNoNewLine".ShouldBe("Stringone\r\nNewLine", StringCompareShould.IgnoreCase);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""StringOneNoNewLine""
                        should be
                    ""Stringone
NewLine""
                        but was
                    ""StringOneNoNewLine""
                        difference
                        Case Insensitive and Line Ending Sensitive Comparison
                        Difference     |                                               |    |                                      
                                       |                                              \|/  \|/                                     
                        Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   
                        Expected Value | S    t    r    i    n    g    o    n    e    \r   \n   N    e    w    L    i    n    e    
                        Actual Value   | S    t    r    i    n    g    O    n    e    N    o    N    e    w    L    i    n    e    
                        Expected Code  | 83   116  114  105  110  103  111  110  101  13   10   78   101  119  76   105  110  101  
                        Actual Code    | 83   116  114  105  110  103  79   110  101  78   111  78   101  119  76   105  110  101  "
                    ;
                }
        }

    }
}
