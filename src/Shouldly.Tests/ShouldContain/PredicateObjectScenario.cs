using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class PredicateObjectScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            new[] {a, b, c}.ShouldContain(o => o.GetType().FullName.Equals(""), "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"
    new[] { a, b, c }
        should contain an element satisfying the condition
    o.GetType().FullName.Equals("""")
        but does not
    Additional Info:
    Some additional context"; }
        }

        protected override void ShouldPass()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            new[] {a, b, c}.ShouldContain(o => o.GetType().FullName.Equals("System.Object"),
                () => "Some additional context");
        }
    }
}