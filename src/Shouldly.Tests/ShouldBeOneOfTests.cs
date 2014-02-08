﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shouldly.Tests
{
    public class ShouldBeOneOfTests
    {
        [Test]
        public void ShouldNotThrowWhenValueIsExpected()
        {
            1.ShouldBeOneOf(1, 2, 3);
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
