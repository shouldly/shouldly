// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.IO;
using NUnit.Core;
using NUnit.Util.Extensibility;
using NUnit.Util.ProjectConverters;

namespace NUnit.Util
{
	/// <summary>
	/// Summary description for ProjectService.
	/// </summary>
	public class ProjectService : IProjectConverter, IService
	{
		/// <summary>
		/// Seed used to generate names for new projects
		/// </summary>
		private int projectSeed = 0;

		/// <summary>
		/// The extension used for test projects
		/// </summary>
        //private static readonly string nunitExtension = ".nunit";

		/// <summary>
		/// Array of all installed ProjectConverters
		/// </summary>
		IProjectConverter[] converters = new IProjectConverter[] 
		{
			new VisualStudioConverter()
		};

		#region Instance Methods
		public bool CanLoadProject(string path)
		{
			return NUnitProject.IsNUnitProjectFile(path) || CanConvertFrom(path);
		}

		public NUnitProject LoadProject(string path)
		{
			if ( NUnitProject.IsNUnitProjectFile(path) )
			{
				NUnitProject project = new NUnitProject( path );
				project.Load();
				return project;
			}

			return ConvertFrom(path);
		}

		/// <summary>
		/// Creates a project to wrap a list of assemblies
		/// </summary>
		public NUnitProject WrapAssemblies( string[] assemblies )
		{
			// if only one assembly is passed in then the configuration file
			// should follow the name of the assembly. This will only happen
			// if the LoadAssembly method is called. Currently the console ui
			// does not differentiate between having one or multiple assemblies
			// passed in.
			if ( assemblies.Length == 1)
				return WrapAssembly(assemblies[0]);


			NUnitProject project = Services.ProjectService.EmptyProject();
			ProjectConfig config = new ProjectConfig( "Default" );
			foreach( string assembly in assemblies )
			{
				string fullPath = Path.GetFullPath( assembly );

				if ( !File.Exists( fullPath ) )
					throw new FileNotFoundException( string.Format( "Assembly not found: {0}", fullPath ) );
				
				config.Assemblies.Add( fullPath );
			}

			project.Configs.Add( config );

			// TODO: Deduce application base, and provide a
			// better value for loadpath and project path
			// analagous to how new projects are handled
			string basePath = Path.GetDirectoryName( Path.GetFullPath( assemblies[0] ) );
			project.ProjectPath = Path.Combine( basePath, project.Name + ".nunit" );

			project.IsDirty = true;

			return project;
		}

		/// <summary>
		/// Creates a project to wrap an assembly
		/// </summary>
		public NUnitProject WrapAssembly( string assemblyPath )
		{
			if ( !File.Exists( assemblyPath ) )
				throw new FileNotFoundException( string.Format( "Assembly not found: {0}", assemblyPath ) );

			string fullPath = Path.GetFullPath( assemblyPath );

			NUnitProject project = new NUnitProject( fullPath );
			
			ProjectConfig config = new ProjectConfig( "Default" );
			config.Assemblies.Add( fullPath );
			project.Configs.Add( config );

			project.IsAssemblyWrapper = true;
			project.IsDirty = false;

			return project;
		}

		public string GenerateProjectName()
		{
			return string.Format( "Project{0}", ++projectSeed );
		}

		public NUnitProject EmptyProject()
		{
			return new NUnitProject( GenerateProjectName() );
		}

		public NUnitProject NewProject()
		{
			NUnitProject project = EmptyProject();

			project.Configs.Add( "Debug" );
			project.Configs.Add( "Release" );
			project.IsDirty = false;

			return project;
		}

		public void SaveProject( NUnitProject project )
		{
			project.Save();
		}
		#endregion

		#region IProjectConverter Members
		public bool CanConvertFrom(string path)
		{
			foreach( IProjectConverter converter in converters )
				if ( converter.CanConvertFrom(path) )
					return true;

			return false;
		}

		public NUnitProject ConvertFrom(string path)
		{
			foreach( IProjectConverter converter in converters )
			{
				if ( converter.CanConvertFrom( path ) )
					return converter.ConvertFrom( path );
			}

			return WrapAssembly(path);
		}
		#endregion

		#region IService Members
		public void InitializeService()
		{
			// TODO:  Add ProjectLoader.InitializeService implementation
		}

		public void UnloadService()
		{
			// TODO:  Add ProjectLoader.UnloadService implementation
		}
		#endregion
	}
}
