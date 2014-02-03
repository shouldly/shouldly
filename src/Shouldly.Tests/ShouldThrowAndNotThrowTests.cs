using System;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldThrowAndNotThrowTests
    {
        [Test]
        public void ShouldThrow_WhenItThrowsCorrectException()
        {
            Action shouldThrowAction =
                () => Shouldly.Should.Throw<NotImplementedException>(() =>
                {
                    throw new NotImplementedException();
                });

            Should.NotError(shouldThrowAction);
        }

        [Test]
        public void ShouldNotThrow_IfCallDoesNotThrow_ShouldDoNothing()
        {
            Should.NotError(
                () => Shouldly.Should.NotThrow(() => {})
                );
            
        }

        [Test]
        public void ShouldNotThrow_IfCallDoesNotThrow_ShouldDoNothingAndReturnValue()
        {
            Should.NotError(
                () => Shouldly.Should.NotThrow(() => 1).ShouldBe(1));
        }
    }
}