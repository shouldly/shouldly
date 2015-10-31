using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive
{
    public class UnsafeStringBackslashVScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "StringOne\vBackslashV".ShouldBe("Stringone\vBackslashV", ShouldBeStringOptions.IgnoreCase);
        }

        protected override void ShouldThrowAWobbly()
        {
            "StringOne\vBackslashV".ShouldBe("Stringone BackslashV", ShouldBeStringOptions.IgnoreCase);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"""StringOne\vBackslashV""
                            should be
                        ""Stringone BackslashV""
                            but was" +
                            "\"StringOne\vBackslashV\"" +
                            @"difference
                          Case Insensitive and Line Ending Sensitive Comparison
                          Difference     |                                               |                  
                                         |                                              \|/                 
                          Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   
                          Expected Value | S    t    r    i    n    g    o    n    e    \s   B    a    c    k    s    l    a    s    h    V    
                          Actual Value   | S    t    r    i    n    g    O    n    e    \v   B    a    c    k    s    l    a    s    h    V    
                          Expected Code  | 83   116  114  105  110  103  111  110  101  32   66   97   99   107  115  108  97   115  104  86   
                          Actual Code    | 83   116  114  105  110  103  79   110  101  11   66   97   99   107  115  108  97   115  104  86   "
                    ;
                }
        }

    }
}
