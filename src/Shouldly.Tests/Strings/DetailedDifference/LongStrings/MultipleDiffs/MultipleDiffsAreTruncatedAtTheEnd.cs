using NUnit.Framework;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.LongStrings.MultipleDiffs
{
    [Ignore]
    public class MultipleDiffsAreTruncatedAtTheEnd : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "StringOne".ShouldBe("Stringone", Case.Insensitive);
        }

        protected override void ShouldThrowAWobbly()
        {
            "StringOneX".ShouldBe("Stringone", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""StringOneX""
                        should be
                    ""Stringone""
                        but was
                    ""StringOneX""
                        difference
                    Case Insensitive Comparison
                    Difference     |                                               |   
                                   |                                              \|/   
                    Index          | 0    1    2    3    4    5    6    7    8    9    
                    Expected Value | S    t    r    i    n    g    o    n    e         
                    Actual Value   | S    t    r    i    n    g    O    n    e    X    
                    Expected Code  | 83   116  114  105  110  103  111  110  101       
                    Actual Code    | 83   116  114  105  110  103  79   110  101  88   "
                    ;
                }
        }
    }
}
