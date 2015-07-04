using NUnit.Framework;
using Shouldly;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace ShouldlyConvention.Tests
{
    [TestFixture]
    public class ShouldlyConventions
    {
        private readonly Types _shouldlyMethodClasses;

        public ShouldlyConventions()
        {
            _shouldlyMethodClasses = Types.InAssemblyOf<ShouldAssertException>(
                "Shouldly extension classes",
                t => t.HasAttribute("Shouldly.ShouldlyMethodsAttribute"));
        }

        [Test]
        public void ShouldHaveCustomMessageOverloads()
        {
            Convention.Is(new ShouldlyMethodsShouldHaveCustomMessageOverload(), _shouldlyMethodClasses);
        }

        [Test]
        public void ShouldThrowMethodsShouldHaveExtensions()
        {
            Convention.Is(new ShouldThrowMatchesExtensionsConvention(), _shouldlyMethodClasses);
        }
    }
}