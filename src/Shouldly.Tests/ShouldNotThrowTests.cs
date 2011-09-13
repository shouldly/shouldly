using System;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldNotThrowTests
    {
        [Test]
        public void ShouldNotThrow_IfCallThrows_ShouldShowException()
        {
            Should.Error(
                () => Shouldly.Should.NotThrow(() => { throw new IndexOutOfRangeException(); }),
                "() => Shouldly.Should not throw System.IndexOutOfRangeException but does"
                );
        }

        [Test]
        public void ShouldNotThrow_IfCallDoesNotThrow_ShouldDoNothing()
        {
            Should.NotError(
                () => Shouldly.Should.NotThrow(() => {})
                );
            
        }
    }
}