﻿using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeDecoratedWith
{
    public class DerivedTypeScenario
    {
        [Fact]
        public void DerivedTypeScenarioShouldPass()
        {
            var myDecoratedThing = typeof(MyDecoratedThing);

            myDecoratedThing.ShouldBeDecoratedWith<UseCultureAttribute>();
        }
    }
}