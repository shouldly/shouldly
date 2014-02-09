using System;
using NUnit.Framework;

namespace Shouldly.Tests
{
    public class ShouldBeOneOfTests
    {
        [Test]
        public void ShouldNotThrowWhenValueIsExpected()
        {
            1.ShouldBeOneOf(1, 2, 3);
            SomeFlags.Val1.ShouldBeOneOf(SomeFlags.Val1, SomeFlags.Val2);
        }

        [Flags]
        enum SomeFlags
        {
            Val1,
            Val2
        }

        [Test]
        public void ShouldThrowWhenValueIsNotExpected()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 1.ShouldBeOneOf(4, 5, 6));
        }

        [Test]
        public void ErrorMessageIsNice()
        {
            Should.Error(
                () => 1.ShouldBeOneOf(4, 5, 6),
                "() => 1 should be one of [4, 5, 6] but was 1");
        }
    }
}
