using System;
using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class EnumerableOfFloatScenario
    {

    [Fact]
    [UseCulture("en-US")]
    public void EnumerableOfFloatScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
new[] { (float)Math.PI, (float)Math.PI }.ShouldBe(new[] { 3.24f, 3.24f }, 0.01, "Some additional context"),

errorWithSource:
@"new[] { (float)Math.PI, (float)Math.PI }
    should be within
0.01d
    of
[3.24f, 3.24f]
    but was
[3.141593f, 3.141593f]
    difference
[*3.141593f*, *3.141593f*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[3.141593f, 3.141593f]
    should be within
0.01d
    of
[3.24f, 3.24f]
    but was not
    difference
[*3.141593f*, *3.141593f*]

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { (float)Math.PI, (float)Math.PI }.ShouldBe(new[] { 3.14f, 3.14f }, 0.01);
    }
}
}