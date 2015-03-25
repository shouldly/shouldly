using NUnit.Framework;
#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrowAsync
{
    [TestFixture]
    public class FuncOfTaskScenarioAsync : ShouldlyShouldTestScenario
    {
       
        protected override void ShouldThrowAWobbly()
        {
            Should.ThrowAsync<InvalidOperationException>(() =>
            {
                var task = Task.Factory.StartNew(() => { var a = 1 + 1; Console.WriteLine(a); },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
            }).Wait();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should throw System.InvalidOperationException but does not"; }
        }

        
        protected  override void ShouldPass()
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

    [TestFixture]
    [Ignore]
    public class Tests
    {
        public class MyClass
        {
            public int Add(int a, int b)
            {
                //if (a > 5)
                //    throw new ArgumentException("a is greater than 5");
                return a + b;
            }
        }

        [Test]
        public void ExceptionTest()
        {
            var m = new MyClass();
            Should.ThrowAsync<ArgumentException>(() => Task.Factory.StartNew(() => m.Add(6, 1))).Wait();
        }
    }
}
#endif