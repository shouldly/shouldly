namespace Shouldly
{
    internal class ShouldThrowAssertionContext : ShouldlyAssertionContext
    {
        public string ExceptionMessage { get; private set; }

        internal ShouldThrowAssertionContext(object expected, object actual = null, string exceptionMessage = null) : base(expected, actual)
        {
            ExceptionMessage = exceptionMessage;
        }
    }
}