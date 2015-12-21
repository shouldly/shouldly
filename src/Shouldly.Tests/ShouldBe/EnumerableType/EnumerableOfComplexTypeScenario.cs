﻿using System.Collections.Generic;
using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class EnumerableOfComplexTypeScenario
    {
        readonly IEnumerable<Widget> _aEnumerable = new Widget { Name = "Joe", Enabled = true }.ToEnumerable();
        readonly Widget[] _bArray = { new Widget { Name = "Joeyjojoshabadoo Jr", Enabled = true } };

        [Fact]
        public void EnumerableOfComplexTypeScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
_aEnumerable.ShouldBe(_bArray, "Some additional context"),

errorWithSource:
@"_aEnumerable
    should be
[Name(Joeyjojoshabadoo Jr) Enabled(True)]
    but was
[Name(Joe) Enabled(True)]
    difference
[*Name(Joe) Enabled(True)*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[Name(Joe) Enabled(True)]
    should be
[Name(Joeyjojoshabadoo Jr) Enabled(True)]
    but was not
    difference
[*Name(Joe) Enabled(True)*]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            _aEnumerable.ShouldBe(_aEnumerable);
        }
    }
}