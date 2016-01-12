using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainValueForKey
{
    public class ValueIsNull : ShouldlyShouldTestScenario
    {
        readonly Dictionary<MyThing, MyThing> _dictionary = new Dictionary<MyThing, MyThing>
        {
            {ThingKey, ThingValue}
        };

        static readonly MyThing ThingKey = new MyThing();
        static readonly MyThing ThingValue = null;

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return
                    "Dictionary \"_dictionary\" should not contain key \"Shouldly.Tests.TestHelpers.MyThing\" with value null but does " +
                    "Additional Info: " +
                    "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldNotContainValueForKey(ThingKey, new MyThing());
        }
    }
}
