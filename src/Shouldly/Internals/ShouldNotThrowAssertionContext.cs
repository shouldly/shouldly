namespace Shouldly
{
    internal class ShouldNotThrowAssertionContext : ShouldlyAssertionContext
    {
        public string ExceptionMessage { get; private set; }

        internal ShouldNotThrowAssertionContext(object expected, object actual = null, string exceptionMessage = null) : base(expected, actual)
        {
            ExceptionMessage = exceptionMessage;
        }
    }
}