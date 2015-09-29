using System.Collections.Generic;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class DocsExample
    {
        [Test]
        public void Docs()
        {
            var apu = new Person { Name = "Apu" };
            var homer = new Person { Name = "Homer" };
            var skinner = new Person { Name = "Skinner" };
            var barney = new Person { Name = "Barney" };
            var theBeSharps = new List<Person> { homer, skinner, barney };
            TestHelpers.Should.Error(() => 
                theBeSharps.ShouldBe(new[] {apu, homer, skinner, barney}),
@"theBeSharps
    should be
[Apu, Homer, Skinner, Barney]
    but was
[Homer, Skinner, Barney]
    difference
[*Homer *, *Skinner *, *Barney *, *]");
        }

        class Person
        {
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}