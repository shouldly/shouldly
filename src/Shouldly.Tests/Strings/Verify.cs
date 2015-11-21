using System;

namespace Shouldly.Tests.Strings
{
    public static class Verify
    {
        public static void ShouldFail(Action action, string errorWithSource, string errorWithoutSource)
        {
            using (ShouldlyConfiguration.DisableSourceInErrors())
            {
                var sourceDisabledEx = Should.Throw<ShouldAssertException>(action);
                sourceDisabledEx.Message.ShouldBe(errorWithoutSource, StringCompareShould.IgnoreLineEndings);
            }

            var sourceEnabledEx = Should.Throw<ShouldAssertException>(action);
            sourceEnabledEx.Message.ShouldBe(errorWithSource, StringCompareShould.IgnoreLineEndings);
        }
    }
}