// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;

namespace NUnit.ConsoleRunner
{
    /// <summary>
    /// TestNameParser is used to parse the arguments to the 
    /// -run option, separating testnames at the correct point.
    /// </summary>
    public class TestNameParser
    {
        /// <summary>
        /// Parse the -run argument and return an array of argument
        /// </summary>
        /// <param name="argument">argument</param>
        /// <returns></returns>
        public static string[] Parse(string argument)
        {
            ArrayList list = new ArrayList();

            int index = 0;
            while (index < argument.Length)
            {
                string name = GetTestName(argument, ref index);
                if (name != null && name != string.Empty)
                    list.Add(name);
            }

            return (string[])list.ToArray(typeof(string));
        }

        private static string GetTestName(string argument, ref int index)
        {
            int separator = GetSeparator(argument, index);
            string result;

            if (separator >= 0)
            {
                result = argument.Substring(index, separator - index).Trim();
                index = separator + 1;
            }
            else
            {
                result = argument.Substring(index).Trim();
                index = argument.Length;
            }

            return result;
        }

        private static int GetSeparator(string argument, int index)
        {
            int nest = 0;

            while (index < argument.Length)
            {
                switch (argument[index])
                {
                    case ',':
                        if (nest == 0)
                            return index;
                        break;

                    case '"':
                        while (++index < argument.Length && argument[index] != '"')
                            ;
                        break;

                    case '(':
                    case '<':
                        nest++;
                        break;

                    case ')':
                    case '>':
                        nest--;
                        break;
                }

                index++;
            }

            return -1;
        }
    }
}
