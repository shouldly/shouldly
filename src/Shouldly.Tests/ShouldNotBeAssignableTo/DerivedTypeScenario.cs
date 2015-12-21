﻿using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldNotBeAssignableTo
{
    public class DerivedTypeScenario
    {
        [Fact]
        public void DerivedTypeScenarioShouldFail()
        {
            var myThing = new MyThing();
            Verify.ShouldFail(() =>
myThing.ShouldNotBeAssignableTo<MyThing>("Some additional context"),

errorWithSource:
@"myThing
    should not be assignable to
Shouldly.Tests.TestHelpers.MyThing
    but was
Shouldly.Tests.TestHelpers.MyThing (000000)",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.MyThing (000000)
    should not be assignable to
Shouldly.Tests.TestHelpers.MyThing
    but was");
        }

        [Fact]
        public void ShouldPass()
        {
            var myThing = new MyThing();
            myThing.ShouldNotBeAssignableTo<string>();
        }
    }
}