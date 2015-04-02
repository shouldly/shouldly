using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive
{
    public class UnsafeStringControlCharacterScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "StringOne\u0000ControlCharacter".ShouldBe("StringOne\u0000ControlCharacter", Case.Sensitive);
        }

        protected override void ShouldThrowAWobbly()
        {
            "StringOne\u0000ControlCharacter".ShouldBe("Stringone\u0001ControlCharacter", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""StringOne\u0000ControlCharacter""" + 
                            "should be" +
                        "\"Stringone\u0001ControlCharacter\"" +
                            "but was" +
                            "\"StringOne\u0000ControlCharacter\"" +
                            @"difference
                          Case Sensitive Comparison
                          Difference     |                                |              |                                                                                   
                                         |                               \|/            \|/                                                                                  
                          Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   21   22   23   24   25   
                          Expected Value | S    t    r    i    n    g    o    n    e    \u1; C    o    n    t    r    o    l    C    h    a    r    a    c    t    e    r    
                          Actual Value   | S    t    r    i    n    g    O    n    e    \u0; C    o    n    t    r    o    l    C    h    a    r    a    c    t    e    r    
                          Expected Code  | 83   116  114  105  110  103  111  110  101  1    67   111  110  116  114  111  108  67   104  97   114  97   99   116  101  114  
                          Actual Code    | 83   116  114  105  110  103  79   110  101  0    67   111  110  116  114  111  108  67   104  97   114  97   99   116  101  114  "
                    ;
                }
        }

    }
}
