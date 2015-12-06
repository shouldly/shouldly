using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe
{
    public class BadEquatibleClassScenario
    {
        [Fact]
        public void BadEquatibleClassScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    new BadEquatable().ShouldBe(new BadEquatable(), "Some additional context"),

    errorWithSource:
@"new BadEquatable()
    should be
Shouldly.Tests.ShouldBe.BadEquatibleClassScenario+BadEquatable (000000)
    but was
Shouldly.Tests.ShouldBe.BadEquatibleClassScenario+BadEquatable (000000)

Additional Info:
    Some additional context",

    errorWithoutSource:
@"Shouldly.Tests.ShouldBe.BadEquatibleClassScenario+BadEquatable (000000)
    should be
Shouldly.Tests.ShouldBe.BadEquatibleClassScenario+BadEquatable (000000)
    but was not

Additional Info:
    Some additional context");
        }

        public class BadEquatable : IEquatable<BadEquatable>
        {
            public bool Equals(BadEquatable other)
            {
                return false;
            }
        }

        [Fact]
        public void ShouldPass()
        {
            var instance = new BadEquatable();
            instance.ShouldBe(instance);
        }
    }
}