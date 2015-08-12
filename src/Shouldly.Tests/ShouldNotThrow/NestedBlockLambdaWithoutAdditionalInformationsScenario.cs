using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class NestedBlockLambdaWithoutAdditionalInformationsScenario  : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.NotThrow(() =>
            {
                if (true)
                {
                    throw new Exception("Dummy message.");
                }
            });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return
                    "() => { if (true) { throw new Exception(\"Dummy message.\"); } } should not throw but threw System.Exception with message \"Dummy message.\"";
            }
        }
    }
} 