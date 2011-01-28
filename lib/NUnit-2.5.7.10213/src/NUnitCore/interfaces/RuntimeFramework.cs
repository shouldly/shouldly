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
            this.frameworkVersion = version;

            this.clrVersion = version;
            if (frameworkVersion.Major == 3)
                this.clrVersion = new Version(2, 0, 50727);
            else if (runtime == RuntimeType.Mono && version.Major == 1)
                this.clrVersion = new Version(1, 1, 4322);

            this.displayName = GetDefaultDisplayName(runtime, version);
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

                    if (isMono && major == 1)
                        minor = 0;

                    currentFramework = new RuntimeFramework(runtime, new Version(major, minor));
                    currentFramework.clrVersion = Environment.Version;

                    if (isMono)
                    {
                        MethodInfo getDisplayNameMethod = monoRuntimeType.GetMethod(
                            "GetDisplayName", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.ExactBinding);
                        if (getDisplayNameMethod != null)
                            currentFramework.displayName = (string)getDisplayNameMethod.Invoke(null, new object[0]);
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
                    AppendMonoFrameworks(frameworks);

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
                    if (this.Matches(framework))
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
                    if (framework.Matches(target) && 
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
        /// Returns true if this framework "matches" the one supplied
        /// as an argument. Two frameworks match if their runtime types 
        /// are the same or either one is RuntimeType.Any and all specified 
        /// components of the CLR version are equal. Negative (i.e. unspecified) 
        /// version components are ignored.
        /// </summary>
        /// <param name="other">The RuntimeFramework to be matched.</param>
        /// <returns>True on match, otherwise false</returns>
        public bool Matches(RuntimeFramework other)
        {
            if (this.Runtime != RuntimeType.Any
                && other.Runtime != RuntimeType.Any
                && this.Runtime != other.Runtime)
                return false;

            if (this.AllowAnyVersion || other.AllowAnyVersion)
                return true;

            return this.ClrVersion.Major == other.ClrVersion.Major
                && this.ClrVersion.Minor == other.ClrVersion.Minor
                && (   this.ClrVersion.Build < 0 
                    || other.ClrVersion.Build < 0 
                    || this.ClrVersion.Build == other.ClrVersion.Build ) 
                && (   this.ClrVersion.Revision < 0
                    || other.ClrVersion.Revision < 0
                    || this.ClrVersion.Revision == other.ClrVersion.Revision );
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
            else
                return runtime.ToString() + " " + version.ToString();
        }

        private static void AppendMonoFrameworks(FrameworkCollection frameworks)
        {
#if true
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
                monoPrefix = Path.GetDirectoryName(Path.GetDirectoryName(libMonoDir));
            }
            
            if (monoPrefix != null)
            {
                string displayFmt = version != null
                    ? "Mono " + version + " - {0} Profile"
                    : "Mono {0} Profile";

                RuntimeFramework framework = new RuntimeFramework(RuntimeType.Mono, new Version(1, 1, 4322));
                framework.displayName = string.Format(displayFmt, "1.0");
                frameworks.Add(framework);

                framework = new RuntimeFramework(RuntimeType.Mono, new Version(2, 0, 50727));
                framework.displayName = string.Format(displayFmt, "2.0");
                frameworks.Add(framework);

                if (File.Exists(Path.Combine(monoPrefix, "lib/Mono/4.0/mscorlib.dll")))
                {
                    framework = new RuntimeFramework(RuntimeType.Mono, new Version(4, 0, 30319));
                    framework.displayName = string.Format(displayFmt, "4.0");
                    frameworks.Add(framework);
                }
            }
#else
            // Code to list various versions of Mono - currently not used
            // because it only works on Windows
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Novell\Mono");
            if (key == null) return;

            foreach (string name in key.GetSubKeyNames())
            {
                RuntimeFramework framework = new RuntimeFramework(RuntimeType.Mono, new Version(1, 0));
                framework.displayName = "Mono " + name + " - 1.0 Profile";
                frameworks.Add(framework);
                framework = new RuntimeFramework(RuntimeType.Mono, new Version(2, 0));
                framework.displayName = "Mono " + name + " - 2.0 Profile";
                frameworks.Add(framework);
            }
#endif
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

#if NET_2_0
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
