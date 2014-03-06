using NUnit.Framework;

namespace Shouldly.Tests.Strings.ShouldEndWith
{
    public class ShouldIgnoreCaseByDefault
    {
        [Test]
        public void Test()
        {
            "Cheese".ShouldEndWith("SE");
        }
    }
}