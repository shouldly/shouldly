// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System.IO;

namespace NUnit.Core
{
    /// <summary>
    /// Abstract base for classes that capture text output
    /// and redirect it to a TextWriter.
    /// </summary>
    public abstract class TextCapture
    {
        #region Abstract Members

        /// <summary>
        /// Gets or sets the TextWriter to which text is redirected
        /// when captured. The value may only be changed when the
        /// logging threshold is set to "Off"
        /// </summary>
        public abstract TextWriter Writer 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the capture threshold value, which represents
        /// the degree of verbosity of the output text stream. Derived
        /// classes will need to translate the LoggingThreshold into
        /// the appropriate levels supported by the logging software.
        /// </summary>
        public abstract LoggingThreshold Threshold
        {
            get; set;
        }

        #endregion
    }

}
