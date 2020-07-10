﻿using System;
using Shouldly.Internals;

namespace Shouldly
{
    [Serializable]
#pragma warning disable 618
    public class ShouldAssertException : Exception
    {
        public ShouldAssertException(string? message) : base(message)
        {
        }

        public ShouldAssertException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        private string? stackTrace;

        public override string StackTrace => StackTraceHelpers.GetStackTrace(this, ref stackTrace);
    }
}
