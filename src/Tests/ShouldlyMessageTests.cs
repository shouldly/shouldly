using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class ShouldlyMessageTests
    {
        [Test]
        public void CanGenerateErrorMessage()
        {
            new ShouldlyMessage("expected", "actual").ToString()
                .ShouldBe("Expected expected but was actual");


        }
    }
}