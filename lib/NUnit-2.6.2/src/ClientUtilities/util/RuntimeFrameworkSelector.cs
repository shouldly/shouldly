// ****************************************************************
// Copyright 2010, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Core;

namespace NUnit.Util
{
    public class RuntimeFrameworkSelector : IRuntimeFrameworkSelector
    {
        static Logger log = InternalTrace.GetLogger(typeof(RuntimeFrameworkSelector));

        /// <summary>
        /// Selects a target runtime framework for a TestPackage based on
        /// the settings in the package and the assemblies themselves.
        /// The package RuntimeFramework setting may be updated as a 
        /// result and the selected runtime is returned.
        /// </summary>
        /// <param name="package">A TestPackage</param>
        /// <returns>The selected RuntimeFramework</returns>
        public RuntimeFramework SelectRuntimeFramework(TestPackage package)
        {
            RuntimeFramework currentFramework = RuntimeFramework.CurrentFramework;
            RuntimeFramework requestedFramework = package.Settings["RuntimeFramework"] as RuntimeFramework;

            log.Debug("Current framework is {0}", currentFramework);
            if (requestedFramework == null)
                log.Debug("No specific framework requested");
            else
                log.Debug("Requested framework is {0}", requestedFramework);

            RuntimeType targetRuntime = requestedFramework == null
                ? RuntimeType.Any 
                : requestedFramework.Runtime;
            Version targetVersion = requestedFramework == null
                ? RuntimeFramework.DefaultVersion
                : requestedFramework.FrameworkVersion;

            if (targetRuntime == RuntimeType.Any)
                targetRuntime = currentFramework.Runtime;

            if (targetVersion == RuntimeFramework.DefaultVersion)
            {
                if (Services.UserSettings.GetSetting("Options.TestLoader.RuntimeSelectionEnabled", true))
                    foreach (string assembly in package.Assemblies)
                    {
                        using (AssemblyReader reader = new AssemblyReader(assembly))
                        {
                            string vString = reader.ImageRuntimeVersion;
                            if (vString.Length > 1) // Make sure it's a valid dot net assembly
                            {
                                Version v = new Version(vString.Substring(1));
                                log.Debug("Assembly {0} uses version {1}", assembly, v);
                                if (v > targetVersion) targetVersion = v;
                            }
                        }
                    }
                else
                    targetVersion = RuntimeFramework.CurrentFramework.ClrVersion;

                RuntimeFramework checkFramework = new RuntimeFramework(targetRuntime, targetVersion);
                if (!checkFramework.IsAvailable || !Services.TestAgency.IsRuntimeVersionSupported(targetVersion))
                {
                    log.Debug("Preferred version {0} is not installed or this NUnit installation does not support it", targetVersion);
                    if (targetVersion < currentFramework.FrameworkVersion)
                        targetVersion = currentFramework.FrameworkVersion;
                }
            }

            RuntimeFramework targetFramework = new RuntimeFramework(targetRuntime, targetVersion);
            package.Settings["RuntimeFramework"] = targetFramework;

            log.Debug("Test will use {0} framework", targetFramework);

            return targetFramework;
        }
    }
}
