using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeOfTypes
{
    public class BasicScenarios
    {
        [Fact]
        public void ArrayTypesMatchExactly()
        {
            var arr = new object[] { new Added(), new Changed(), new Removed() };

            arr.ShouldBeOfTypes(typeof(Added), typeof(Changed), typeof(Removed));
        }

        [Fact]
        public void ArrayTypesMatchExactlyWithCustomContext()
        {
            var arr = new object[] { new Added(), new Changed(), new Removed() };

            arr.ShouldBeOfTypes(new[] { typeof(Added), typeof(Changed), typeof(Removed)}, "additional context");
        }

        [Fact]
        public void ArrayTypesMatchExactlyWithLambdaContext()
        {
            var arr = new object[] { new Added(), new Changed(), new Removed() };

            arr.ShouldBeOfTypes(new[] { typeof(Added), typeof(Changed), typeof(Removed)}, () => "additional context");
        }

        [Fact]
        public void FailsIfTypesDontMatchExactly()
        {
            Verify.ShouldFail(() =>
new object[] { new Added(), new Changed() }.ShouldBeOfTypes(new[] { typeof(Added), typeof(object) }, "Some additional context"),

errorWithSource:
@"new object[] { new Added(), new Changed() }
    should be
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, System.Object]
    but was
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Changed]
    difference
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, *Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Changed*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Changed]
    should be
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, System.Object]
    but was not
    difference
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, *Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Changed*]

Additional Info:
    Some additional context");
        }


        [Fact]
        public void FailsIfAcualAndExpectedAreDifferentLengths()
        {
            Verify.ShouldFail(() =>
new object[] { new Added(), new Changed() }.ShouldBeOfTypes(new[] { typeof(Added) }, "Some additional context"),

errorWithSource:
@"new object[] { new Added(), new Changed() }
    should be
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added]
    but was
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Changed]
    difference
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, *Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Changed*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Changed]
    should be
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added]
    but was not
    difference
[Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Added, *Shouldly.Tests.ShouldBeOfTypes.BasicScenarios+Changed*]

Additional Info:
    Some additional context");
        }

        private class Added {}
        private class Changed {}
        private class Removed {}
    }
}
