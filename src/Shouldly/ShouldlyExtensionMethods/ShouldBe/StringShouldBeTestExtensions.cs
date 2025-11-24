using System.ComponentModel;
using Shouldly.Internals.AssertionFactories;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeStringTestExtensions
{
    extension([NotNullIfNotNull("expected")] string? actual)
    {
        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBe([NotNullIfNotNull(nameof(actual))] string? expected,
            string? customMessage = null)
        {
            // ReSharper disable once IntroduceOptionalParameters.Global
            ShouldBe(actual, expected, customMessage, 0);
        }

        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBe([NotNullIfNotNull(nameof(actual))] string? expected,
            StringCompareShould options)
        {
            ShouldBe(actual, expected, null, options);
        }

        /// <summary>
        /// Perform a string comparison with sensitivity options and custom message
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBe([NotNullIfNotNull(nameof(actual))] string? expected,
            string? customMessage,
            StringCompareShould options)
        {
            var assertion = StringShouldBeAssertionFactory.Create(expected, actual, options);
            ExecuteAssertion(assertion, customMessage);
        }
    }

    private static void ExecuteAssertion(Internals.Assertions.IAssertion assertion, string? customMessage)
    {
        try
        {
            if (assertion.IsSatisfied()) return;
        }
        catch (ArgumentException ex)
        {
            throw new ShouldAssertException(ex.Message, ex);
        }

        throw new ShouldAssertException(assertion.GenerateMessage(customMessage));
    }
}