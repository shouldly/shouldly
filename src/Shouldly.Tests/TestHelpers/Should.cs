using System;

namespace Shouldly.Tests.TestHelpers
{
    [ShouldlyMethods]
    public static class Should
    {
        public static void Error(Action action, string errorMessage)
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(action).Message.ShouldContainWithoutWhitespace(errorMessage);
        }

        public static void NotError(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new ChuckedAWobbly(new ExpectedShouldlyMessage(ex.GetType()).ToString());
            }
        }
    }
}