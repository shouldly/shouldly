using System.Collections.Generic;
using Xunit;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class SameInstanceInDifferentInterfaceCollectionTypes
    {
        interface IFoo { }

        class Foo : IFoo { }

        [Fact]
        public void WhenFoo()
        {
            var foo = new Foo();

            IList<IFoo> a = new List<IFoo>();
            a.Add(foo);

            a.ShouldBe(new IFoo[] { foo });
        }
    }
}