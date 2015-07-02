using NUnit.Framework;
#if net40
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shouldly.Tests.ShouldNotThrowAsync
{
    [TestFixture]
    public class FuncOfTaskAsyncScenario
    {
        [Test]
        public void ShouldThrowAWobbly()
        {
            try
            {
                var task = Task.Factory.StartNew(() => {
                                                           throw new RankException();
                },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                Should.NotThrowAsync(task, TimeSpan.FromMilliseconds(2000), () => "Some additional context").Wait();
                
            }
            catch (AggregateException e)
            {
                var inner = e.Flatten().InnerException;
                var ex = inner.ShouldBeOfType<RankException>();
//                ex.Message.ShouldContainWithoutWhitespace(@"Should not throw System.RankException but does
//                                Additional Info:
//                                Some additional context");
 
            }
                 
        }

        [Test]
        public  void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);
            Should.NotThrowAsync(task,TimeSpan.FromMilliseconds(2),()=>"").Wait();
        }
    }
}
#endif