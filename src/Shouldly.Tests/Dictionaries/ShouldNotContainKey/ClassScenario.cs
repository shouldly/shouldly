using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainKey
{
    public class ClassScenario : ShouldlyShouldTestScenario
    {
        readonly Dictionary<MyThing, MyThing> _dictionary = new Dictionary<MyThing, MyThing>
        {
            {ThingKey, new MyThing()}
        };

        private static readonly MyThing ThingKey = new MyThing();

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainKey(ThingKey);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_dictionary should not contain key Shouldly.Tests.TestHelpers.MyThing but does"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldNotContainKey(new MyThing());
        }
    }
}