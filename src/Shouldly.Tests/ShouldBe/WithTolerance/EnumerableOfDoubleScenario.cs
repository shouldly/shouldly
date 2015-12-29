using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class EnumerableOfDoubleScenario
    {

    [Fact]
    public void EnumerableOfDoubleScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
new[] { Math.PI, Math.PI }.ShouldBe(new[] { 3.24, 3.24 }, 0.01, "Some additional context"),

errorWithSource:
@"new[] { Math.PI, Math.PI }
    should be within
0.01
    of
[3.24, 3.24]
    but was
[3.14159265358979, 3.14159265358979]
    difference
[*3.14159265358979*, *3.14159265358979*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[3.14159265358979, 3.14159265358979]
    should be within
0.01
    of
[3.24, 3.24]
    but was not
    difference
[*3.14159265358979*, *3.14159265358979*]

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { Math.PI, Math.PI }.ShouldBe(new[] { 3.14, 3.14 }, 0.01);
    }
}
}