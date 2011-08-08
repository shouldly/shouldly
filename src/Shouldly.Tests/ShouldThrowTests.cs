using System;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldThrowTests
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
        public void ShouldThrow_WhenItThrowsIncorrectException()
        {
            Action shouldThrowAction =
            () => Shouldly.Should.Throw<NotImplementedException>(() =>
            {
                throw new RankException();
            });

            Should.Error(shouldThrowAction, "() => Shouldly.Should throw System.NotImplementedException but was System.RankException");
        }
        
        [Test]
        public void ShouldThrow_WhenItDoesntThrow()
        {
            Action shouldThrowAction =
            () => Shouldly.Should.Throw<NotImplementedException>(() =>
            {
            });

            Should.Error(shouldThrowAction, "() => Shouldly.Should throw System.NotImplementedException but does not");
        }
    }
}