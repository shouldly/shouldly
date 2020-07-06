using System;
using Shouldly.Internals;

namespace Shouldly
{
    public class ShouldlyTimeoutException : TimeoutException
    {
        public ShouldlyTimeoutException()
        {
        }

        public ShouldlyTimeoutException(string? message, ShouldlyTimeoutException? inner) : base(message, inner)
        {
        }

        private string? stackTrace;

        public override string StackTrace => StackTraceHelpers.GetStackTrace(this, ref stackTrace);
    }
}
