using NUnit.Framework;
using Shouldly;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace ShouldlyConvention.Tests
{
    [TestFixture]
    public class ShouldlyConventions
    {
        [Test]
        public void ShouldHaveCustomMessageOverloads()
        {
            var shouldlyMethodClasses = Types.InAssemblyOf<ShouldAssertException>("Shouldly extension classes", t => t.HasAttribute("Shouldly.ShouldlyMethodsAttribute"));
            Convention.Is(new ShouldlyMethodsShouldHaveCustomMessageOverload(), shouldlyMethodClasses);
        }
    }
}