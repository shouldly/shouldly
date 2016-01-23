using System;
using System.Collections.Generic;
using System.Text;
using Shouldly.ShouldlyExtensionMethods;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.Shared.ShouldNotHaveFlag
{
    public class ShouldNotHaveFlagScenarios
    {
        [Fact]
        public void FlagScenarioShouldFail()
        {
            var actual = TestEnum.FlagOne;
            var value = TestEnum.FlagOne;

            Verify.ShouldFail(() => actual.ShouldNotHaveFlag(value, "Some additional context"),
                errorWithSource:
@"Verify
    should not have flag
TestEnum.FlagOne
    but it had
TestEnum.FlagOne

Additional Info:
    Some additional context",
                errorWithoutSource:
@"TestEnum.FlagOne
    should not have flag
TestEnum.FlagOne
    but had

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPassOneFlagSet()
        {
            var actual = TestEnum.FlagOne;
            var value = TestEnum.FlagTwo;

            actual.ShouldNotHaveFlag(value);
        }

        [Fact]
        public void ShouldPassTwoFlagsSet()
        {
            var actual = TestEnum.FlagOne | TestEnum.FlagTwo;
            var value = TestEnum.FlagThree;

            actual.ShouldNotHaveFlag(value);
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
