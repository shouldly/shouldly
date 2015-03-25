using System;

namespace Shouldly
{
    internal static class ShouldlyCoreExtensions
    {
        internal static void AssertAwesomely<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected, Func<string> customMessage = null)
        {
            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                var evaledCustomMessage = EvaluateCustomMessage(customMessage);
                throw new ShouldAssertException(evaledCustomMessage + ex.Message, ex);
            }

            var evaledCustomMessage2 = EvaluateCustomMessage(customMessage);
            throw new ShouldAssertException(evaledCustomMessage2 + new ExpectedActualShouldlyMessage(originalExpected, originalActual).ToString());
        }

        internal static void AssertAwesomelyIgnoringOrder<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected, Func<string> customMessage = null)
        {
            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                var evaledCustomMessage = EvaluateCustomMessage(customMessage);
                throw new ShouldAssertException(evaledCustomMessage + ex.Message, ex);
            }

            var evaledCustomMessage2 = EvaluateCustomMessage(customMessage);
            throw new ShouldAssertException(evaledCustomMessage2 + new ExpectedActualShouldlyMessage(originalExpected, originalActual).ToString());
        }

        internal static void AssertAwesomely<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected, object tolerance, Func<string> customMessage = null)
        {
            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                var evaledCustomMessage = EvaluateCustomMessage(customMessage);
                throw new ShouldAssertException(evaledCustomMessage + ex.Message, ex);
            }

            var evaledCustomMessage2 = EvaluateCustomMessage(customMessage);
            throw new ShouldAssertException(evaledCustomMessage2 + new ExpectedActualToleranceShouldlyMessage(originalExpected, originalActual, tolerance).ToString());
        }

        private static string EvaluateCustomMessage(Func<string> customMessage)
        {
            var evaledCustomMessage = string.Empty;

            if (customMessage != null)
            {
                try
                {
                    evaledCustomMessage = customMessage() + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    evaledCustomMessage = ex.ToString() + Environment.NewLine;
                }
            }

            return evaledCustomMessage;
        }
    }
}
