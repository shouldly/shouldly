using System.Runtime.CompilerServices;

namespace Shouldly
{
    internal class ShouldThrowAssertionContext : ShouldlyAssertionContext
    {
        public string ExceptionMessage { get; private set; }

        public bool IsAsync { get; private set; }

#if PORTABLE
        /// <summary>
        /// Only pass stacktrace if asynchronous
        /// </summary>
        internal ShouldThrowAssertionContext(object expected, object actual = null, string exceptionMessage = null,
            bool isAsync = false,
            [CallerMemberName] string shouldlyMethod = null) : base(shouldlyMethod, expected, actual)
        {
            ExceptionMessage = exceptionMessage;
            IsAsync = isAsync;
        }
#else
        /// <summary>
        /// Only pass stacktrace if asynchronous
        /// </summary>
        internal ShouldThrowAssertionContext(object expected, object actual = null, string exceptionMessage = null,
            bool isAsync = false,
            System.Diagnostics.StackTrace stackTrace = null,
            [CallerMemberName] string shouldlyMethod = null) : base(shouldlyMethod, expected, actual, stackTrace)
        {
            ExceptionMessage = exceptionMessage;
            IsAsync = isAsync;
        }
#endif
    }
}