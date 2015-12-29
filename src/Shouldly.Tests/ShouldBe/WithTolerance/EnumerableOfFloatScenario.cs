using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class EnumerableOfFloatScenario
    {

    [Fact]
    public void EnumerableOfFloatScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
new[] { (float)Math.PI, (float)Math.PI }.ShouldBe(new[] { 3.24f, 3.24f }, 0.01, "Some additional context"),

errorWithSource:
@"new[] { (float)Math.PI, (float)Math.PI }
    should be within
0.01
    of
[3.24, 3.24]
    but was
[3.141593, 3.141593]
    difference
[*3.141593*, *3.141593*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[3.141593, 3.141593]
    should be within
0.01
    of
[3.24, 3.24]
    but was not
    difference
[*3.141593*, *3.141593*]

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