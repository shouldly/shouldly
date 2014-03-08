using System.Collections.Generic;

namespace Shouldly.Tests.Dictionaries.ShouldContainKeyAndValue
{
    public class ClassScenario : ShouldlyShouldTestScenario
    {
        readonly Dictionary<MyThing, MyThing> _dictionary = new Dictionary<MyThing,MyThing>
        {
            {ThingKey, ThingValue}
        };

        private static readonly MyThing ThingKey = new MyThing();
        private static readonly MyThing ThingValue = new MyThing();

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKeyAndValue(new MyThing(), new MyThing());
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_dictionary should contain key and value Shouldly.Tests.MyThing but does not"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKeyAndValue(ThingKey, ThingValue);
        }
    }
}