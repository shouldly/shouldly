// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;
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
			if ( VSProject.IsProjectFile(path) )
			{
                return ConvertVSProject(path);
			}
			else if ( VSProject.IsSolutionFile(path) )
			{
                return Services.UserSettings.GetSetting("Options.TestLoader.VisualStudio.UseSolutionConfigs", true)
                    ? ConvertVSSolution(path)
                    : LegacyConvertVSSolution(path);
			}

			return null;
		}

        private static NUnitProject ConvertVSProject(string path)
        {
            NUnitProject project = new NUnitProject(Path.GetFullPath(path));
            project.Add(new VSProject(path));
			project.IsDirty = false;
            return project;
        }

        private static NUnitProject ConvertVSSolution(string path)
        {
            NUnitProject project = new NUnitProject(Path.GetFullPath(path));

            string solutionDirectory = Path.GetDirectoryName(path);
            using (StreamReader reader = new StreamReader(path))
            {
                char[] delims = { '=', ',' };
                char[] trimchars = { ' ', '"' };
                string buildMarker = ".Build.0 =";

                Hashtable projectLookup = new Hashtable();

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line.StartsWith("Project("))
                    {
                        string[] parts = line.Split(delims);
                        string vsProjectPath = parts[2].Trim(trimchars);
                        string vsProjectGuid = parts[3].Trim(trimchars);

                        if (VSProject.IsProjectFile(vsProjectPath))
                            projectLookup[vsProjectGuid] = new VSProject(Path.Combine(solutionDirectory, vsProjectPath));
                    }
                    else if (line.IndexOf(buildMarker) >= 0)
                    {
                        line = line.Trim();
                        int endBrace = line.IndexOf('}');

                        string vsProjectGuid = line.Substring(0, endBrace + 1);
                        VSProject vsProject = projectLookup[vsProjectGuid] as VSProject;

                        if (vsProject != null)
                        {
                            line = line.Substring(endBrace + 2);

                            int split = line.IndexOf(buildMarker) + 1;
                            string solutionConfig = line.Substring(0, split - 1);
                            int bar = solutionConfig.IndexOf('|');
                            if (bar >= 0)
                                solutionConfig = solutionConfig.Substring(0, bar);

                            string projectConfig = line.Substring(split + buildMarker.Length);
                            if (vsProject.Configs[projectConfig] == null)
                            {
                                bar = projectConfig.IndexOf('|');
                                if (bar >= 0)
                                    projectConfig = projectConfig.Substring(0, bar);
                            }

                            if (project.Configs[solutionConfig] == null)
                                project.Configs.Add(new ProjectConfig(solutionConfig));

                            foreach (string assembly in vsProject.Configs[projectConfig].Assemblies)
                                if (!project.Configs[solutionConfig].Assemblies.Contains(assembly))
                                    project.Configs[solutionConfig].Assemblies.Add(assembly);

                            //if (VSProject.IsProjectFile(vsProjectPath))
                            //    project.Add(new VSProject(Path.Combine(solutionDirectory, vsProjectPath)));
                        }
                    }

                    line = reader.ReadLine();
                }

                project.IsDirty = false;
                return project;
            }
        }

        private static NUnitProject LegacyConvertVSSolution(string path)
        {
            NUnitProject project = new NUnitProject(Path.GetFullPath(path));

            string solutionDirectory = Path.GetDirectoryName(path);
            using (StreamReader reader = new StreamReader(path))
            {
                char[] delims = { '=', ',' };
                char[] trimchars = { ' ', '"' };

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line.StartsWith("Project("))
                    {
                        string[] parts = line.Split(delims);
                        string vsProjectPath = parts[2].Trim(trimchars);

                        if (VSProject.IsProjectFile(vsProjectPath))
                            project.Add(new VSProject(Path.Combine(solutionDirectory, vsProjectPath)));
                    }

                    line = reader.ReadLine();
                }

                project.IsDirty = false;
                return project;
            }
        }

		#endregion
	}
}
