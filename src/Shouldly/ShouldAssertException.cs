using System;

namespace Shouldly
{
#if Serializable
    [Serializable]
#endif
#pragma warning disable 618
    public class ShouldAssertException : ChuckedAWobbly
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