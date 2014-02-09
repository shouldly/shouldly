// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace NUnit.ProjectEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
            //MessageBox.Show("Attach to editor if desired", "Debug ProjectEditor?");
#endif

            // Set up main editor triad
            ProjectDocument doc = new ProjectDocument();
            MainForm view = new MainForm();
            MainPresenter presenter = new MainPresenter(doc, view);

            // TODO: Process arguments
            //    -new          = create new project
            //    -config=name  = create a new config (implies -new)
            //    assemblyName  = add assembly to the last config specified (or Default)

            if (args.Length == 1 && ProjectDocument.IsProjectFile(args[0]))
                doc.OpenProject(args[0]);
            else if (args.Length > 0)
            {
                doc.CreateNewProject();
                XmlNode configNode = XmlHelper.AddElement(doc.RootNode, "Config");
                XmlHelper.AddAttribute(configNode, "name", "Default");

                foreach (string fileName in args)
                {
                    if (PathUtils.IsAssemblyFileType(fileName))
                    {
                        XmlNode assemblyNode = XmlHelper.AddElement(configNode, "assembly");
                        XmlHelper.AddAttribute(assemblyNode, "path", fileName);
                    }
                }

                // Simulate view change so view gets updated
                presenter.ActiveViewChanged();
            }

            Application.Run(view);
        }
    }
}
