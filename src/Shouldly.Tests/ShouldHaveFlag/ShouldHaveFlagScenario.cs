using Shouldly.ShouldlyExtensionMethods;

namespace Shouldly.Tests.ShouldHaveFlag;

public class ShouldHaveFlagScenario
{
    [Fact]
    public void FlagScenarioShouldFail()
    {
        var actual = TestEnum.FlagTwo;
        var value = TestEnum.FlagOne;

        Verify.ShouldFail(() =>
                actual.ShouldHaveFlag(value, "Some additional context"),
            errorWithSource:
            """
            actual
                should have flag
            TestEnum.FlagOne
                but had
            TestEnum.FlagTwo

            Additional Info:
                Some additional context
            """,
            errorWithoutSource:
            """
            TestEnum.FlagTwo
                should have flag
            TestEnum.FlagOne
                but did not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldThrowException()
    {
        var actual = TestEnumWithoutFlagAttribute.FlagOne;
        var value = TestEnumWithoutFlagAttribute.FlagOne;

        Should.Throw<ArgumentException>(() => actual.ShouldHaveFlag(value));
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

public enum TestEnumWithoutFlagAttribute
{
    FlagOne,
    FlagTwo,
    FlagThree
}