#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldThrowAsync
{  
    // TODO When ThrowAsync is put back, uncomment this.
//    [TestFixture]
//    public class FuncOfTaskScenarioAsync
//    {
//        [Test]
//        public void ShouldThrowAWobbly()
//        {
//            try
//            {
//                Should.ThrowAsync<InvalidOperationException>(() =>
//                {
//                    var task = Task.Factory.StartNew(() => { var a = 1 + 1; Console.WriteLine(a); },
//                        CancellationToken.None, TaskCreationOptions.None,
//                        TaskScheduler.Default);
//                    return task;
//                }, "Some additional context").Wait();
//            }
//            catch (AggregateException e)
//            {
//                var inner = e.Flatten().InnerException;
//                var ex = inner.ShouldBeOfType<ShouldAssertException>();
//                ex.Message.ShouldContainWithoutWhitespace(@"
//    The provided expression
//        < throw async>b__26 
//    System.InvalidOperationException
//        but does not
//Additional Info:
//Some additional context");
//            }
//        }

//        [Test]
//        public void ShouldPass()
//        {
//            Should.ThrowAsync<InvalidOperationException>(() =>
//            {
//                var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); },
//                    CancellationToken.None, TaskCreationOptions.None,
//                    TaskScheduler.Default);
//                return task;
//            }).Wait();
//        }
//    }
}
#endif