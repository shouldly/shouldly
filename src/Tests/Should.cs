using System;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    public static class Should
    {
        public static void Error(Action action, string errorMessage)
        {
            Shouldly.Should.Throw<AssertionException>(action).ShouldBeCloseTo(errorMessage);
        }
    }
}