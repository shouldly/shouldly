// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Threading;
using NUnit.Core;

namespace NUnit.Util
{
	/// <summary>
	/// Class that represents an NUnit test project
	/// </summary>
	public class NUnitProject
    {
        #region Constants
        public static readonly string Extension = ".nunit";
        #endregion

        #region Instance variables

        /// <summary>
		/// Path to the file storing this project
		/// </summary>
		private string projectPath;

		/// <summary>
		/// Application Base for the project. Since this
		/// can be null, always fetch from the property
		/// rather than using the field directly.
		/// </summary>
		private string basePath;

		/// <summary>
		///  Whether the project is dirty
		/// </summary>
		private bool isDirty = false;

        /// <summary>
        /// Whether canges have been made requiring a reload
        /// </summary>
        private bool reloadRequired = false;
		
		/// <summary>
		/// Collection of configs for the project
		/// </summary>
		private ProjectConfigCollection configs;

        /// <summary>
        /// True for NUnit-related projects that follow the config
        /// of the NUnit build under which they are running.
        /// </summary>
        private bool autoConfig;

		/// <summary>
		/// The currently active configuration
		/// </summary>
		private ProjectConfig activeConfig;

		/// <summary>
		/// Flag indicating that this project is a
		/// temporary wrapper for an assembly.
		/// </summary>
		private bool isAssemblyWrapper = false;

        /// <summary>
        /// The ProcessModel to be used in loading this project
        /// </summary>
        private ProcessModel processModel;

        /// <summary>
        /// The DomainUsage setting to be used in loading this project
        /// </summary>
        private DomainUsage domainUsage;

		#endregion

		#region Constructor

		public NUnitProject( string projectPath )
		{
			this.projectPath = Path.GetFullPath( projectPath );
			configs = new ProjectConfigCollection( this );
		}

		#endregion

		#region Properties and Events

		/// <summary>
		/// The path to which a project will be saved.
		/// </summary>
		public string ProjectPath
		{
			get { return projectPath; }
			set 
			{
				projectPath = Path.GetFullPath( value );
				isDirty = true;
			}
		}

		public string DefaultBasePath
		{
			get { return Path.GetDirectoryName( projectPath ); }
		}

		/// <summary>
		/// Indicates whether a base path was specified for the project
		/// </summary>
		public bool BasePathSpecified
		{
			get
			{
				return basePath != null && basePath != string.Empty;
			}
		}

		/// <summary>
		/// The base path for the project. Constructor sets
		/// it to the directory part of the project path.
		/// </summary>
		public string BasePath
		{
			get 
			{ 
				if ( !BasePathSpecified )
					return DefaultBasePath; 
				return basePath;
			}
            set
            {
                basePath = value;

                if (basePath != null && basePath != string.Empty
                    && !Path.IsPathRooted(basePath))
                {
                    basePath = Path.Combine(
                        DefaultBasePath,
                        basePath);
                }

                basePath = PathUtils.Canonicalize(basePath);
                HasChangesRequiringReload = IsDirty = true;
            }
		}

		/// <summary>
		/// The name of the project.
		/// </summary>
		public string Name
		{
			get { return Path.GetFileNameWithoutExtension( projectPath ); }
		}

        public bool AutoConfig
        {
            get { return autoConfig; }
            set { autoConfig = value; }
        }

		public ProjectConfig ActiveConfig
		{
			get 
			{ 
				// In case the previous active config was removed
				if ( activeConfig != null && !configs.Contains( activeConfig ) )
					activeConfig = null;
				
				// In case no active config is set or it was removed
                if (activeConfig == null && configs.Count > 0)
                    activeConfig = configs[0];
				
				return activeConfig; 
			}
		}

		// Safe access to name of the active config
		public string ActiveConfigName
		{
			get
			{
				ProjectConfig config = ActiveConfig;
				return config == null ? null : config.Name;
			}
		}

		public bool IsLoadable
		{
			get
			{
				return	ActiveConfig != null &&
					ActiveConfig.Assemblies.Count > 0;
			}
		}

		// A project made from a single assembly is treated
		// as a transparent wrapper for some purposes until
		// a change is made to it.
		public bool IsAssemblyWrapper
		{
			get { return isAssemblyWrapper; }
			set { isAssemblyWrapper = value; }
		}

		public string ConfigurationFile
		{
			get 
			{ 
				// TODO: Check this
				return isAssemblyWrapper
					  ? Path.GetFileName( projectPath ) + ".config"
					  : Path.GetFileNameWithoutExtension( projectPath ) + ".config";
			}
		}

		public bool IsDirty
		{
			get { return isDirty; }
			set 
            { 
                isDirty = value;

                if (isAssemblyWrapper && value == true)
                {
                    projectPath = Path.ChangeExtension(projectPath, ".nunit");
                    isAssemblyWrapper = false;
                    HasChangesRequiringReload = true;
                }
            }
		}

        public bool HasChangesRequiringReload
        {
            get { return reloadRequired; }
            set { reloadRequired = value; }
        }

        public ProcessModel ProcessModel
        {
            get { return processModel; }
            set
            {
                processModel = value;
                HasChangesRequiringReload = IsDirty = true;
            }
        }

        public DomainUsage DomainUsage
        {
            get { return domainUsage; }
            set
            {
                domainUsage = value;
                HasChangesRequiringReload = IsDirty = true;
            }
        }

		public ProjectConfigCollection Configs
		{
			get { return configs; }
		}
		#endregion

        #region Static Methods
        public static bool IsNUnitProjectFile(string path)
        {
            return Path.GetExtension(path) == Extension;
        }

        public static string ProjectPathFromFile(string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(path) + NUnitProject.Extension;
            return Path.Combine(Path.GetDirectoryName(path), fileName);
        }
        #endregion

        #region Instance Methods

        public void SetActiveConfig( int index )
		{
			activeConfig = configs[index];
            HasChangesRequiringReload = IsDirty = true;
		}

		public void SetActiveConfig( string name )
		{
			foreach( ProjectConfig config in configs )
			{
				if ( config.Name == name )
				{
					activeConfig = config;
                    HasChangesRequiringReload = IsDirty = true;
                    break;
				}
			}
		}

		public void Add( VSProject vsProject )
		{
			foreach( VSProjectConfig vsConfig in vsProject.Configs )
			{
				string name = vsConfig.Name;

				if ( !configs.Contains( name ) )
					configs.Add( name );

				ProjectConfig config = this.Configs[name];

				foreach ( string assembly in vsConfig.Assemblies )
					config.Assemblies.Add( assembly );
			}
		}

		public void Load()
		{
			XmlTextReader reader = new XmlTextReader( projectPath );

			string activeConfigName = null;
			ProjectConfig currentConfig = null;
			
			try
			{
				reader.MoveToContent();
				if ( reader.NodeType != XmlNodeType.Element || reader.Name != "NUnitProject" )
					throw new ProjectFormatException( 
						"Invalid project format: <NUnitProject> expected.", 
						reader.LineNumber, reader.LinePosition );

				while( reader.Read() )
					if ( reader.NodeType == XmlNodeType.Element )
						switch( reader.Name )
						{
							case "Settings":
								if ( reader.NodeType == XmlNodeType.Element )
								{
									activeConfigName = reader.GetAttribute( "activeconfig" );

                                    string autoConfig = reader.GetAttribute("autoconfig");
                                    if (autoConfig != null)
                                        this.AutoConfig = autoConfig.ToLower() == "true";
                                    if (this.AutoConfig)
										activeConfigName = NUnitConfiguration.BuildConfiguration;
									
                                    string appbase = reader.GetAttribute( "appbase" );
									if ( appbase != null )
										this.BasePath = appbase;

                                    string processModel = reader.GetAttribute("processModel");
                                    if (processModel != null)
                                        this.ProcessModel = (ProcessModel)Enum.Parse(typeof(ProcessModel), processModel);

                                    string domainUsage = reader.GetAttribute("domainUsage");
                                    if (domainUsage != null)
                                        this.DomainUsage = (DomainUsage)Enum.Parse(typeof(DomainUsage), domainUsage);
                                }
								break;

							case "Config":
								if ( reader.NodeType == XmlNodeType.Element )
								{
									string configName = reader.GetAttribute( "name" );
									currentConfig = new ProjectConfig( configName );
									currentConfig.BasePath = reader.GetAttribute( "appbase" );
									currentConfig.ConfigurationFile = reader.GetAttribute( "configfile" );

									string binpath = reader.GetAttribute( "binpath" );
									currentConfig.PrivateBinPath = binpath;
									string type = reader.GetAttribute( "binpathtype" );
									if ( type == null )
										if ( binpath == null )
											currentConfig.BinPathType = BinPathType.Auto;
										else
											currentConfig.BinPathType = BinPathType.Manual;
									else
										currentConfig.BinPathType = (BinPathType)Enum.Parse( typeof( BinPathType ), type, true );

                                    string runtime = reader.GetAttribute("runtimeFramework");
                                    if ( runtime != null )
                                        currentConfig.RuntimeFramework = RuntimeFramework.Parse(runtime);

                                    Configs.Add(currentConfig);
									if ( configName == activeConfigName )
										activeConfig = currentConfig;
								}
								else if ( reader.NodeType == XmlNodeType.EndElement )
									currentConfig = null;
								break;

							case "assembly":
								if ( reader.NodeType == XmlNodeType.Element && currentConfig != null )
								{
									string path = reader.GetAttribute( "path" );
									currentConfig.Assemblies.Add( 
										Path.Combine( currentConfig.BasePath, path ) );
								}
								break;

							default:
								break;
						}

				this.IsDirty = false;
                this.reloadRequired = false;
			}
			catch( FileNotFoundException )
			{
				throw;
			}
			catch( XmlException e )
			{
				throw new ProjectFormatException(
					string.Format( "Invalid project format: {0}", e.Message ),
					e.LineNumber, e.LinePosition );
			}
			catch( Exception e )
			{
				throw new ProjectFormatException( 
					string.Format( "Invalid project format: {0} Line {1}, Position {2}", 
					e.Message, reader.LineNumber, reader.LinePosition ),
					reader.LineNumber, reader.LinePosition );
			}
			finally
			{
				reader.Close();
			}
		}

		public void Save()
		{
			projectPath = ProjectPathFromFile( projectPath );

			XmlTextWriter writer = new XmlTextWriter(  projectPath, System.Text.Encoding.UTF8 );
			writer.Formatting = Formatting.Indented;

			writer.WriteStartElement( "NUnitProject" );
			
			if ( configs.Count > 0 || this.BasePath != this.DefaultBasePath )
			{
				writer.WriteStartElement( "Settings" );
				if ( configs.Count > 0 )
					writer.WriteAttributeString( "activeconfig", ActiveConfigName );
				if ( this.BasePath != this.DefaultBasePath )
					writer.WriteAttributeString( "appbase", this.BasePath );
                if (this.AutoConfig)
                    writer.WriteAttributeString("autoconfig", "true");
                if (this.ProcessModel != ProcessModel.Default)
                    writer.WriteAttributeString("processModel", this.ProcessModel.ToString());
                if (this.DomainUsage != DomainUsage.Default)
                    writer.WriteAttributeString("domainUsage", this.DomainUsage.ToString());
				writer.WriteEndElement();
			}
			
			foreach( ProjectConfig config in Configs )
			{
				writer.WriteStartElement( "Config" );
				writer.WriteAttributeString( "name", config.Name );
				string appbase = config.BasePath;
				if ( !PathUtils.SamePathOrUnder( this.BasePath, appbase ) )
					writer.WriteAttributeString( "appbase", appbase );
				else if ( config.RelativeBasePath != null )
					writer.WriteAttributeString( "appbase", config.RelativeBasePath );
				
				string configFile = config.ConfigurationFile;
				if ( configFile != null && configFile != this.ConfigurationFile )
					writer.WriteAttributeString( "configfile", config.ConfigurationFile );
				
				if ( config.BinPathType == BinPathType.Manual )
					writer.WriteAttributeString( "binpath", config.PrivateBinPath );
				else
					writer.WriteAttributeString( "binpathtype", config.BinPathType.ToString() );

                if (config.RuntimeFramework != null)
                    writer.WriteAttributeString("runtimeFramework", config.RuntimeFramework.ToString());

				foreach( string assembly in config.Assemblies )
				{
					writer.WriteStartElement( "assembly" );
					writer.WriteAttributeString( "path", PathUtils.RelativePath( config.BasePath, assembly ) );
					writer.WriteEndElement();
				}

				writer.WriteEndElement();
			}

			writer.WriteEndElement();

			writer.Close();
			this.IsDirty = false;

			// Once we save a project, it's no longer
			// loaded as an assembly wrapper on reload.
			this.isAssemblyWrapper = false;
		}

		public void Save( string projectPath )
		{
			this.ProjectPath = projectPath;
			Save();
		}
		#endregion
	}
}
