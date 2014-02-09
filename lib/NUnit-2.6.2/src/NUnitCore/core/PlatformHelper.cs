// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Reflection;

namespace NUnit.Core
{
	public class PlatformHelper
	{
		private OSPlatform os;
		private RuntimeFramework rt;

		// Set whenever we fail to support a list of platforms
		private string reason = string.Empty;

        /// <summary>
		/// Comma-delimited list of all supported OS platform constants
		/// </summary>
		public static readonly string OSPlatforms =
#if CLR_2_0 || CLR_4_0
            "Win,Win32,Win32S,Win32NT,Win32Windows,WinCE,Win95,Win98,WinMe,NT3,NT4,NT5,NT6,Win2K,WinXP,Win2003Server,Vista,Win2008Server,Win2008ServerR2,Win2012Server,Windows7,Windows8,Unix,Linux,Xbox,MacOSX";
#else
			"Win,Win32,Win32S,Win32NT,Win32Windows,WinCE,Win95,Win98,WinMe,NT3,NT4,NT5,NT6,Win2K,WinXP,Win2003Server,Vista,Win2008Server,Win2008ServerR2,Win2012Server,Windows7,Windows8,Unix,Linux";
#endif
		
		/// <summary>
		/// Comma-delimited list of all supported Runtime platform constants
		/// </summary>
		public static readonly string RuntimePlatforms =
			"Net,NetCF,SSCLI,Rotor,Mono";

		/// <summary>
		/// Default constructor uses the operating system and
		/// common language runtime of the system.
		/// </summary>
		public PlatformHelper()
		{
			this.os = OSPlatform.CurrentPlatform;
			this.rt = RuntimeFramework.CurrentFramework;
		}

		/// <summary>
		/// Contruct a PlatformHelper for a particular operating
		/// system and common language runtime. Used in testing.
		/// </summary>
		/// <param name="os">OperatingSystem to be used</param>
		public PlatformHelper( OSPlatform os, RuntimeFramework rt )
		{
			this.os = os;
			this.rt = rt;
		}

		/// <summary>
		/// Test to determine if one of a collection of platforms
		/// is being used currently.
		/// </summary>
		/// <param name="platforms"></param>
		/// <returns></returns>
		public bool IsPlatformSupported( string[] platforms )
		{
			foreach( string platform in platforms )
				if ( IsPlatformSupported( platform ) )
					return true;

			return false;
		}

		/// <summary>
		/// Tests to determine if the current platform is supported
		/// based on a platform attribute.
		/// </summary>
		/// <param name="platformAttribute">The attribute to examine</param>
		/// <returns></returns>
		public bool IsPlatformSupported( Attribute platformAttribute )
		{
			//Use reflection to avoid dependency on a particular framework version
			string include = (string)Reflect.GetPropertyValue( 
				platformAttribute, "Include", 
				BindingFlags.Public | BindingFlags.Instance );

			string exclude = (string)Reflect.GetPropertyValue(
				platformAttribute, "Exclude", 
				BindingFlags.Public | BindingFlags.Instance );

			try
			{
				if (include != null && !IsPlatformSupported(include))
				{
					reason = string.Format("Only supported on {0}", include);
					return false;
				}

				if (exclude != null && IsPlatformSupported(exclude))
				{
					reason = string.Format("Not supported on {0}", exclude);
					return false;
				}
			}
			catch( ArgumentException ex )
			{
				reason = string.Format( "Invalid platform name: {0}", ex.ParamName );
				return false; 
			}

			return true;
		}

		/// <summary>
		/// Test to determine if the a particular platform or comma-
		/// delimited set of platforms is in use.
		/// </summary>
		/// <param name="platform">Name of the platform or comma-separated list of platform names</param>
		/// <returns>True if the platform is in use on the system</returns>
		public bool IsPlatformSupported( string platform )
		{
			if ( platform.IndexOf( ',' ) >= 0 )
				return IsPlatformSupported( platform.Split( new char[] { ',' } ) );

			string platformName = platform.Trim();
            bool isSupported = false;

            //string versionSpecification = null;

            //string[] parts = platformName.Split( new char[] { '-' } );
            //if ( parts.Length == 2 )
            //{
            //    platformName = parts[0];
            //    versionSpecification = parts[1];
            //}

			switch( platformName.ToUpper() )
			{
				case "WIN":
				case "WIN32":
                    isSupported = os.IsWindows;
					break;
				case "WIN32S":
                    isSupported = os.IsWin32S;
					break;
				case "WIN32WINDOWS":
                    isSupported = os.IsWin32Windows;
					break;
				case "WIN32NT":
                    isSupported = os.IsWin32NT;
					break;
				case "WINCE":
                    isSupported = os.IsWinCE;
					break;
				case "WIN95":
                    isSupported = os.IsWin95;
					break;
				case "WIN98":
                    isSupported = os.IsWin98;
					break;
				case "WINME":
                    isSupported = os.IsWinME;
					break;
				case "NT3":
                    isSupported = os.IsNT3;
					break;
				case "NT4":
                    isSupported = os.IsNT4;
					break;
                case "NT5":
                    isSupported = os.IsNT5;
                    break;
                case "WIN2K":
                    isSupported = os.IsWin2K;
					break;
				case "WINXP":
                    isSupported = os.IsWinXP;
					break;
				case "WIN2003SERVER":
                    isSupported = os.IsWin2003Server;
					break;
                case "NT6":
                    isSupported = os.IsNT6;
                    break;
                case "VISTA":
                    isSupported = os.IsVista;
                    break;
                case "WIN2008SERVER":
                    isSupported = os.IsWin2008Server;
                    break;
                case "WIN2008SERVERR2":
                    isSupported = os.IsWin2008ServerR2;
                    break;
                case "WIN2012SERVER":
                    isSupported = os.IsWin2012Server;
                    break;
                case "WINDOWS7":
                    isSupported = os.IsWindows7;
                    break;
                case "WINDOWS8":
                    isSupported = os.IsWindows8;
                    break;
                case "UNIX":
				case "LINUX":
                    isSupported = os.IsUnix;
					break;
#if (CLR_2_0 || CLR_4_0) && !NETCF
                case "XBOX":
                    isSupported = os.IsXbox;
                    break;
                case "MACOSX":
                    isSupported = os.IsMacOSX;
                    break;
#endif

                default:
                    isSupported = IsRuntimeSupported(platformName);
                    break;
			}

            if (!isSupported)
			    this.reason = "Only supported on " + platform;

			return isSupported;
		}

		/// <summary>
		/// Return the last failure reason. Results are not
		/// defined if called before IsSupported( Attribute )
		/// is called.
		/// </summary>
		public string Reason
		{
			get { return reason; }
		}

        private bool IsRuntimeSupported(string platformName)
        {
            string versionSpecification = null;
            string[] parts = platformName.Split(new char[] { '-' });
            if (parts.Length == 2)
            {
                platformName = parts[0];
                versionSpecification = parts[1];
            }

            switch (platformName.ToUpper())
            {
                case "NET":
                    return IsRuntimeSupported(RuntimeType.Net, versionSpecification);

                case "NETCF":
                    return IsRuntimeSupported(RuntimeType.NetCF, versionSpecification);

                case "SSCLI":
                case "ROTOR":
                    return IsRuntimeSupported(RuntimeType.SSCLI, versionSpecification);

                case "MONO":
                    return IsRuntimeSupported(RuntimeType.Mono, versionSpecification);

                default:
                    throw new ArgumentException("Invalid platform name", platformName);
            }
        }

        private bool IsRuntimeSupported(RuntimeType runtime, string versionSpecification)
        {
            Version version = versionSpecification == null
                ? RuntimeFramework.DefaultVersion
                : new Version(versionSpecification);

            RuntimeFramework target = new RuntimeFramework(runtime, version);

            return rt.Supports(target);
        }
    }
}
