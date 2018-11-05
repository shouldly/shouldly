using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldlyConfigurations
{
    public class DifferenceHighlightingCanBeDisabledTest
    {
        [Fact]
        public void DifferenceHighlightingDisabled()
        {
            using (ShouldlyConfiguration.DisableDifferenceHighlighting())
            {
                var message = "Hello World";
                Verify.ShouldFail(() =>
                        message.ShouldBe("Hello World2", "Some additional context"),

                    errorWithSource:
@"message
    should be
""Hello World""
    but was
Hello World2

Additional Info:
    Some additional context",

                    errorWithoutSource:
@"Hello World
    should be
""Hello World2""
    but was not

Additional Info:
    Some additional context");
            }
        }
    }
}