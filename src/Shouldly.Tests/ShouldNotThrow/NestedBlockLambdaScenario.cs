using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class NestedBlockLambdaScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.NotThrow(() =>
            {
                if (true)
                {
                    throw new Exception("Dummy message.");
                }
            }, () => "Additional info");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "`if (true) { throw new Exception(\"Dummy message.\"); }` " +
                       "should not throw but threw System.Exception with message \"Dummy message.\" Additional Info: Additional info";
            }
        }
    }
} 