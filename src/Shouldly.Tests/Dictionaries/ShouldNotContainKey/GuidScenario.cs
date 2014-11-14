using System;
using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainKey
{
    public class GuidScenario : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<Guid, Guid> _dictionary = new Dictionary<Guid, Guid>
        {
            { GuidKey, Guid.NewGuid()}
        };

        private static readonly Guid GuidKey = new Guid("89bdbe3d-3436-4749-bcb7-84264394026c");

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainKey(GuidKey);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Dictionary \"_dictionary\" should not contain key \"89bdbe3d-3436-4749-bcb7-84264394026c\" but does"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldNotContainKey(Guid.NewGuid());
        }
    }
}