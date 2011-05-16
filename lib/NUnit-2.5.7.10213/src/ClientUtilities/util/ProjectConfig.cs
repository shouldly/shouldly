// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Text;
using System.Collections;
using System.IO;
using NUnit.Core;

namespace NUnit.Util
{
	public enum BinPathType
	{
		Auto,
		Manual,
		None
	}

	public class ProjectConfig
	{
		#region Instance Variables

		/// <summary>
		/// The name of this config
		/// </summary>
		private string name;

		/// <summary>
		/// IProject interface of containing project
		/// </summary>
		protected NUnitProject project = null;

		/// <summary>
		/// List of the names of the assemblies
		/// </summary>
		private AssemblyList assemblies;

		/// <summary>
		/// Base path specific to this configuration
		/// </summary>
		private string basePath;

		/// <summary>
		/// Our configuration file, if specified
		/// </summary>
		private string configFile;

		/// <summary>
		/// Private bin path, if specified
		/// </summary>
		private string binPath;

		/// <summary>
		/// True if assembly paths should be added to bin path
		/// </summary>
		private BinPathType binPathType = BinPathType.Auto;
        
        /// <summary>
        /// The CLR under which tests are to be run
        /// </summary>
        private RuntimeFramework runtimeFramework;

		#endregion

		#region Constructor
		public ProjectConfig( string name )
		{
			this.name = name;
			this.assemblies = new AssemblyList();
			assemblies.Changed += new EventHandler( assemblies_Changed );
		}
		#endregion

		#region Properties and Events

		public NUnitProject Project
		{
			set { project = value; }
		}

		public string Name
		{
			get { return name; }
			set 
			{
				if ( name != value )
				{
					name = value;
					NotifyProjectOfChange();
				}
			}
		}

		private bool BasePathSpecified
		{
			get 
			{
				return project.BasePathSpecified || this.basePath != null && this.basePath != "";
			}
		}

		/// <summary>
		/// The base directory for this config - used
		/// as the application base for loading tests.
		/// </summary>
		public string BasePath
		{
			get
			{ 
				if ( project == null || project.BasePath == null )
					return basePath;

				if ( basePath == null )
					return project.BasePath;

				return Path.Combine( project.BasePath, basePath );
			}
			set 
			{
				if ( BasePath != value )
				{
					basePath = value;
					NotifyProjectOfChange();
				}
			}
		}

		/// <summary>
		/// The base path relative to the project base
		/// </summary>
		public string RelativeBasePath
		{
			get
			{
				if ( project == null || basePath == null || !Path.IsPathRooted( basePath ) )
					return basePath;

				return PathUtils.RelativePath( project.BasePath, basePath );
			}
		}

		private bool ConfigurationFileSpecified
		{
			get { return configFile != null; }
		}

		public string ConfigurationFile
		{
			get 
			{ 
				return configFile == null && project != null
					? project.ConfigurationFile 
					: configFile;
			}
			set
			{
				if ( ConfigurationFile != value )
				{
					configFile = value;
					NotifyProjectOfChange();
				}
			}
		}

		public string ConfigurationFilePath
		{
			get
			{		
				return BasePath != null && ConfigurationFile != null
					? Path.Combine( BasePath, ConfigurationFile )
					: ConfigurationFile;
			}
		}

		private bool PrivateBinPathSpecified
		{
			get { return binPath != null; }
		}

		/// <summary>
		/// The Path.PathSeparator-separated path containing all the
		/// assemblies in the list.
		/// </summary>
		public string PrivateBinPath
		{
			get	{ return binPath; }
			set
			{
				if ( binPath != value )
				{
					binPath = value;
					binPathType = binPath == null ? BinPathType.Auto : BinPathType.Manual;
					NotifyProjectOfChange();
				}
			}
		}

		/// <summary>
		/// How our PrivateBinPath is generated
		/// </summary>
		public BinPathType BinPathType
		{
			get { return binPathType; }
			set 
			{
				if ( binPathType != value )
				{
					binPathType = value;
					NotifyProjectOfChange();
				}
			}
		}

		/// <summary>
		/// Return our AssemblyList
		/// </summary>
		public AssemblyList Assemblies
		{
			get { return assemblies; }
		}

        public RuntimeFramework RuntimeFramework
        {
            get { return runtimeFramework; }
            set 
			{
				if ( runtimeFramework != value )
				{
					runtimeFramework = value; 
					NotifyProjectOfChange();
				}
			}
        }
		#endregion

		public TestPackage MakeTestPackage()
		{
			TestPackage package = new TestPackage( project.ProjectPath );

			if ( !project.IsAssemblyWrapper )
				foreach ( string assembly in this.Assemblies )
					package.Assemblies.Add( assembly );

			if ( this.BasePathSpecified || this.PrivateBinPathSpecified || this.ConfigurationFileSpecified )
			{
				package.BasePath = this.BasePath;
				package.PrivateBinPath = this.PrivateBinPath;
				package.ConfigurationFile = this.ConfigurationFile;
			}

			package.AutoBinPath = this.BinPathType == BinPathType.Auto;
            if (this.RuntimeFramework != null)
                package.Settings["RuntimeFramework"] = this.RuntimeFramework;

            if (project.ProcessModel != ProcessModel.Default)
                package.Settings["ProcessModel"] = project.ProcessModel;

            if (project.DomainUsage != DomainUsage.Default)
                package.Settings["DomainUsage"] = project.DomainUsage;

			return package;
		}

		private void assemblies_Changed( object sender, EventArgs e )
		{
			NotifyProjectOfChange();
		}

        private void NotifyProjectOfChange()
        {
            if (project != null)
            {
                project.IsDirty = true;
                if (ReferenceEquals(this, project.ActiveConfig))
                    project.HasChangesRequiringReload = true;
            }
        }
	}
}
