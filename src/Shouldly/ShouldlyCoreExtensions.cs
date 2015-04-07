using System;

namespace Shouldly
{
    internal static class ShouldlyCoreExtensions
    {
        internal static void AssertAwesomely<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected)
        {
            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }

            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(originalExpected, originalActual).ToString());
        }

        internal static void AssertAwesomelyWithCaseSensitivity<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected, Case caseSensitivity)
        {
            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }

            throw new ShouldAssertException(new ExpectedActualWithCaseSensitivityShouldlyMessage(originalExpected, originalActual, caseSensitivity).ToString());
        }

        internal static void AssertAwesomelyIgnoringOrder<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected)
        {
            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }

            throw new ShouldAssertException(new ExpectedActualIgnoreOrderShouldlyMessage(originalExpected, originalActual).ToString());
        }

        internal static void AssertAwesomely<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected, object tolerance)
        {
            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }

            throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(originalExpected, originalActual, tolerance).ToString());
        }
    }
}
