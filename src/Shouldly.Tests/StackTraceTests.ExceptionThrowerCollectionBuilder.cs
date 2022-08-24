namespace Shouldly.Tests;

partial class StackTraceTests
{
    private sealed class ExceptionThrowerCollectionBuilder
    {
        private readonly List<ExceptionThrower> exceptionThrowers = new();

        /// <param name="throwDirectly">Required to cover the code path where the stack trace is not trimmed.</param>
        /// <param name="throwInShouldlyAssembly">Required to cover the code path where the stack trace is trimmed.</param>
        public ExceptionThrowerCollectionBuilder Add<TException>(Action throwDirectly, params Action[] throwInShouldlyAssembly)
            where TException : Exception
        {
            exceptionThrowers.Add(new ExceptionThrower(typeof(TException), false, throwDirectly));

            if (throwInShouldlyAssembly.Length == 0)
                throw new ArgumentException("If an action cannot be provided which throws this type of exception in the Shouldly assembly, specify the reason in the other overload.", nameof(throwInShouldlyAssembly));

            foreach (var action in throwInShouldlyAssembly)
                exceptionThrowers.Add(new ExceptionThrower(typeof(TException), true, action));

            return this;
        }

        /// <param name="throwDirectly">Required to cover the code path where the stack trace is not trimmed.</param>
        public ExceptionThrowerCollectionBuilder Add<TException>(Action throwDirectly)
            where TException : Exception
        {
            exceptionThrowers.Add(new ExceptionThrower(typeof(TException), false, throwDirectly));
            return this;
        }

        public IReadOnlyCollection<ExceptionThrower> Build()
        {
            var missingExceptionTypes = new List<string>();

            foreach (var type in typeof(ShouldAssertException).Assembly.GetExportedTypes())
            {
                if (type.IsSubclassOf(typeof(Exception)))
                {
                    if (exceptionThrowers.All(t => t.ExceptionType != type))
                    {
                        missingExceptionTypes.Add(type.Name);
                    }
                }
            }

            if (missingExceptionTypes.Any())
            {
                throw new InvalidOperationException("All exception types must be covered by these tests. Missing: " + string.Join(", ", missingExceptionTypes));
            }

            return exceptionThrowers;
        }
    }
}