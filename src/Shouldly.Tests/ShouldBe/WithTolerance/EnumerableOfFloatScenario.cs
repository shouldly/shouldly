using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class EnumerableOfFloatScenario
    {
    [Fact]
    [UseCulture("en-US")]
    public void EnumerableOfFloatScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
new[] { (float)MathEx.PI, (float)MathEx.PI }.ShouldBe(new[] { 3.24f, 3.24f }, 0.01, "Some additional context"),

errorWithSource:
@"new[] { (float)MathEx.PI, (float)MathEx.PI }
    should be within
0.01d
    of
[3.24f, 3.24f]
    but was
[3.14159f, 3.14159f]
    difference
[*3.14159f*, *3.14159f*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[3.14159f, 3.14159f]
    should be within
0.01d
    of
[3.24f, 3.24f]
    but was not
    difference
[*3.14159f*, *3.14159f*]

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { (float)MathEx.PI, (float)MathEx.PI }.ShouldBe(new[] { 3.14f, 3.14f }, 0.01);
    }
}
}