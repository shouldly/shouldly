using System;
using System.Collections.Generic;
using System.Text;
using Shouldly.ShouldlyExtensionMethods;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.Shared.ShouldHaveFlag
{
    public class ShouldHaveFlagScenario
    {
        [Fact]
        public void FlagScenarioShouldFail()
        {
            var actual = TestEnum.FlagTwo;
            var value = TestEnum.FlagOne;

            Verify.ShouldFail(() => actual.ShouldHaveFlag(value, "Some additional context"), 
                errorWithSource: 
@"Verify
    should have flag
TestEnum.FlagOne
    but had
TestEnum.FlagTwo

Additional Info:
    Some additional context", 
                errorWithoutSource:
@"TestEnum.FlagTwo
    should have flag
TestEnum.FlagOne
    but did not

Additional Info:
    Some additional context");
        }
        
        [Fact]
        public void ShouldPassOneFlagSet()
        {
            var actual = TestEnum.FlagOne;
            var value = TestEnum.FlagOne;

            actual.ShouldHaveFlag(value);
        }

        [Fact]
        public void ShouldPassTwoFlagsSet()
        {
            var actual = TestEnum.FlagOne | TestEnum.FlagTwo;
            var value = TestEnum.FlagTwo;

            actual.ShouldHaveFlag(value);
        }
    }

    [Flags]
    public enum TestEnum
    {
        FlagOne = 1,
        FlagTwo = 2,
        FlagThree = 4
    }
}
