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
    /// This class is responsible for extracting a Windows like path value
    /// from a line of the given stack trace. This class bases its work
    /// on the following assumptions:
    /// - paths are supposed to be absolute,
    /// - paths are supposed to be made of two parts: [drive][path]
    /// Where [drive] refers to a sequence like: "C:\\"
    /// and [path] a non empty string of characters that extends to the
    /// trailing ':' (as given in stack trace).
    /// </summary>
    public class WindowsPathParser :
        IErrorParser
    {
        #region IErrorParser Membres

        /// <summary>
        /// Locates and fills RawError.Path property with the first
        /// Windows path values found from RawError.Input property.
        /// </summary>
        /// <param name="parser">The stack trace parser. This parameter
        /// must not be null.</param>
        /// <param name="args">The RawError from which retrieving and
        /// filling Input and Path properties. This parameter cannot not
        /// be null.</param>
        /// <returns>True if a match occured, false otherwise.</returns>
        public bool TryParse(StackTraceParser parser, RawError args)
        {
            int posDriveLetter;
            int posTrailingColon;
            string path;

            UiExceptionHelper.CheckNotNull(parser, "parser");
            UiExceptionHelper.CheckNotNull(args, "args");

            posDriveLetter = lookForDriveLetter(args.Input, 0);
            if (posDriveLetter == -1)
                return (false);

            posTrailingColon = PathCompositeParser.IndexOfTrailingColon(args.Input, posDriveLetter + 3);
            if (posTrailingColon == -1)
                return (false);

            path = args.Input.Substring(posDriveLetter, posTrailingColon - posDriveLetter);
            path = path.Trim();

            if (path.Length <= 3)
                return (false);

            args.Path = path;

            return (true);
        }

        #endregion

        private int lookForDriveLetter(string error, int startIndex)
        {
            int i;

            for (i = startIndex; i < error.Length - 2; ++i)
            {
                if (((error[i] >= 'a' && error[i] <= 'z') ||
                    (error[i] >= 'A' && error[i] <= 'Z')) &&
                    error[i + 1] == ':' &&
                    error[i + 2] == '\\')
                    return (i);
            }

            return (-1);
        }   
    }
}
