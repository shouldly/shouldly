using System;
using Shouldly.Internals;

namespace Shouldly
{
#if Serializable
    [Serializable]
#endif
    public class ShouldAssertException : Exception
    {
        public ShouldAssertException(string? message) : base(message)
        {
        }

        public ShouldAssertException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

#if StackTrace
        private string? stackTrace;

        public override string StackTrace => StackTraceHelpers.GetStackTrace(this, ref stackTrace);
#endif
    }
}
