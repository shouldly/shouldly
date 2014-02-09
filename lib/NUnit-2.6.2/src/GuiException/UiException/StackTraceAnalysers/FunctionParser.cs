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
    /// This class is responsible for locating and initializing
    /// RawError.Function property with the function name as it
    /// is mentioned in the stack trace line.
    ///   To work correclty, the class makes some
    /// assumptions concerning the function format.
    ///   A function name is made of two parts: [name][args]
    /// where [name] refers to a string of characters that doesn't
    /// contain ' ' and [args] refers to a string delimited by
    /// '(' and ')'.
    /// </summary>
    public class FunctionParser :
        IErrorParser
    {
        #region IErrorParser Membres

        /// <summary>
        /// Try to match a function name by reading RawError.Input.
        /// If a match is found, the method outputs the result into
        /// RawError.Function and returns true.
        /// </summary>
        /// <param name="parser">An instance of parser, this parameter
        /// cannot be null.</param>
        /// <param name="args">An instance of RawError. This parameter
        /// cannot be null.</param>
        /// <returns>True if a match occurs, false otherwise.</returns>
        public bool TryParse(StackTraceParser parser, RawError args)
        {
            int posEndingParenthesis;
            int posOpeningParenthesis;
            int posName;
            int endName;
            string res;
            int i;

            UiExceptionHelper.CheckNotNull(parser, "parser");
            UiExceptionHelper.CheckNotNull(args, "args");

            posEndingParenthesis = args.Input.LastIndexOf(")");
            posOpeningParenthesis = args.Input.LastIndexOf("(");

            if (posEndingParenthesis == -1 || posOpeningParenthesis == -1 ||
                posOpeningParenthesis > posEndingParenthesis)
                return (false);

            endName = posOpeningParenthesis;
            for (i = posOpeningParenthesis - 1; i >= 0; i--)
            {
                if (args.Input[i] != ' ')
                    break;

                endName = i;
            }

            posName = -1;
            for (i = endName - 1; i >= 0; i--)
            {
                if (args.Input[i] == ' ')
                    break;

                posName = i;
            }

            // Added this test to allow for odd case where we would
            // otherwise include the leading "at" or "à" in name.
            if (posName == 0)
                return false;

            if (posName == -1)
                return (false);

            res = args.Input.Substring(posName, posEndingParenthesis - posName + 1);
            args.Function = res;
            
            return (true);
        }       

        #endregion
    }
}
