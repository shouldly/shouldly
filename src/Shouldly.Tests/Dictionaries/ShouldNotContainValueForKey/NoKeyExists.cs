using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainValueForKey
{
    public class NoKeyExists : ShouldlyShouldTestScenario
    {
        readonly Dictionary<MyThing, MyThing> _dictionary = new Dictionary<MyThing,MyThing>
        {
            {ThingKey, ThingValue}
        };

        static readonly MyThing ThingKey = new MyThing();
        static readonly MyThing ThingValue = new MyThing();

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainValueForKey(new MyThing(), ThingValue, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return
                    "Dictionary \"_dictionary\" should not contain key \"Shouldly.Tests.TestHelpers.MyThing\" with value \"Shouldly.Tests.TestHelpers.MyThing\" but the key does not exist " +
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