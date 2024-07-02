using System.Diagnostics;

namespace Shouldly.Tests;

partial class StackTraceTests
{
    public sealed class ExceptionThrower
    {
        public Type ExceptionType { get; }
        public bool InShouldlyAssembly { get; }
        public Action ThrowingAction { get; }

        public ExceptionThrower(Type exceptionType, bool inShouldlyAssembly, Action throwingAction)
        {
            ExceptionType = exceptionType ?? throw new ArgumentNullException(nameof(exceptionType));
            InShouldlyAssembly = inShouldlyAssembly;
            ThrowingAction = throwingAction ?? throw new ArgumentNullException(nameof(throwingAction));
        }

        public override string ToString() =>
            InShouldlyAssembly ? ThrowingAction.Method.Name :
                ExceptionType.Name + " thrown directly";

        public Exception Catch()
        {
            // Don’t rely on a framework for this in case of the outside chance that the framework manipulates the stack trace.
            try
            {
                ThrowingAction.Invoke();
            }
            catch (Exception ex) when (ex.GetType() == ExceptionType)
            {
                return ex;
            }

            throw new UnreachableException($"`ExceptionThrower.Catch` should always catch and return an exception. In this case, `ThrowingAction` did not throw the expected exception (`{ExceptionType}`).");
        }
    }
}