// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Reflection;

namespace NUnit.Core
{
    public class AssemblyHelper
    {
        #region GetAssemblyPath

        public static string GetAssemblyPath(Type type)
        {
            return GetAssemblyPath(type.Assembly);
        }

        // There are two implementations of GetAssemblyPath,
        // one for .NET 1.1 and one for later frameworks.
        public static string GetAssemblyPath(Assembly assembly)
        {
#if CLR_2_0 || CLR_4_0
            string codeBase = assembly.EscapedCodeBase;
            
            if (IsFileUri(codeBase))
                return GetAssemblyPathFromEscapedCodeBase(codeBase);
#else
            string codeBase = assembly.CodeBase;

            if (IsFileUri(codeBase))
                return GetAssemblyPathFromCodeBase(codeBase);
#endif
            
            return assembly.Location;
        }

        #endregion

        #region

#if CLR_2_0 || CLR_4_0
        // Public for testing purposes
        public static string GetAssemblyPathFromEscapedCodeBase(string escapedCodeBase)
        {
            return new Uri(escapedCodeBase).LocalPath;
        }
#else
        public static string GetAssemblyPathFromCodeBase(string codeBase)
        {
            // Skip over the file:// part
            int start = Uri.UriSchemeFile.Length + Uri.SchemeDelimiter.Length;

            bool isWindows = System.IO.Path.DirectorySeparatorChar == '\\';

            if (codeBase[start] == '/') // third slash means a local path
            {
                // Handle Windows Drive specifications
                if (isWindows && codeBase[start + 2] == ':')
                    ++start;  
                // else leave the last slash so path is absolute  
            }
            else // It's either a Windows Drive spec or a share
            {
                if (!isWindows || codeBase[start + 1] != ':')
                    start -= 2; // Back up to include two slashes
            }

            return codeBase.Substring(start);
        }
#endif

        #endregion

        #region GetDirectoryName
        public static string GetDirectoryName( Assembly assembly )
        {
            return System.IO.Path.GetDirectoryName(GetAssemblyPath(assembly));
        }
        #endregion

        #region Helper Methods

        private static bool IsFileUri(string uri)
        {
            return uri.ToLower().StartsWith(Uri.UriSchemeFile);
        }

        #endregion
    }
}
