using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldContainKeyAndValue
{
    public class OnlyKeyMatches : ShouldlyShouldFailureTestScenario
    {
        readonly Dictionary<MyThing, MyThing> _dictionary = new Dictionary<MyThing,MyThing>
        {
            {ThingKey, ThingValue}
        };

        private static readonly MyThing ThingKey = new MyThing();
        private static readonly MyThing ThingValue = new MyThing();

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKeyAndValue(ThingKey, new MyThing());
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_dictionary should contain key and value Shouldly.Tests.TestHelpers.MyThing but was Shouldly.Tests.TestHelpers.MyThing"; }
        }
    }
}