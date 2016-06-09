using System;

namespace Shouldly
{
    public class ShouldCompleteInException : TimeoutException // Need to do this to not break existing API
    {
        public ShouldCompleteInException(string message, TimeoutException inner) : base(message, inner)
        {
        }
    }
}