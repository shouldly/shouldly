using System;

namespace Shouldly
{
    [Serializable]
    internal class ShouldAssertException : ChuckedAWobbly
    {
        public ShouldAssertException(string message) : base(message)
        {
        }

        public ShouldAssertException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}