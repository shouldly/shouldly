using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;
using Xunit;

namespace Shouldly.Tests.ConventionTests
{
    public class ShouldlyConventions
    {
        private readonly Types _shouldlyMethodClasses;

        public ShouldlyConventions()
        {
            _shouldlyMethodClasses = Types.InAssemblyOf<ShouldAssertException>(
                "Shouldly extension classes",
                t => t.HasAttribute("Shouldly.ShouldlyMethodsAttribute"));
        }

        [Fact]
        public void ShouldHaveCustomMessageOverloads()
        {
            Convention.Is(new ShouldlyMethodsShouldHaveCustomMessageOverload(), _shouldlyMethodClasses);
        }

        [Fact]
        public void ShouldThrowMethodsShouldHaveExtensions()
        {
            Convention.Is(new ShouldThrowMatchesExtensionsConvention(), _shouldlyMethodClasses);
        }
    }
}