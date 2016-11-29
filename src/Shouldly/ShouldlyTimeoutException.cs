using System;

namespace Shouldly
{
    public class ShouldlyTimeoutException : TimeoutException
    {
        public ShouldlyTimeoutException() : base()
        {
        }

        public ShouldlyTimeoutException(string message, ShouldlyTimeoutException inner) : base(message, inner)
        {
        }
    }
}
