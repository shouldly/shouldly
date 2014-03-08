using System.Collections.Generic;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class CannotIgnoreOrderWhenItemsAreNotComparable : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new List<object> {new object(), new object()}.ShouldBe(new[] {new object(), new object()}, ignoreOrder: true);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "At least one object must implement IComparable."; }
        }
    }
}