using System;
using Shouldly;

namespace Tests
{
    public static class Should
    {
        public static void Error(Action action, string errorMessage)
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(action).ShouldBeCloseTo(errorMessage);
        }
    }
}