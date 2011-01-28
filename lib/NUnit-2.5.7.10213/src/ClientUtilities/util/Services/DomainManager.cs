// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;
using System.Security;
using System.Security.Policy;
using NUnit.Core;

namespace NUnit.Util
{
	/// <summary>
	/// The DomainManager class handles the creation and unloading
	/// of domains as needed and keeps track of all existing domains.
	/// </summary>
	public class DomainManager : IService
	{
        static Logger log = InternalTrace.GetLogger(typeof(DomainManager));

		#region Properties
		private static string shadowCopyPath;
		public static string ShadowCopyPath
		{
			get
			{
				if ( shadowCopyPath == null )
				{
                    shadowCopyPath = Services.UserSettings.GetSetting("Options.TestLoader.ShadowCopyPath", "");
                    if (shadowCopyPath == "")
                        shadowCopyPath = Path.Combine(Path.GetTempPath(), @"nunit20\ShadowCopyCache");
					else
						shadowCopyPath = Environment.ExpandEnvironmentVariables(shadowCopyPath);
				}

				return shadowCopyPath;
			}
		}
		#endregion

		#region Create and Unload Domains
		/// <summary>
		/// Construct an application domain for running a test package
		/// </summary>
		/// <param name="package">The TestPackage to be run</param>
		public AppDomain CreateDomain( TestPackage package )
		{
			AppDomainSetup setup = new AppDomainSetup();
			 
			//For paralell tests, we need to use distinct application name
        	setup.ApplicationName = "Tests" + "_" + Environment.TickCount;

            FileInfo testFile = package.FullName != null && package.FullName != string.Empty
                ? new FileInfo(package.FullName)
                : null;

            string appBase = package.BasePath;
            string configFile = package.ConfigurationFile;
            string binPath = package.PrivateBinPath;

            if (testFile != null)
            {
                if (appBase == null || appBase == string.Empty)
                    appBase = testFile.DirectoryName;

                if (configFile == null || configFile == string.Empty)
                    configFile = Services.ProjectService.CanLoadProject(testFile.Name)
                        ? Path.GetFileNameWithoutExtension(testFile.Name) + ".config"
                        : testFile.Name + ".config";
            }
            else if (appBase == null || appBase == string.Empty)
                appBase = GetCommonAppBase(package.Assemblies);

            setup.ApplicationBase = appBase;
            // TODO: Check whether Mono still needs full path to config file...
            setup.ConfigurationFile = appBase != null && configFile != null
                ? Path.Combine(appBase, configFile)
                : configFile;

            if (package.AutoBinPath)
				binPath = GetPrivateBinPath( appBase, package.Assemblies );

			setup.PrivateBinPath = binPath;

            if (package.GetSetting("ShadowCopyFiles", true))
            {
                setup.ShadowCopyFiles = "true";
                setup.ShadowCopyDirectories = appBase;
                setup.CachePath = GetCachePath();
            }
            else
                setup.ShadowCopyFiles = "false";

			string domainName = "test-domain-" + package.Name;
            // Setup the Evidence
            Evidence evidence = new Evidence(AppDomain.CurrentDomain.Evidence);
            if (evidence.Count == 0)
            {
                Zone zone = new Zone(SecurityZone.MyComputer);
                evidence.AddHost(zone);
                Assembly assembly = Assembly.GetExecutingAssembly();
                Url url = new Url(assembly.CodeBase);
                evidence.AddHost(url);
                Hash hash = new Hash(assembly);
                evidence.AddHost(hash);
            }

            log.Info("Creating AppDomain " + domainName);

            AppDomain runnerDomain = AppDomain.CreateDomain(domainName, evidence, setup);

			// HACK: Only pass down our AddinRegistry one level so that tests of NUnit
			// itself start without any addins defined.
			if ( !IsTestDomain( AppDomain.CurrentDomain ) )
				runnerDomain.SetData("AddinRegistry", Services.AddinRegistry);

            // Inject DomainInitializer into the remote domain - there are other
            // approaches, but this works for all CLR versions.
            DomainInitializer initializer = DomainInitializer.CreateInstance(runnerDomain);
            initializer.InitializeDomain( IsTestDomain(AppDomain.CurrentDomain)
                ? TraceLevel.Off : InternalTrace.Level );

			return runnerDomain;
		}

        public void Unload(AppDomain domain)
        {
            new DomainUnloader(domain).Unload();
        }

		#endregion

        #region Nested DomainUnloader Class
        class DomainUnloader
        {
            private Thread thread;
            private AppDomain domain;

            public DomainUnloader(AppDomain domain)
            {
                this.domain = domain;
            }

            public void Unload()
            {
                log.Info("Unloading AppDomain " + domain.FriendlyName);

                thread = new Thread(new ThreadStart(UnloadOnThread));
                thread.Start();
                if (!thread.Join(20000))
                {
                    log.Error("Unable to unload AppDomain {0}, Unload thread timed out", domain.FriendlyName);
                    thread.Abort();
                }
            }

            private void UnloadOnThread()
            {
                bool shadowCopy = domain.ShadowCopyFiles;
                string cachePath = domain.SetupInformation.CachePath;
                string domainName = domain.FriendlyName;

                try
                {
                    AppDomain.Unload(domain);
                }
                catch (Exception ex)
                {
                    // We assume that the tests did something bad and just leave
                    // the orphaned AppDomain "out there". 
                    // TODO: Something useful.
                    log.Error("Unable to unload AppDomain " + domainName, ex);
                }
                finally
                {
                    if (shadowCopy)
                        DeleteCacheDir(new DirectoryInfo(cachePath));
                }
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
		/// Get the location for caching and delete any old cache info
		/// </summary>
		private string GetCachePath()
		{
            int processId = Process.GetCurrentProcess().Id;
            long ticks = DateTime.Now.Ticks;
			string cachePath = Path.Combine( ShadowCopyPath, processId.ToString() + "_" + ticks.ToString() ); 
				
			try 
			{
				DirectoryInfo dir = new DirectoryInfo(cachePath);		
				if(dir.Exists) dir.Delete(true);
			}
			catch( Exception ex)
			{
				throw new ApplicationException( 
					string.Format( "Invalid cache path: {0}",cachePath ),
					ex );
			}

			return cachePath;
		}

		/// <summary>
		/// Helper method to delete the cache dir. This method deals 
		/// with a bug that occurs when files are marked read-only
		/// and deletes each file separately in order to give better 
		/// exception information when problems occur.
		/// 
		/// TODO: This entire method is problematic. Should we be doing it?
		/// </summary>
		/// <param name="cacheDir"></param>
		private static void DeleteCacheDir( DirectoryInfo cacheDir )
		{
			//			Debug.WriteLine( "Modules:");
			//			foreach( ProcessModule module in Process.GetCurrentProcess().Modules )
			//				Debug.WriteLine( module.ModuleName );
			

			if(cacheDir.Exists)
			{
				foreach( DirectoryInfo dirInfo in cacheDir.GetDirectories() )
					DeleteCacheDir( dirInfo );

				foreach( FileInfo fileInfo in cacheDir.GetFiles() )
				{
					fileInfo.Attributes = FileAttributes.Normal;
					try 
					{
						fileInfo.Delete();
					}
					catch( Exception ex )
					{
						Debug.WriteLine( string.Format( 
							"Error deleting {0}, {1}", fileInfo.Name, ex.Message ) );
					}
				}

				cacheDir.Attributes = FileAttributes.Normal;

				try
				{
					cacheDir.Delete();
				}
				catch( Exception ex )
				{
					Debug.WriteLine( string.Format( 
						"Error deleting {0}, {1}", cacheDir.Name, ex.Message ) );
				}
			}
		}

		private bool IsTestDomain(AppDomain domain)
		{
			return domain.FriendlyName.StartsWith( "test-domain-" );
		}

        public static string GetCommonAppBase(IList assemblies)
        {
            string commonBase = null;

            foreach (string assembly in assemblies)
            {
                string dir = Path.GetFullPath(Path.GetDirectoryName(assembly));
                if (commonBase == null)
                    commonBase = dir;
                else while (!PathUtils.SamePathOrUnder(commonBase, dir) && commonBase != null)
                        commonBase = Path.GetDirectoryName(commonBase);
            }

            return commonBase;
        }

		public static string GetPrivateBinPath( string basePath, IList assemblies )
		{
			StringBuilder sb = new StringBuilder(200);
			ArrayList dirList = new ArrayList();

			foreach( string assembly in assemblies )
			{
				string dir = PathUtils.RelativePath(
                    Path.GetFullPath(basePath), 
                    Path.GetDirectoryName( Path.GetFullPath(assembly) ) );
				if ( dir != null && dir != "." && !dirList.Contains( dir ) )
				{
					dirList.Add( dir );
					if ( sb.Length > 0 )
						sb.Append( Path.PathSeparator );
					sb.Append( dir );
				}
			}

			return sb.Length == 0 ? null : sb.ToString();
		}

		public static void DeleteShadowCopyPath()
		{
			if ( Directory.Exists( ShadowCopyPath ) )
				Directory.Delete( ShadowCopyPath, true );
		}
		#endregion

		#region IService Members

		public void UnloadService()
		{
            // TODO:  Add DomainManager.UnloadService implementation
        }

		public void InitializeService()
		{
			// TODO:  Add DomainManager.InitializeService implementation
		}

		#endregion
	}
}
