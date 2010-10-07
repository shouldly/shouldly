using System;
using System.IO;
using System.Reflection;

namespace Shouldly
{
    public static class NUnitAssemblyResolver
    {
	private static bool _wiredUp = false;

        /// <summary>
        /// Starts listening to the AppDomain.AssemblyResolve event
        /// and attempts to resolve any missing NUnit references with whatever version
        /// of the requested DLL is present in the AppDomain.BaseDirectory.
        /// </summary>
        public static void WireUp()
        {
	    if (!_wiredUp)
	    {
                AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;

		// this flag is set so that consumers don't have to worry
 		// about calling this method multiple times within the same AppDomain
	        _wiredUp = true;
	    }
        }

        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var requestedName = new AssemblyName(args.Name);

            if (requestedName.Name == "nunit.framework")
            {
                string file =
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "nunit.framework.dll");
                if (File.Exists(file))
                {
                    return Assembly.LoadFile(file);
                }
            }

            return null;
        }
    }
}