#if net40
using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests
{
    public class TaskSupport
    {
        [Test]
        public void ShouldThrowObservesTask()
        {
            var ex = Assert.Throws<ChuckedAWobbly>(()=>
                Should.Throw<NotImplementedException>(() => Task.Factory.StartNew(() =>
                {
                    throw new RankException();
                })));

            ex.Message.ShouldContainWithoutWhitespace("Should throw System.NotImplementedException but was System.RankException");

            Action shouldThrowAction =
            () => Should.Throw<NotImplementedException>(() => Task.Factory.StartNew(() => { }));

            TestHelpers.Should.Error(shouldThrowAction, "() => Should throw System.NotImplementedException but does not");
        }

        [Test]
        public void ShouldNotThrowObservesTask()
        {
            Should.NotThrow(() => Task.Factory.StartNew(() => { }));

            // NotThrow<T>(Func<Task> ) overload
            var error = Assert.Throws<ChuckedAWobbly>(()=>
                Should.NotThrow(() => Task.Factory.StartNew(() => { throw new IndexOutOfRangeException(); })));
            error.Message.ShouldContainWithoutWhitespace("Should not throw System.IndexOutOfRangeException but does");

            // NotThrow<T>(Func<Task<T>> ) overload
            error = Assert.Throws<ChuckedAWobbly>(()=>
                Should.NotThrow(() => Task.Factory.StartNew<string>(() => { throw new IndexOutOfRangeException(); })));
            error.Message.ShouldContainWithoutWhitespace("Should not throw System.IndexOutOfRangeException but does");

            var result = Should.NotThrow(() => Task.Factory.StartNew(() => "foo"));
            result.ShouldBe("foo");
            TestHelpers.Should.Error(() =>
                Should.NotThrow(() => Task.Factory.StartNew(() => { throw new IndexOutOfRangeException(); })),
                "Should not throw System.IndexOutOfRangeException but does");
        }

        [Test]
        public void HandlesExceptionThrownOutsideTask()
        {
            // ReSharper disable once RedundantDelegateCreation (forces Throw(Func<Task>) overload)
            var ex = Assert.Throws<ChuckedAWobbly>(() => 
                Should.Throw<NotImplementedException>(new Func<Task>(() => { throw new IndexOutOfRangeException(); })));
            ex.Message.ShouldContainWithoutWhitespace("Should throw System.NotImplementedException but was System.IndexOutOfRangeException");
            
            // Specify Func<Task<T>> overload
            ex = Assert.Throws<ChuckedAWobbly>(() => 
                Should.NotThrow(new Func<Task<string>>(() => { throw new IndexOutOfRangeException(); })));
            ex.Message.ShouldContainWithoutWhitespace("Should not throw System.IndexOutOfRangeException but does");

            // ReSharper disable once RedundantDelegateCreation (forces NotThrow(Func<Task>) overload)
            ex = Assert.Throws<ChuckedAWobbly>(() => 
                Should.NotThrow(new Func<Task>(() => { throw new IndexOutOfRangeException(); })));
            ex.Message.ShouldContainWithoutWhitespace("Should not throw System.IndexOutOfRangeException but does");
        }
    }
}
#endif