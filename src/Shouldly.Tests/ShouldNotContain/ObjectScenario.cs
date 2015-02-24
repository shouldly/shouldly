﻿using Shouldly.Tests.TestHelpers;
using System;

namespace Shouldly.Tests.ShouldNotContain
{
    public class ObjectScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            new[] { a, b, c }.ShouldNotContain(c);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"
    new[] { a, b, c }
        should not contain
    System.Object
        but does";
            }

        }

        protected override void ShouldPass()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            var d = new Object();
            new[] { a, b, c }.ShouldNotContain(d);
        }
    }
}