using System;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldlySpecsTests
    {
        [Test]
        public void ShouldlySpecs_PassedPassingSpecifications_ShouldPass()
        {
            Should.NotError(() =>
                "Math is working".Spec(() => 2.ShouldBe(2)));
            Should.NotError(() =>
                "String has correct length".Spec(() => "Five".Length.ShouldBe(4)));
        }

        [Test]
        public void ShouldlySpecs_PassedSpecThatThrowsNormalException_ShouldShowException()
        {
            Should.Error(
                () => "Feature should be implemented".Spec(() => { throw new NotImplementedException(); }),
                "Feature should be implemented: System.NotImplementedException: The method or operation is not implemented."
                );
        }
        [Test]
        public void ShouldlySpecs_PassedSpecThatShouldlyFails_ShouldShowException()
        {
            Should.Error(
                () => "True should be false".Spec(() => true.ShouldBe(false)),
                "True should be false: () => true should be False but was True"
            );
        }
    }
}