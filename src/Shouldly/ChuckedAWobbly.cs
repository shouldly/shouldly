﻿using System;
using System.Threading;

namespace Shouldly
{
#if Serializable
    [Serializable]
#endif
    [Obsolete("This class is only kept here for backwards compatibility. Please use ShouldAssertException instead.")]
    public class ChuckedAWobbly : Exception
    {
        public ChuckedAWobbly(string message) : base(message)
        {
        }

        public ChuckedAWobbly(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
