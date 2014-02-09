// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Reflection;
using System.Collections;
using Microsoft.Win32;

namespace NUnit.ProjectEditor
{
	/// <summary>
	/// Enumeration identifying a common language 
	/// runtime implementation.
	/// </summary>
	public enum RuntimeType
	{
        /// <summary>AnyRuntime supported runtime framework</summary>
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
	/// RuntimeFramework encapsulates the naming standards
    /// for identifying CLR implementations by runtime type
    /// and version in the XML project file and elsewhere.
	/// </summary>
    [Serializable]
	public sealed class RuntimeFramework
    {
        #region Instance Fields

        private static readonly RuntimeFramework anyRuntime = new RuntimeFramework(RuntimeType.Any, new Version());

        private RuntimeType runtime;
        private Version version;

        #endregion

        #region Constructors

        /// <summary>
		/// Construct from a runtime type and version
		/// </summary>
		/// <param name="runtime">The runtime type of the framework</param>
		/// <param name="version">The version of the framework</param>
		public RuntimeFramework( RuntimeType runtime, Version version)
		{
			this.runtime = runtime;
            this.version = version;
        }

        /// <summary>
        /// Construct from a string
        /// </summary>
        /// <param name="s">A string representing the runtime</param>
        public RuntimeFramework(string s)
        {
            runtime = RuntimeType.Any;
            version = new Version();

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
            else if (char.IsNumber(s[0]))
            {
                version = new Version(s);
            }
            else
            {
                runtime = (RuntimeType)System.Enum.Parse(typeof(RuntimeType), s, true);
                version = Environment.Version;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Represents any runtime at all
        /// </summary>
        public static RuntimeFramework AnyRuntime
        {
            get { return anyRuntime; }
        }

        /// <summary>
        /// The type of this runtime framework
        /// </summary>
        public RuntimeType Runtime
        {
            get { return runtime; }
        }

        /// <summary>
        /// The version of this runtime framework
        /// </summary>
        public Version Version
        {
            get { return version; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Overridden to return the short name of the framework
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
            string vstring = version.ToString();
            if (runtime == RuntimeType.Any)
                return "v" + vstring;
            else
                return runtime.ToString().ToLower() + "-" + vstring;
		}

        #endregion
    }
}
