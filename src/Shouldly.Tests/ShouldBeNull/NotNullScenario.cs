using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeNull
{
    public class NotNullScenario
    {
        [Fact]
        public void ShouldFailForNullReference()
        {
            string? myNullRef = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Verify.ShouldFail(() =>
myNullRef.ShouldNotBeNull("Some additional context"),

errorWithSource:
@"myNullRef
    should not be null but was

Additional Info:
    Some additional context",

errorWithoutSource:
@"null
    should not be null but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPassForNonNullReference()
        {
            "Hello World".ShouldNotBeNull();
        }

        [Fact]
        public void ShouldFailForSystemNullableWithoutValue()
        {
            int? myNullRef = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Verify.ShouldFail(() =>
                    myNullRef.ShouldNotBeNull("Some additional context"),

                errorWithSource:
                @"myNullRef
    should not be null but was

Additional Info:
    Some additional context",

                errorWithoutSource:
                @"null
    should not be null but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPassForSystemNullableWithValue()
        {
            ((int?)0).ShouldNotBeNull();
        }
    }
}