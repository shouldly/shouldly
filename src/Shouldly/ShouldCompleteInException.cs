namespace Shouldly
{
    public class ShouldCompleteInException : ShouldlyTimeoutException // Need to do this to not break existing API
    {
        public ShouldCompleteInException(string message, ShouldlyTimeoutException inner) : base(message, inner)
        {
        }
    }
}