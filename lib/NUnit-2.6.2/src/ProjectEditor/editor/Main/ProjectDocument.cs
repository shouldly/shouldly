// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NUnit.ProjectEditor
{
    public class ProjectDocument : IProjectDocument
    {
        private enum DocumentState
        {
            Empty,
            InvalidXml,
            Valid
        }

        #region Static Fields

        /// <summary>
        /// Used to generate default names for projects
        /// </summary>
        private static int projectSeed = 0;

        /// <summary>
        /// The extension used for test projects
        /// </summary>
        private static readonly string nunitExtension = ".nunit";

        #endregion

        #region Instance Fields

        /// <summary>
        /// The original text from which the doc was loaded.
        /// Updated from the doc when the xml view is displayed
        /// and from the view when the user edits it.
        /// </summary>
        string xmlText;

        /// <summary>
        /// The XmlDocument representing the loaded doc. It
        /// is generated from the text when the doc is loaded
        /// unless an exception is thrown. It is modified as the
        /// user makes changes.
        /// </summary>
        XmlDocument xmlDoc;

        /// <summary>
        /// An exception thrown when trying to build the xml
        /// document from the xml text.
        /// </summary>
        Exception exception;

        /// <summary>
        /// Path to the file storing this doc
        /// </summary>
        private string projectPath;

        /// <summry>
        /// True if the Xml Document has been changed
        /// </summary>
        private DocumentState documentState = DocumentState.Empty;

        /// <summary>
        /// True if the doc has been changed and not yet saved
        /// </summary>
        private bool hasUnsavedChanges = false;

        #endregion

        #region Constructors

        public ProjectDocument() : this(GenerateProjectName()) { }

        public ProjectDocument(string projectPath)
        {
            this.xmlDoc = new XmlDocument();
            this.projectPath = Path.GetFullPath(projectPath);

            xmlDoc.NodeChanged += new XmlNodeChangedEventHandler(xmlDoc_Changed);
            xmlDoc.NodeInserted += new XmlNodeChangedEventHandler(xmlDoc_Changed);
            xmlDoc.NodeRemoved += new XmlNodeChangedEventHandler(xmlDoc_Changed);
        }

        #endregion

        #region IProjectDocument Members

        #region Events

        public event ActionDelegate ProjectCreated;
        public event ActionDelegate ProjectClosed;
        public event ActionDelegate ProjectChanged;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the doc.
        /// </summary>
        public string Name
        {
            get { return Path.GetFileNameWithoutExtension(projectPath); }
        }

        /// <summary>
        /// Gets or sets the path to which a doc will be saved.
        /// </summary>
        public string ProjectPath
        {
            get { return projectPath; }
            set
            {
                string newProjectPath = Path.GetFullPath(value);
                if (newProjectPath != projectPath)
                {
                    projectPath = newProjectPath;
                }
            }
        }

        public string XmlText
        {
            get { return xmlText; }
            set { LoadXml(value); }
        }

        public Exception Exception
        {
            get { return exception; }
        }

        /// <summary>
        /// The top-level (NUnitProject) node
        /// </summary>
        public XmlNode RootNode
        {
            get { return xmlDoc.FirstChild; }
        }

        /// <summary>
        /// The Settings node if present, otherwise null
        /// </summary>
        public XmlNode SettingsNode
        {
            get { return RootNode.SelectSingleNode("Settings"); }
        }

        /// <summary>
        /// The collection of Config nodes - may be empty
        /// </summary>
        public XmlNodeList ConfigNodes
        {
            get { return RootNode.SelectNodes("Config"); }
        }

        public bool HasUnsavedChanges
        {
            get { return hasUnsavedChanges; }
        }

        public bool IsValid
        {
            get { return documentState == DocumentState.Valid; }
        }

        public bool IsEmpty
        {
            get { return documentState == DocumentState.Empty; }
        }

        #endregion

        #region Methods

        public void CreateNewProject()
        {
            this.XmlText = "<NUnitProject />";

            hasUnsavedChanges = false;

            if (ProjectCreated != null)
                ProjectCreated();
        }

        public void OpenProject(string fileName)
        {
            StreamReader rdr = new StreamReader(fileName);
            this.XmlText = rdr.ReadToEnd();
            rdr.Close();

            this.projectPath = Path.GetFullPath(fileName);

            if (ProjectCreated != null)
                ProjectCreated();

            hasUnsavedChanges = false;
        }

        public void CloseProject()
        {
            if (ProjectClosed != null)
                ProjectClosed();
        }

        public void SaveProject()
        {
            XmlTextWriter writer = new XmlTextWriter(
                ProjectPathFromFile(projectPath),
                System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;

            xmlDoc.WriteTo(writer);
            writer.Close();

            hasUnsavedChanges = false;
        }

        public void SaveProject(string fileName)
        {
            projectPath = fileName;
            SaveProject();
        }

        public string GetSettingsAttribute(string name)
        {
            if (SettingsNode == null)
                return null;

            return XmlHelper.GetAttribute(SettingsNode, name);
        }

        public void SetSettingsAttribute(string name, string value)
        {
            if (value == null)
                RemoveSettingsAttribute(name);
            else
            {
                if (SettingsNode == null)
                    XmlHelper.InsertElement(RootNode, "Settings", 0);

                XmlHelper.SetAttribute(SettingsNode, name, value);
            }
        }

        public void RemoveSettingsAttribute(string name)
        {
            if (SettingsNode != null)
                XmlHelper.RemoveAttribute(SettingsNode, name);
        }

        #region Load Methods

        public void Load()
        {
            StreamReader rdr = new StreamReader(this.projectPath);
            this.XmlText = rdr.ReadToEnd();
            rdr.Close();

            this.hasUnsavedChanges = false;
        }

        public void LoadXml(string xmlText)
        {
            // Mark as empty to avoid double updates
            // in the xmldoc_Changed method.
            this.documentState = DocumentState.Empty;

            this.xmlText = xmlText;

            try
            {
                this.xmlDoc.LoadXml(xmlText);
                this.documentState = DocumentState.Valid;
                this.exception = null;

                if (RootNode.Name != "NUnitProject")
                    throw new XmlException("Top level element must be <NUnitProject...>.");
            }
            catch (Exception ex)
            {
                this.documentState = DocumentState.InvalidXml;
                this.exception = ex;
            }
        }

        #endregion

        #region Save methods

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(ProjectPathFromFile(projectPath), false, System.Text.Encoding.UTF8))
            {
                writer.Write(xmlText);
            }

            hasUnsavedChanges = false;
        }

        public void Save(string fileName)
        {
            this.projectPath = Path.GetFullPath(fileName);
            Save();
        }

        private string ToXml()
        {
            StringWriter buffer = new StringWriter();

            using (XmlTextWriter writer = new XmlTextWriter(buffer))
            {
                writer.Formatting = Formatting.Indented;
                xmlDoc.WriteTo(writer);
            }

            return buffer.ToString();
        }

        #endregion

        #endregion

        #endregion

        #region Event Handlers

        void xmlDoc_Changed(object sender, XmlNodeChangedEventArgs e)
        {
            hasUnsavedChanges = true;

            if (this.IsValid)
                xmlText = this.ToXml();

            if (this.ProjectChanged != null)
                ProjectChanged();
        }

        #endregion

        #region Private Properties and Helper Methods

        private string DefaultBasePath
        {
            get { return Path.GetDirectoryName(projectPath); }
        }

        public static bool IsProjectFile(string path)
        {
            return Path.GetExtension(path) == nunitExtension;
        }

        private static string ProjectPathFromFile(string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(path) + nunitExtension;
            return Path.Combine(Path.GetDirectoryName(path), fileName);
        }

        private static string GenerateProjectName()
        {
            return string.Format("Project{0}", ++projectSeed);
        }

        #endregion
    }
}
