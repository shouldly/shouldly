using NUnit.Framework;

namespace Shouldly.Tests.Strings.ShouldStartWith
{
    public class ShouldIgnoreCaseByDefault
    {
        [Test]
        public void Test()
        {
            "Cheese".ShouldStartWith("CH");
        }
    }
}