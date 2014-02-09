// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Reflection;
using System.Collections;
using Microsoft.Win32;

namespace NUnit.Core
{
	/// <summary>
	/// Enumeration identifying a common language 
	/// runtime implementation.
	/// </summary>
	public enum RuntimeType
	{
        /// <summary>Any supported runtime framework</summary>
        Any,
		/// <summary>Microsoft .NET Framework</summary>
		Net,
		/// <summary>Microsoft .NET Compact Framework</summary>
		NetCF,
		/// <summary>Microsoft Shared Source CLI</summary>
		SSCLI,
		/// <summary>Mono</summary>
		Mono
	}

	/// <summary>
	/// RuntimeFramework represents a particular version
	/// of a common language runtime implementation.
	/// </summary>
    [Serializable]
	public sealed class RuntimeFramework
    {
        #region Static and Instance Fields

        /// <summary>
        /// DefaultVersion is an empty Version, used to indicate that
        /// NUnit should select the CLR version to use for the test.
        /// </summary>
        public static readonly Version DefaultVersion = new Version();

        private static RuntimeFramework currentFramework;
        private static RuntimeFramework[] availableFrameworks;
        private static Version[] knownVersions = new Version[] {
            new Version(1, 0, 3705),
            new Version(1, 1, 4322),
            new Version(2, 0, 50727),
            new Version(4, 0, 30319)
        };
      
        private RuntimeType runtime;
        private Version frameworkVersion;
        private Version clrVersion;
		private string displayName;
        #endregion

        #region Constructor

        /// <summary>
		/// Construct from a runtime type and version
		/// </summary>
		/// <param name="runtime">The runtime type of the framework</param>
		/// <param name="version">The version of the framework</param>
		public RuntimeFramework( RuntimeType runtime, Version version)
		{
            this.runtime = runtime;

            if (version.Build < 0)
                InitFromFrameworkVersion(version);
            else
                InitFromClrVersion(version);


            if (version.Major == 3)
                this.clrVersion = new Version(2, 0, 50727);
			if(version.Major == 4)
				this.clrVersion = new Version(4, 0, 30319);
            //else if (runtime == RuntimeType.Mono && version.Major == 1)
            //{
            //    if (version.Minor == 0)
            //        this.clrVersion = new Version(1, 1);
            //    else if (version.Minor == 1)
            //        this.frameworkVersion = new Version(1, 0);
            //}

            this.displayName = GetDefaultDisplayName(runtime, version);
        }

        private void InitFromFrameworkVersion(Version version)
        {
            this.frameworkVersion = this.clrVersion = version;
            foreach (Version v in knownVersions)
                if (v.Major == version.Major && v.Minor == version.Minor)
                {
                    this.clrVersion = v;
                    break;
                }

            if (this.runtime == RuntimeType.Mono && version.Major == 1)
            {
                this.frameworkVersion = new Version(1, 0);
                this.clrVersion = new Version(1, 1, 4322);
            }
        }

        private void InitFromClrVersion(Version version)
        {
            this.frameworkVersion = new Version(version.Major, version.Minor);
            this.clrVersion = version;
            if (runtime == RuntimeType.Mono && version.Major == 1)
                this.frameworkVersion = new Version(1, 0);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Static method to return a RuntimeFramework object
        /// for the framework that is currently in use.
        /// </summary>
        public static RuntimeFramework CurrentFramework
        {
            get
            {
                if (currentFramework == null)
                {
                    Type monoRuntimeType = Type.GetType("Mono.Runtime", false);
                    bool isMono = monoRuntimeType != null;

                    RuntimeType runtime = isMono ? RuntimeType.Mono : RuntimeType.Net;

                    int major = Environment.Version.Major;
                    int minor = Environment.Version.Minor;

                    if (isMono)
					{
						switch (major)
						{
						case 1:
                        	minor = 0;
							break;
						case 2:
							major = 3;
							minor = 5;
							break;
						}
					}
					else /* It's windows */
					if (major == 2)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework");
                        if (key != null)
                        {
                            string installRoot = key.GetValue("InstallRoot") as string;
                            if (installRoot != null)
                            {
                                if (Directory.Exists(Path.Combine(installRoot, "v3.5")))
                                {
                                    major = 3;
                                    minor = 5;
                                }
                                else if (Directory.Exists(Path.Combine(installRoot, "v3.0")))
                                {
                                    major = 3;
                                    minor = 0;
                                }
                            }
                        }
                    }
                    else if (major == 4)
                    {
                        minor = Type.GetType("System.Reflection.ReflectionContext", false) != null
                            ? 5
                            : 0;
                    }

                    currentFramework = new RuntimeFramework(runtime, new Version(major, minor));
                    currentFramework.clrVersion = Environment.Version;

                    if (isMono)
                    {
                        MethodInfo getDisplayNameMethod = monoRuntimeType.GetMethod(
                            "GetDisplayName", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.ExactBinding);
                        if (getDisplayNameMethod != null)
                            currentFramework.displayName += string.Format(" ( {0} )",
                                (string)getDisplayNameMethod.Invoke(null, new object[0]));
                    }
                }

                return currentFramework;
            }
        }

        /// <summary>
        /// Gets an array of all available frameworks
        /// </summary>
        public static RuntimeFramework[] AvailableFrameworks
        {
            get
            {
                if (availableFrameworks == null)
                {
                    FrameworkCollection frameworks = new FrameworkCollection();

                    AppendDotNetFrameworks(frameworks);
                    AppendDefaultMonoFramework(frameworks);
                    // NYI
                    //AppendMonoFrameworks(frameworks);

                    availableFrameworks = frameworks.ToArray();
                }

                return availableFrameworks;
            }
        }

        /// <summary>
        /// Returns true if the current RuntimeFramework is available.
        /// In the current implementation, only Mono and Microsoft .NET
        /// are supported.
        /// </summary>
        /// <returns>True if it's available, false if not</returns>
        public bool IsAvailable
        {
            get
            {
                foreach (RuntimeFramework framework in AvailableFrameworks)
                    if (this.Supports(framework))
                        return true;

                return false;
            }
        }

        /// <summary>
        /// The type of this runtime framework
        /// </summary>
        public RuntimeType Runtime
        {
            get { return runtime; }
        }

        /// <summary>
        /// The framework version for this runtime framework
        /// </summary>
        public Version FrameworkVersion
        {
            get { return frameworkVersion; }
        }

        /// <summary>
        /// The CLR version for this runtime framework
        /// </summary>
        public Version ClrVersion
        {
            get { return clrVersion; }
        }

        /// <summary>
        /// Return true if any CLR version may be used in
        /// matching this RuntimeFramework object.
        /// </summary>
        public bool AllowAnyVersion
        {
            get { return this.clrVersion == DefaultVersion; }
        }

        /// <summary>
        /// Returns the Display name for this framework
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses a string representing a RuntimeFramework.
        /// The string may be just a RuntimeType name or just
        /// a Version or a hyphentated RuntimeType-Version or
        /// a Version prefixed by 'v'.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static RuntimeFramework Parse(string s)
        {
            RuntimeType runtime = RuntimeType.Any;
            Version version = DefaultVersion;

            string[] parts = s.Split(new char[] { '-' });
            if (parts.Length == 2)
            {
                runtime = (RuntimeType)System.Enum.Parse(typeof(RuntimeType), parts[0], true);
                string vstring = parts[1];
                if (vstring != "")
                    version = new Version(vstring);
            }
            else if (char.ToLower(s[0]) == 'v')
            {
                version = new Version(s.Substring(1));
            }
            else if (IsRuntimeTypeName(s))
            {
                runtime = (RuntimeType)System.Enum.Parse(typeof(RuntimeType), s, true);
            }
            else
            {
                version = new Version(s);
            }

            return new RuntimeFramework(runtime, version);
        }

        /// <summary>
        /// Returns the best available framework that matches a target framework.
        /// If the target framework has a build number specified, then an exact
        /// match is needed. Otherwise, the matching framework with the highest
        /// build number is used.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static RuntimeFramework GetBestAvailableFramework(RuntimeFramework target)
        {
            RuntimeFramework result = target;

            if (target.ClrVersion.Build < 0)
            {
                foreach (RuntimeFramework framework in AvailableFrameworks)
                    if (framework.Supports(target) && 
                        framework.ClrVersion.Build > result.ClrVersion.Build)
                    {
                        result = framework;
                    }
            }

            return result;
        }

        /// <summary>
        /// Overridden to return the short name of the framework
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
            if (this.AllowAnyVersion)
            {
                return runtime.ToString().ToLower();
            }
            else
            {
                string vstring = frameworkVersion.ToString();
                if (runtime == RuntimeType.Any)
                    return "v" + vstring;
                else
                    return runtime.ToString().ToLower() + "-" + vstring;
            }
		}

        /// <summary>
        /// Returns true if this framework's supports executing under the 
        /// requested target framework. The target is supported if
        /// 
        /// 1. The runtime types are the same or either one is RuntimeType.Any
        /// 
        /// 2. All specified (non-negative) components of the CLR version are equal. 
        /// 
        /// 3. The major and minor components of the current framework version are
        ///    greater than or equal to the corresponding target components.
        ///    
        /// The last provision allows tests requiring .NET 2.0 to run under the
        /// 3.0 and 3.5 platforms and those requiring .NET 4.0 to run under 4.5.
        /// </summary>
        /// <param name="target">The RuntimeFramework to be matched.</param>
        /// <returns>True on match, otherwise false</returns>
        public bool Supports(RuntimeFramework target)
        {
            if (this.Runtime != RuntimeType.Any
                && target.Runtime != RuntimeType.Any
                && this.Runtime != target.Runtime)
                return false;

            if (this.AllowAnyVersion || target.AllowAnyVersion)
                return true;

            return VersionsMatch(this.ClrVersion, target.ClrVersion)
                && this.FrameworkVersion.Major >= target.FrameworkVersion.Major
                && this.FrameworkVersion.Minor >= target.FrameworkVersion.Minor;
        }

        #endregion

        #region Helper Methods

        private static bool IsRuntimeTypeName(string name)
        {
            foreach (string item in Enum.GetNames(typeof(RuntimeType)))
                if (item.ToLower() == name.ToLower())
                    return true;

            return false;
        }

        private static string GetDefaultDisplayName(RuntimeType runtime, Version version)
        {
            if (version == DefaultVersion)
                return runtime.ToString();
            else if (runtime == RuntimeType.Any)
                return "v" + version.ToString();
            else if (runtime == RuntimeType.Mono && version.Major == 1)
                return "Mono 1.0";
            else
                return runtime.ToString() + " " + version.ToString();
        }

        private static bool VersionsMatch(Version v1, Version v2)
        {
            return v1.Major == v2.Major &&
                   v1.Minor == v2.Minor &&
                  (v1.Build < 0 || v2.Build < 0 || v1.Build == v2.Build) &&
                  (v1.Revision < 0 || v2.Revision < 0 || v1.Revision == v2.Revision);
        }

        private static void AppendMonoFrameworks(FrameworkCollection frameworks)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                AppendAllMonoFrameworks(frameworks);
            else
                AppendDefaultMonoFramework(frameworks);
        }

        private static void AppendAllMonoFrameworks(FrameworkCollection frameworks)
        {
            // TODO: Find multiple installed Mono versions under Linux
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // Use registry to find alternate versions
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Novell\Mono");
                if (key == null) return;

                foreach (string version in key.GetSubKeyNames())
                {
                    RegistryKey subKey = key.OpenSubKey(version);
                    string monoPrefix = subKey.GetValue("SdkInstallRoot") as string;

                    AppendMonoFramework(frameworks, monoPrefix, version);
                }
            }
            else
                AppendDefaultMonoFramework(frameworks);
        }

        // This method works for Windows and Linux but currently
        // is only called under Linux.
        private static void AppendDefaultMonoFramework(FrameworkCollection frameworks)
        {
            string monoPrefix = null;
            string version = null;

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Novell\Mono");
                if (key != null)
                {
                    version = key.GetValue("DefaultCLR") as string;
                    if (version != null && version != "")
                    {
                        key = key.OpenSubKey(version);
                        if (key != null)
                            monoPrefix = key.GetValue("SdkInstallRoot") as string;
                    }
                }
            }
            else // Assuming we're currently running Mono - change if more runtimes are added
            {
                string libMonoDir = Path.GetDirectoryName(typeof(object).Assembly.Location);
                monoPrefix = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(libMonoDir)));
            }

            AppendMonoFramework(frameworks, monoPrefix, version);
        }

        private static void AppendMonoFramework(FrameworkCollection frameworks, string monoPrefix, string version)
        {
            if (monoPrefix != null)
            {
                string displayFmt = version != null
                    ? "Mono " + version + " - {0} Profile"
                    : "Mono {0} Profile";

                if (File.Exists(Path.Combine(monoPrefix, "lib/mono/1.0/mscorlib.dll")))
                {
                    RuntimeFramework framework = new RuntimeFramework(RuntimeType.Mono, new Version(1, 1, 4322));
                    framework.displayName = string.Format(displayFmt, "1.0");
                    frameworks.Add(framework);
                }

                if (File.Exists(Path.Combine(monoPrefix, "lib/mono/2.0/mscorlib.dll")))
                {
                    RuntimeFramework framework = new RuntimeFramework(RuntimeType.Mono, new Version(2, 0, 50727));
                    framework.displayName = string.Format(displayFmt, "2.0");
                    frameworks.Add(framework);
                }

                if (File.Exists(Path.Combine(monoPrefix, "lib/mono/4.0/mscorlib.dll")))
                {
                    RuntimeFramework framework = new RuntimeFramework(RuntimeType.Mono, new Version(4, 0, 30319));
                    framework.displayName = string.Format(displayFmt, "4.0");
                    frameworks.Add(framework);
                }
            }
        }

        private static void AppendDotNetFrameworks(FrameworkCollection frameworks)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\.NETFramework\policy");
                if (key != null)
                {
                    foreach (string name in key.GetSubKeyNames())
                    {
                        if (name.StartsWith("v"))
                        {
                            RegistryKey key2 = key.OpenSubKey(name);
                            foreach (string build in key2.GetValueNames())
                                frameworks.Add(new RuntimeFramework(RuntimeType.Net, new Version(name.Substring(1) + "." + build)));
                        }
                    }
                }
            }
        }

#if CLR_2_0 || CLR_4_0
        private class FrameworkCollection : System.Collections.Generic.List<RuntimeFramework> { }
#else
        private class FrameworkCollection : ArrayList 
        {
            public new RuntimeFramework[] ToArray()
            {
                return (RuntimeFramework[])ToArray(typeof(RuntimeFramework));
            }
        }
#endif

        #endregion
    }
}
