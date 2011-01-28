// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.UiException.StackTraceAnalyzers;

namespace NUnit.UiException.StackTraceAnalysers
{
    /// <summary>
    /// Encapsulates a set of algorithms that try to match and locate
    /// a path value coming from a raw stack trace line.
    /// </summary>
    public class PathCompositeParser :
        IErrorParser
    {
        /// <summary>
        /// This array encapsulates a list of classes that inherit from
        /// IErrorParser. Each instance is made for handling a path from
        /// a specific file system such as: Windows or UNIX.
        /// </summary>
        private IErrorParser[] _array;

        /// <summary>
        /// Build a new instance of PathParser.
        /// </summary>
        public PathCompositeParser()
        {
            // setup this array with a couple of algorithms
            // that handle respectively Windows and Unix like paths.
            
            _array = new IErrorParser[] { 
                new WindowsPathParser(),
                new UnixPathParser()

                // add your own parser here
            };

            return;
        }

        /// <summary>
        /// Gives access to the IErrorParser instance that handles
        /// Windows like path values.
        /// </summary>
        public IErrorParser WindowsPathParser
        {
            get { return (_array[0]); }
        }

        /// <summary>
        /// Gives access to the IErrorParser instance that handles
        /// Unix like path values.
        /// </summary>
        public IErrorParser UnixPathParser
        {
            get { return (_array[1]); }
        }

        #region IErrorParser Membres

        /// <summary>
        /// Try to read from a stack trace line a path value given either
        /// under the form of a Windows path or a UNIX path. If a match occurs
        /// the method fills args.Function with the identified data.
        /// </summary>
        /// <param name="parser">The instance of StackTraceParser, this parameter
        /// cannot be null.</param>
        /// <param name="args">The instance of RawError from where read and write
        /// RawError.Input and RawError.Function properties. This parameter
        /// cannot be null.</param>
        /// <returns>True if a match occurs, false otherwise.</returns>
        public bool TryParse(StackTraceParser parser, RawError args)
        {
            foreach (IErrorParser item in _array)
                if (item.TryParse(parser, args))
                    return (true);

            return (false);
        }

        #endregion

        /// <summary>
        /// Helper method that locate the trailing ':' in a stack trace row.
        /// </summary>
        /// <returns>The index position of ':' in the string or -1 if not found.</returns>
        public static int IndexOfTrailingColon(string error, int startIndex)
        {
            int i;

            for (i = startIndex; i < error.Length; ++i)
                if (error[i] == ':')
                    return (i);

            return (-1);
        }                                
    }
}
