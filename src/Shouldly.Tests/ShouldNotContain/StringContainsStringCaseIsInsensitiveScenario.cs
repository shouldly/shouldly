﻿using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringCaseIsInsensitiveScenario
    {

        [Fact]
        public void StringContainsStringCaseIsInsensitiveScenarioShouldFail()
        {
            const string target = "Shouldly is legendary";
            Verify.ShouldFail(() =>
target.ShouldNotContain("LEGENDARY", Case.Insensitive),

errorWithSource:
@"target
    should not contain (case insensitive comparison)
""LEGENDARY""
    but was actually
""Shouldly is legendary""",

errorWithoutSource:
@"""Shouldly is legendary""
    should not contain (case insensitive comparison)
""LEGENDARY""
    but did");
        }

        [Fact]
        public void ShouldPass()
        {
            "Shouldly is legendary".ShouldNotContain("LEGEND-wait for it-ary", Case.Insensitive);
        }
    }
}