﻿#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskOfStringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.Throw<InvalidOperationException>(() =>
            {
                var task = Task.Factory.StartNew(() => "Foo",
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
            }, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"Should throw System.InvalidOperationException but does not
Additional Info:
Some additional context"; }
        }

        protected override void ShouldPass()
        {
            var ex = Should.Throw<InvalidOperationException>(() =>
            {
                var task = Task.Factory.StartNew<string>(() => { throw new InvalidOperationException(); },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
            });
            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
#endif