using System;
using Shouldly.Internals;

namespace Shouldly
{
    public class ShouldlyTimeoutException : TimeoutException
    {
        public ShouldlyTimeoutException() : base()
        {
        }

        public ShouldlyTimeoutException(string? message, ShouldlyTimeoutException? inner) : base(message, inner)
        {
        }

#if StackTrace
        private string? stackTrace;

        public override string StackTrace => StackTraceHelpers.GetStackTrace(this, ref stackTrace);
#endif
    }
}
