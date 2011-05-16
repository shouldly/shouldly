// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;
using System.Threading;
using Microsoft.Win32;

namespace NUnit.Core
{
    /// <summary>
    /// Provides static methods for accessing the NUnit config
    /// file 
    /// </summary>
    public class NUnitConfiguration
    {
        #region Class Constructor
        /// <summary>
        /// Class constructor initializes fields from config file
        /// </summary>
        static NUnitConfiguration()
        {
            try
            {
                NameValueCollection settings = GetConfigSection("NUnit/TestCaseBuilder");
                if (settings != null)
                {
                    string oldStyle = settings["OldStyleTestCases"];
                    if (oldStyle != null)
                            allowOldStyleTests = Boolean.Parse(oldStyle);
                }

                settings = GetConfigSection("NUnit/TestRunner");
                if (settings != null)
                {
                    string apartment = settings["ApartmentState"];
                    if (apartment != null)
                        apartmentState = (ApartmentState)
                            System.Enum.Parse(typeof(ApartmentState), apartment, true);

                    string priority = settings["ThreadPriority"];
                    if (priority != null)
                        threadPriority = (ThreadPriority)
                            System.Enum.Parse(typeof(ThreadPriority), priority, true);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Invalid configuration setting in {0}",
                    AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                throw new ApplicationException(msg, ex);
            }
        }

        private static NameValueCollection GetConfigSection( string name )
        {
#if NET_2_0
            return (NameValueCollection)System.Configuration.ConfigurationManager.GetSection(name);
#else
			return (NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig(name);
#endif
        }
        #endregion

        #region Public Properties

        #region AllowOldStyleTests
        private static bool allowOldStyleTests = false;
        public static bool AllowOldStyleTests
        {
            get { return allowOldStyleTests; }
        }
        #endregion

        #region ThreadPriority
        private static ThreadPriority threadPriority = ThreadPriority.Normal;
        public static ThreadPriority ThreadPriority
        {
            get { return threadPriority; }
        }
        #endregion

        #region ApartmentState
        private static ApartmentState apartmentState = ApartmentState.Unknown;
        public static ApartmentState ApartmentState
        {
            get { return apartmentState; }
            //set { apartmentState = value; }
        }
        #endregion

        #region BuildConfiguration
        public static string BuildConfiguration
        {
            get
            {
#if DEBUG
                    return "Debug";
#else
					return "Release";
#endif
            }
        }
        #endregion

        #region NUnitLibDirectory
        private static string nunitLibDirectory;
        /// <summary>
        /// Gets the path to the lib directory for the version and build
        /// of NUnit currently executing.
        /// </summary>
        public static string NUnitLibDirectory
        {
            get
            {
                if (nunitLibDirectory == null)
                {
                    nunitLibDirectory =
                        AssemblyHelper.GetDirectoryName(Assembly.GetExecutingAssembly());
                }

                return nunitLibDirectory;
            }
        }
        #endregion

        #region NUnitBinDirectory
        private static string nunitBinDirectory;
        public static string NUnitBinDirectory
        {
            get
            {
                if (nunitBinDirectory == null)
                {
                    nunitBinDirectory = NUnitLibDirectory;
                    if (Path.GetFileName(nunitBinDirectory).ToLower() == "lib")
                        nunitBinDirectory = Path.GetDirectoryName(nunitBinDirectory);
                }

                return nunitBinDirectory;
            }
        }
        #endregion

        #region AddinDirectory
        private static string addinDirectory;
        public static string AddinDirectory
        {
            get
            {
                if (addinDirectory == null)
                {
                    addinDirectory = Path.Combine(NUnitBinDirectory, "addins");
                }

                return addinDirectory;
            }
        }
        #endregion

        #region TestAgentExePath
        //private static string testAgentExePath;
        //private static string TestAgentExePath
        //{
        //    get
        //    {
        //        if (testAgentExePath == null)
        //            testAgentExePath = Path.Combine(NUnitBinDirectory, "nunit-agent.exe");

        //        return testAgentExePath;
        //    }
        //}
        #endregion

        #region MonoExePath
        private static string monoExePath;
        public static string MonoExePath
        {
            get
            {
                if (monoExePath == null)
                {
                    string[] searchNames = IsWindows()
                        ? new string[] { "mono.bat", "mono.cmd", "mono.exe" }
                        : new string[] { "mono", "mono.exe" };
                    
                    monoExePath = FindOneOnPath(searchNames);

                    if (monoExePath == null && IsWindows())
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Novell\Mono");
                        if (key != null)
                        {
                            string version = key.GetValue("DefaultCLR") as string;
                            if (version != null)
                            {
                                key = key.OpenSubKey(version);
                                if (key != null)
                                {
                                    string installDir = key.GetValue("SdkInstallRoot") as string;
                                    if (installDir != null)
                                        monoExePath = Path.Combine(installDir, @"bin\mono.exe");
                                }
                            }
                        }
                    }

                    if (monoExePath == null)
                        return "mono";
                }

                return monoExePath;
            }
        }

        private static string FindOneOnPath(string[] names)
        {
            //foreach (string dir in Environment.GetEnvironmentVariable("path").Split(new char[] { Path.PathSeparator }))
            //    foreach (string name in names)
            //    {
            //        string fullPath = Path.Combine(dir, name);
            //        if (File.Exists(fullPath))
            //            return name;
            //    }

            return null;
        }

        private static bool IsWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }
        #endregion

        #region ApplicationDataDirectory
        private static string applicationDirectory;
        public static string ApplicationDirectory
        {
            get
            {
                if (applicationDirectory == null)
                {
                    applicationDirectory = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "NUnit");
                }

                return applicationDirectory;
            }
        }
        #endregion

        #region HelpUrl
        public static string HelpUrl
        {
            get
            {
#if NET_2_0
                string helpUrl = ConfigurationManager.AppSettings["helpUrl"];
#else
                string helpUrl = ConfigurationSettings.AppSettings["helpUrl"];
#endif

                if (helpUrl == null)
                {
                    helpUrl = "http://nunit.org";
                    string dir = Path.GetDirectoryName(NUnitBinDirectory);
                    if ( dir != null )
                    {
                        dir = Path.GetDirectoryName(dir);
                        if ( dir != null )
                        {
                            string localPath = Path.Combine(dir, @"doc/index.html");
                            if (File.Exists(localPath))
                            {
                                UriBuilder uri = new UriBuilder();
                                uri.Scheme = "file";
                                uri.Host = "localhost";
                                uri.Path = localPath;
                                helpUrl = uri.ToString();
                            }
                        }
                    }
                }

                return helpUrl;
            }
        }
        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Return the NUnit Bin Directory for a particular
        /// runtime version, or null if it's not installed.
        /// For normal installations, there are only 1.1 and
        /// 2.0 directories. However, this method accomodates
        /// 3.5 and 4.0 directories for the benefit of NUnit
        /// developers using those runtimes.
        /// </summary>
        public static string GetNUnitBinDirectory(Version v)
        {
            string dir = NUnitBinDirectory;

            if ((Environment.Version.Major >= 2) == (v.Major >= 2))
                return dir;

            string[] v1Strings = new string[] { "1.0", "1.1" };
            string[] v2Strings = new string[] { "2.0", "3.0", "3.5", "4.0" };
            
            string[] search;
            string[] replace;

            if (Environment.Version.Major == 1)
            {
                search = v1Strings;
                replace = v2Strings;
            }
            else
            {
                search = v2Strings;
                replace = v1Strings;
            }

            // Look for current value in path so it can be replaced
            string current = null;
            foreach (string s in search)
                if (dir.IndexOf(s) >= 0)
                {
                    current = s;
                    break;
                }

            // If nothing found, we can't do it
            if (current == null)
                return null;

            // First try exact replacement
            string altDir = dir.Replace(current, v.ToString(2));
            if (Directory.Exists(altDir))
                return altDir;

            // Now try all the alternatives
            foreach (string target in replace)
            {
                altDir = dir.Replace(current, target);
                if (Directory.Exists(altDir))
                    return altDir;
            }

            // Nothing was found
            return null;
        }

        public static string GetTestAgentExePath(Version v)
        {
            string binDir = GetNUnitBinDirectory(v);
            if ( binDir == null ) return null;

#if NET_2_0
            Assembly a = System.Reflection.Assembly.GetEntryAssembly();
            string agentName = v.Major > 1 && a != null && a.GetName().ProcessorArchitecture == ProcessorArchitecture.X86
                ? "nunit-agent-x86.exe" 
                : "nunit-agent.exe";
#else
            string agentName = "nunit-agent.exe";
#endif

            string agentExePath = Path.Combine(binDir, agentName);
            return File.Exists(agentExePath) ? agentExePath : null;
        }

        #endregion
    }
}
