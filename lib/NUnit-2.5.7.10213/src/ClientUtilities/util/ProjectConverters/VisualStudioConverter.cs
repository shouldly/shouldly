// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.IO;
using NUnit.Core;
using NUnit.Core.Extensibility;
using NUnit.Util.Extensibility;

namespace NUnit.Util.ProjectConverters
{
	/// <summary>
	/// Summary description for VSProjectLoader.
	/// </summary>
	public class VisualStudioConverter : IProjectConverter
	{
		#region IProjectConverter Members

		public bool CanConvertFrom(string path)
		{
			return VSProject.IsProjectFile(path)|| VSProject.IsSolutionFile(path);
		}

		public NUnitProject ConvertFrom(string path)
		{
			NUnitProject project = new NUnitProject( Path.GetFullPath( path ) );

			if ( VSProject.IsProjectFile(path) )
			{
				VSProject vsProject = new VSProject( path );
				project.Add( vsProject );
			}
			else if ( VSProject.IsSolutionFile(path) )
			{
				string solutionDirectory = Path.GetDirectoryName( path );
				using(StreamReader reader = new StreamReader( path ))
				{
					char[] delims = { '=', ',' };
					char[] trimchars = { ' ', '"' };

					string line = reader.ReadLine();
					while ( line != null )
					{
						if ( line.StartsWith( "Project" ) )
						{
							string[] parts = line.Split( delims );
							string vsProjectPath = parts[2].Trim(trimchars);
						
							if ( VSProject.IsProjectFile( vsProjectPath ) )
								project.Add( new VSProject( Path.Combine( solutionDirectory, vsProjectPath ) ) );
						}

						line = reader.ReadLine();
					}
				}
			}

			project.IsDirty = false;

			return project;
		}

		#endregion
	}
}
