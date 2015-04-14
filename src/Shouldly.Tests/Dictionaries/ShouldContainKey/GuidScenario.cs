using System;
using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldContainKey
{
    public class GuidScenario : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<Guid, Guid> _dictionary = new Dictionary<Guid, Guid>
        {
            { GuidKey, Guid.NewGuid()}
        };

        private static readonly Guid GuidKey = Guid.NewGuid();
        private readonly Guid _missingGuid = new Guid("5250646b-4c46-4b0e-86d8-e1421f2a0ea2");

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKey(_missingGuid, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return
                    "Dictionary \"_dictionary\" should contain key \"5250646b-4c46-4b0e-86d8-e1421f2a0ea2\" but does not " +
                    "Additional Info: " +
                    "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKey(GuidKey);
        }
    }
}