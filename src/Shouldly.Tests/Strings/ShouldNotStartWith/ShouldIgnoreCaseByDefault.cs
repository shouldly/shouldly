using NUnit.Framework;

namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class ShouldIgnoreCaseByDefault
    {
        [Test]
        public void Test()
        {
            TestHelpers.Should.Error(() =>
                "Cheese".ShouldNotStartWith("CH", () => "Some additional context"),
                "\"Cheese\" should not start with \"CH\" but was \"Cheese\" " +
                "Additional Info: " +
                "Some additional context");
        }
    }
}