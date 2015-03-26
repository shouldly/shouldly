using NUnit.Framework;
#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrowAsync
{  
    [TestFixture]
    public class FuncOfTaskScenarioAsync
    {
       
        [Test]
        public void ShouldThrowAWobbly()
        {
            try
            {
               
                Should.ThrowAsync<InvalidOperationException>(() =>
                {
                    var task = Task.Factory.StartNew(() => { var a = 1 + 1; Console.WriteLine(a); },
                        CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default);
                    return task;
                }).Wait();
            }
            catch (AggregateException e)
            {
                var inner = e.Flatten().InnerException;
                inner.ShouldBeOfType<ShouldAssertException>();
            }
           
        }

        [Test]
        public void ShouldPass()
        {
            Should.ThrowAsync<InvalidOperationException>(() =>
            {
                var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
            }).Wait();
        }
    }
}
#endif