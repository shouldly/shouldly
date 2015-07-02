using System;

namespace Shouldly
{
    [Serializable]
    #pragma warning disable 618
    internal class ShouldAssertException : ChuckedAWobbly
    #pragma warning restore 618
    {
        public ShouldAssertException(string message) : base(message)
        {
        }

        public ShouldAssertException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}