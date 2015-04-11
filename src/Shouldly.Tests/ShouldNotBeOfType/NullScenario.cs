using NUnit.Framework;

namespace Shouldly.Tests.ShouldNotBeOfType
{
    public class NullIsNotOfType
    {
        [Test]
        public void AndShouldNotThrow()
        {
            object o = null;
            o.ShouldNotBeOfType<int>(() => "Some additional context");
        }

        //TODO Test of null.ShouldNotBeOfType<int?>()
    }
}