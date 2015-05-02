﻿#if net40
using System;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class TaskThrowsDifferentExceptionScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.Throw<InvalidOperationException>(Task.Factory.StartNew(() => { throw new RankException(); }), "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"Should throw System.InvalidOperationException but was System.RankException
Additional Info:
Some additional context"; }
        }

        protected override void ShouldPass()
        {
            var ex = Should.Throw<InvalidOperationException>(Task.Factory.StartNew(() => { throw new InvalidOperationException(); }));
            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
#endif