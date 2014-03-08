using System.Collections.Generic;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class ActualIsNullScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            IEnumerable<int> something = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            something.ShouldBe(new[] { 1, 2, 3 });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "something should be [1, 2, 3] but was null"; }
        }
    }
}