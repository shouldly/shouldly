#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests.ShouldThrowAsync
{  
    public class FuncOfTaskScenarioAsync
   
    {

    [Fact]
        public void ShouldThrowAWobbly()
        {
            try
            {
                Task task = Task.Factory.StartNew(() => { var a = 1 + 1; Console.WriteLine(a); },
                        CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default);

                var result = task.ShouldThrowAsync<InvalidOperationException>("Some additional context");
                result.Wait();
            }
            catch (AggregateException e)
            {
                var inner = e.Flatten().InnerException;
                var ex = inner.ShouldBeOfType<ShouldAssertException>();
                ex.Message.ShouldContainWithoutWhitespace(@"
                            `task` should throw System.InvalidOperationException but did not
                            Additional Info:
                            Some additional context");
            }
        }

        [Fact]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var result = task.ShouldThrowAsync<InvalidOperationException>();
            result.Wait();
        }
    }
}
#endif