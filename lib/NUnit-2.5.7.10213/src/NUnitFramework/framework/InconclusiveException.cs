// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Thrown when a test executes inconclusively.
    /// </summary>
    /// 
    [Serializable]
    public class InconclusiveException : System.Exception
    {
        /// <param name="message">The error message that explains 
        /// the reason for the exception</param>
        public InconclusiveException(string message)
            : base(message)
        { }

        /// <param name="message">The error message that explains 
        /// the reason for the exception</param>
        /// <param name="inner">The exception that caused the 
        /// current exception</param>
        public InconclusiveException(string message, Exception inner)
            :
            base(message, inner)
        { }

        /// <summary>
        /// Serialization Constructor
        /// </summary>
        protected InconclusiveException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }

    }
}
