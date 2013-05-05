using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeOneOfTests
    {
        [Test]
        public void ShouldNotThrowWhenValueIsInTheParams()
        {
            1.ShouldBeOneOf(1, 2, 3);
        }

        [Test]
        public void ShouldThrowWhenValueIsNotInTheParams()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 5.ShouldBeOneOf(1, 2, 3));
        }
    }
}
