﻿using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Shouldly
{
    internal static class ShouldlyCoreExtensions
    {
        internal static void AssertAwesomely<T>(
            this T actual, Func<T, bool> specifiedConstraint,
            object originalActual, object originalExpected,
            [InstantHandle] Func<string> customMessage = null,
            [CallerMemberName] string shouldlyMethod = null)
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

            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(originalExpected, originalActual, customMessage, shouldlyMethod).ToString());
        }

        internal static void AssertAwesomelyWithCaseSensitivity<T>(
            this T actual, Func<T, bool> specifiedConstraint, 
            object originalActual, object originalExpected, 
            Case caseSensitivity, Func<string> customMessage = null,
            [CallerMemberName] string shouldlyMethod = null)
        {
            try
            {
                if (specifiedConstraint(actual)) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }

            var message = new ExpectedActualWithCaseSensitivityShouldlyMessage(originalExpected, originalActual, caseSensitivity, customMessage, shouldlyMethod);
            throw new ShouldAssertException(message.ToString());
        }

        internal static void AssertAwesomelyIgnoringOrder<T>(
            this T actual, Func<T, bool> specifiedConstraint,
            object originalActual, object originalExpected,
            Func<string> customMessage = null,
            [CallerMemberName] string shouldlyMethod = null)
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

            throw new ShouldAssertException(new ExpectedActualIgnoreOrderShouldlyMessage(originalExpected, originalActual, customMessage, shouldlyMethod).ToString());
        }

        internal static void AssertAwesomely<T>(
            this T actual, Func<T, bool> specifiedConstraint,
            object originalActual, object originalExpected, object tolerance,
            [InstantHandle] Func<string> customMessage = null,
            [CallerMemberName] string shouldlyMethod = null)
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

            throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(originalExpected, originalActual, tolerance, customMessage, shouldlyMethod).ToString());
        }

        internal static void AssertAwesomely<T>(
            this T actual, Func<T, bool> specifiedConstraint,
            object originalActual, object originalExpected, Case caseSensitivity,
            [InstantHandle] Func<string> customMessage = null,
            [CallerMemberName] string shouldlyMethod = null)
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

            throw new ShouldAssertException(new ExpectedActualWithCaseSensitivityShouldlyMessage(originalExpected, originalActual, caseSensitivity, customMessage, shouldlyMethod).ToString());
        }
    }
}
