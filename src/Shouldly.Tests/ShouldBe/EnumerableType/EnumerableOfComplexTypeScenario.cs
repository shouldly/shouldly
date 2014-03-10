using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class EnumerableOfComplexTypeScenario : ShouldlyShouldTestScenario
    {
        private readonly IEnumerable<Widget> _aEnumerable = new Widget { Name = "Joe", Enabled = true }.ToEnumerable();
        private readonly Widget[] _bArray = {new Widget {Name = "Joeyjojoshabadoo Jr", Enabled = true}};

        protected override void ShouldThrowAWobbly()
        {
            _aEnumerable.ShouldBe(_bArray);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_aEnumerable " +
                         "should be [Name(Joeyjojoshabadoo Jr) Enabled(True)] " +
                         "but was [Name(Joe) Enabled(True)] " +
                         "difference [*Name(Joe) Enabled(True)*]"; }
        }

        protected override void ShouldPass()
        {
            _aEnumerable.ShouldBe(_aEnumerable);
        }
    }
}