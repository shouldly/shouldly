using System;

namespace Shouldly.Tests
{
    public static class Should
    {
        public static void Error(Action action, string errorMessage)
        {
            Shouldly.Should.Throw<Shouldly.ChuckedAWobbly>(action).ShouldBeCloseTo(errorMessage);
        }
    }
}