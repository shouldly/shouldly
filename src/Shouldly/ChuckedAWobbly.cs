using System;

namespace Shouldly
{
    [Obsolete("This class is only kept here for backwards compatibility. Please use ShouldAssertException instead.")]
    [Serializable]
    internal class ChuckedAWobbly : Exception
    {
        public ChuckedAWobbly(string message) : base(message)
        {
        }

        public ChuckedAWobbly(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
