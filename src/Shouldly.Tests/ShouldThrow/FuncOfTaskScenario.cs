﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskScenario
    {

        [Fact]
        public void FuncOfTaskScenarioShouldFail()
        {
            var task = Task.Factory.StartNew(() => { },
                            CancellationToken.None, TaskCreationOptions.None,
                            TaskScheduler.Default);

            Verify.ShouldFail(() =>
            task.ShouldThrow<InvalidOperationException>("Some additional context"),

errorWithSource:
@"Task `task`
    should throw
System.InvalidOperationException
    but did not

Additional Info:
    Some additional context",

errorWithoutSource:
@"Task
    should throw
System.InvalidOperationException
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);

            var ex = task.ShouldThrow<InvalidOperationException>();

            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
