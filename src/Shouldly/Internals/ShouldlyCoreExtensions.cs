using System;

namespace Shouldly
{
    internal static class ShouldlyCoreExtensions
    {
        internal static void AssertAwesomely<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected, Func<string> customMessage = null)
        {
            if (customMessage == null)
                customMessage = () => null;

            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }

            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(originalExpected, originalActual, customMessage).ToString());
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

        internal static void AssertAwesomelyIgnoringOrder<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected,  Func<string> customMessage = null)
        {
            if (customMessage == null)
                customMessage = () => null;

            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }

            throw new ShouldAssertException(new ExpectedActualIgnoreOrderShouldlyMessage(originalExpected, originalActual, customMessage).ToString());
        }

        internal static void AssertAwesomely<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected, object tolerance, Func<string> customMessage = null )
        {
            if (customMessage == null)
                customMessage = () => null;

            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }

            throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(originalExpected, originalActual, tolerance, customMessage).ToString());
        }

        internal static void AssertAwesomely<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected, Case caseSensitivity, Func<string> customMessage = null)
        {
            if (customMessage == null)
                customMessage = () => null;

            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }

            throw new ShouldAssertException(new ExpectedActualWithCaseSensitivityShouldlyMessage(originalExpected, originalActual, caseSensitivity, customMessage).ToString());
        }

    }
}
