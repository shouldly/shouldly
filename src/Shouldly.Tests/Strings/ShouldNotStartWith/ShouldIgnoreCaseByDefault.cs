﻿using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class ShouldIgnoreCaseByDefault
    {
        [Fact]
        public void ShouldIgnoreCaseByDefaultShouldFail()
        {
            Verify.ShouldFail(() =>
"Cheese".ShouldNotStartWith("cH", "Some additional context"),

errorWithSource:
@"""Cheese""
    should not start with
""cH""
    but was

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Cheese""
    should not start with
""cH""
    but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotStartWith("Ce");
        }
    }
}