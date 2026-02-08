namespace Shouldly.Tests.ShouldBeOfTypes;

public class BasicScenarios
{
    [Fact]
    public void ArrayTypesMatchExactly()
    {
        var arr = new object[] { new Added(), new Changed(), new Removed() };

        arr.ShouldBeOfTypes(typeof(Added), typeof(Changed), typeof(Removed));
    }

    [Fact]
    public void ArrayTypesMatchExactlyWithCustomContext()
    {
        var arr = new object[] { new Added(), new Changed(), new Removed() };

        arr.ShouldBeOfTypes([typeof(Added), typeof(Changed), typeof(Removed)], "additional context");
    }

    [Fact]
    public void FailsIfTypesDontMatchExactly()
    {
        Verify.ShouldFail(() =>
            new object[] { new Added(), new Changed() }.ShouldBeOfTypes([typeof(Added), typeof(object)], "Some additional context"));
    }

    [Fact]
    public void FailsIfActualAndExpectedAreDifferentLengths()
    {
        Verify.ShouldFail(() =>
            new object[] { new Added(), new Changed() }.ShouldBeOfTypes([typeof(Added)], "Some additional context"));
    }

    private class Added { }
    private class Changed { }
    private class Removed { }
}