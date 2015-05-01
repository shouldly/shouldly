using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringCaseIsInsensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            try
            {
                "Shouldly is legendary".ShouldNotContain("LEGENDARY", Case.Insensitive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Shouldly is legendary\" should not contain case insensitive \"LEGENDARY\" but does"; } 
        }

        protected override void ShouldPass()
        {
            "Shouldly is legendary".ShouldNotContain("LEGEND-wait for it-ary", Case.Insensitive);
        }
    }
}