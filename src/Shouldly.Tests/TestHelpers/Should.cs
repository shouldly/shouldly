using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shouldly.Tests.TestHelpers
{
    [ShouldlyMethods]
    public static class Should
    {
        public static void Error(Action action, string errorMessage)
        {
            Shouldly.Should.Throw<ShouldAssertException>(action).Message.ShouldContainWithoutWhitespace(errorMessage);
        }

        public static void NotError(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.GetType()).ToString());
            }
        }
    }
}