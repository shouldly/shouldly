﻿// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Runtime.InteropServices;

namespace NUnit.Core
{
    /// <summary>
    /// OSPlatform represents a particular operating system platform
    /// </summary>
    public class OSPlatform
    {
        PlatformID platform;
        Version version;
        ProductType product;

        #region Static Members
        private static OSPlatform currentPlatform;

        
        /// <summary>
        /// Platform ID for Unix as defined by Microsoft .NET 2.0 and greater
        /// </summary>
        public static readonly PlatformID UnixPlatformID_Microsoft = (PlatformID)4;

        /// <summary>
        /// Platform ID for Unix as defined by Mono
        /// </summary>
        public static readonly PlatformID UnixPlatformID_Mono = (PlatformID)128;

        /// <summary>
        /// Get the OSPlatform under which we are currently running
        /// </summary>
        public static OSPlatform CurrentPlatform
        {
            get
            {
                if (currentPlatform == null)
                {
                    OperatingSystem os = Environment.OSVersion;

                    if (os.Platform == PlatformID.Win32NT && os.Version.Major >= 6)
                    {
                        OSVERSIONINFOEX osvi = new OSVERSIONINFOEX();
                        osvi.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osvi);
                        GetVersionEx(ref osvi);
                        currentPlatform = new OSPlatform(os.Platform, os.Version, (ProductType)osvi.ProductType);
                    }
                    else
                        currentPlatform = new OSPlatform(os.Platform, os.Version);
                }

                return currentPlatform;
            }
        }
        #endregion

        #region Members used for Win32NT platform only
        /// <summary>
        /// Product Type Enumeration used for Windows
        /// </summary>
        public enum ProductType
        {
            /// <summary>
            /// Product type is unknown or unspecified
            /// </summary>
            Unknown,

            /// <summary>
            /// Product type is Workstation
            /// </summary>
            WorkStation,

            /// <summary>
            /// Product type is Domain Controller
            /// </summary>
            DomainController,

            /// <summary>
            /// Product type is Server
            /// </summary>
            Server,
        }

        [StructLayout(LayoutKind.Sequential)]
        struct OSVERSIONINFOEX
        {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public Int16 wServicePackMajor;
            public Int16 wServicePackMinor;
            public Int16 wSuiteMask;
            public Byte ProductType;
            public Byte Reserved;
        }

        [DllImport("Kernel32.dll")]
        private static extern bool GetVersionEx(ref OSVERSIONINFOEX osvi);
        #endregion

        /// <summary>
        /// Construct from a platform ID and version
        /// </summary>
        public OSPlatform(PlatformID platform, Version version)
        {
            this.platform = platform;
            this.version = version;
        }

        /// <summary>
        /// Construct from a platform ID, version and product type
        /// </summary>
        public OSPlatform(PlatformID platform, Version version, ProductType product)
            : this( platform, version )
        {
            this.product = product;
        }

        /// <summary>
        /// Get the platform ID of this instance
        /// </summary>
        public PlatformID Platform
        {
            get { return platform; }
        }

        /// <summary>
        /// Get the Version of this instance
        /// </summary>
        public Version Version
        {
            get { return version; }
        }

        /// <summary>
        /// Get the Product Type of this instance
        /// </summary>
        public ProductType Product
        {
            get { return product; }
        }

        /// <summary>
        /// Return true if this is a windows platform
        /// </summary>
        public bool IsWindows
        {
            get
            {
                return platform == PlatformID.Win32NT
                    || platform == PlatformID.Win32Windows
                    || platform == PlatformID.Win32S
                    || platform == PlatformID.WinCE;
            }
        }

        /// <summary>
        /// Return true if this is a Unix or Linux platform
        /// </summary>
        public bool IsUnix
        {
            get
            {
                return platform == UnixPlatformID_Microsoft
                    || platform == UnixPlatformID_Mono;
            }
        }

        /// <summary>
        /// Return true if the platform is Win32S
        /// </summary>
        public bool IsWin32S
        {
            get { return platform == PlatformID.Win32S; }
        }

        /// <summary>
        /// Return true if the platform is Win32Windows
        /// </summary>
        public bool IsWin32Windows
        {
            get { return platform == PlatformID.Win32Windows; }
        }

        /// <summary>
        ///  Return true if the platform is Win32NT
        /// </summary>
        public bool IsWin32NT
        {
            get { return platform == PlatformID.Win32NT; }
        }

        /// <summary>
        /// Return true if the platform is Windows CE
        /// </summary>
        public bool IsWinCE
        {
            get { return (int)platform == 3; } // PlatformID.WinCE not defined in .NET 1.0
        }

        /// <summary>
        /// Return true if the platform is Windows 95
        /// </summary>
        public bool IsWin95
        {
            get { return platform == PlatformID.Win32Windows && version.Major == 4 && version.Minor == 0; }
        }

        /// <summary>
        /// Return true if the platform is Windows 98
        /// </summary>
        public bool IsWin98
        {
            get { return platform == PlatformID.Win32Windows && version.Major == 4 && version.Minor == 10; }
        }

        /// <summary>
        /// Return true if the platform is Windows ME
        /// </summary>
        public bool IsWinME
        {
            get { return platform == PlatformID.Win32Windows && version.Major == 4 && version.Minor == 90; }
        }

        /// <summary>
        /// Return true if the platform is NT 3
        /// </summary>
        public bool IsNT3
        {
            get { return platform == PlatformID.Win32NT && version.Major == 3; }
        }

        /// <summary>
        /// Return true if the platform is NT 4
        /// </summary>
        public bool IsNT4
        {
            get { return platform == PlatformID.Win32NT && version.Major == 4; }
        }

        /// <summary>
        /// Return true if the platform is NT 5
        /// </summary>
        public bool IsNT5
        {
            get { return platform == PlatformID.Win32NT && version.Major == 5; }
        }

        /// <summary>
        /// Return true if the platform is Windows 2000
        /// </summary>
        public bool IsWin2K
        {
            get { return IsNT5 && version.Minor == 0; }
        }

        /// <summary>
        /// Return true if the platform is Windows XP
        /// </summary>
        public bool IsWinXP
        {
            get { return IsNT5 && version.Minor == 1; }
        }

        /// <summary>
        /// Return true if the platform is Windows 2003 Server
        /// </summary>
        public bool IsWin2003Server
        {
            get { return IsNT5 && version.Minor == 2; }
        }

        /// <summary>
        /// Return true if the platform is NT 6
        /// </summary>
        public bool IsNT6
        {
            get { return platform == PlatformID.Win32NT && version.Major == 6; }
        }

        /// <summary>
        /// Return true if the platform is Vista
        /// </summary>
        public bool IsVista
        {
            get { return IsNT6 && version.Minor == 0 && Product == ProductType.WorkStation; }
        }

        /// <summary>
        /// Return true if the platform is Windows 2008 Server
        /// </summary>
        public bool IsWin2008Server
        {
            get { return IsNT6 && version.Minor == 0 && Product == ProductType.Server; }
        }
    }
}
