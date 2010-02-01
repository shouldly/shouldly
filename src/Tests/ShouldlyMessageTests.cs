using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    [ShouldlyMethods]
    public class ShouldlyMessageTests
    {
        [Test]
        public void CanGenerateErrorMessage()
        {
            new ShouldlyMessage("expected", "actual").ToString()
                .ShouldBe(@"            new ShouldlyMessage(""expected"", ""actual"").ToString()
        can generate error message
    ""expected""
        but was
    ""actual""");
        }
    }
}