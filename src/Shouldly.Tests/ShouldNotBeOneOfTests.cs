﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shouldly.Tests
{
    public class ShouldNotBeOneOfTests
    {
        [Test]
        public void ShouldThrowWhenValueIsExpected()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 1.ShouldNotBeOneOf(1, 2, 3));
        }

        [Test]
        public void ShouldNotThrowWhenValueIsNotExpected()
        {
            1.ShouldNotBeOneOf(4, 5, 6);
        }

        [Test]
        public void ErrorMessageIsNice()
        {
            Should.Error(
                () => 1.ShouldNotBeOneOf(1, 2, 3),
                "() => 1 should not be one of [1, 2, 3] but was 1");
        }
    }
}
