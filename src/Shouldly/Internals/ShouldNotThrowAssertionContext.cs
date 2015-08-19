using System.Diagnostics;

namespace Shouldly
{
    internal class ShouldThrowAssertionContext : ShouldlyAssertionContext
    {
        public string ExceptionMessage { get; private set; }

        public bool IsAsync { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="exceptionMessage"></param>
        /// <param name="stackTrace">Only pass if asynchronous</param>
        internal ShouldThrowAssertionContext(object expected, object actual = null, string exceptionMessage = null, bool isAsync = false, StackTrace stackTrace = null) : base(expected, actual, stackTrace)
        {
            ExceptionMessage = exceptionMessage;
            IsAsync = isAsync;
        }
    }
}