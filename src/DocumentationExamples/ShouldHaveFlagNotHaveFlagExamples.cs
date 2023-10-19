using Shouldly.ShouldlyExtensionMethods;

public class ShouldHaveFlagNotHaveFlagExamples
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ShouldHaveFlagNotHaveFlagExamples(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ShouldHaveFlag()
    {
        DocExampleWriter.Document(
            () =>
            {
                var actual = TestEnum.FlagTwo;
                var value = TestEnum.FlagOne;
                actual.ShouldHaveFlag(value);
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotHaveFlag()
    {
        DocExampleWriter.Document(
            () =>
            {
                var actual = TestEnum.FlagOne;
                var value = TestEnum.FlagOne;
                actual.ShouldNotHaveFlag(value);
            },
            _testOutputHelper);
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
}