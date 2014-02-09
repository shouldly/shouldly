// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NUnit.ProjectEditor
{
    /// <summary>
    /// MainPresenter is the top-level presenter with subordinate 
    /// presenters for each view of the doc. It directly handles
    /// the menu commands from the top-level view and coordinates 
    /// changes in the two different submodels.
    /// </summary>
    public class MainPresenter
    {
        private IMainView view;
        private IProjectDocument doc;

        private PropertyPresenter propertyPresenter;
        private XmlPresenter xmlPresenter;

        #region Constructor

        public MainPresenter(IProjectDocument doc, IMainView view)
        {
            this.doc = doc;
            this.view = view;

            // Set up property editor triad
            ProjectModel project = new ProjectModel(doc);
            IPropertyView propertyView = view.PropertyView;
            this.propertyPresenter = new PropertyPresenter(project, propertyView);

            // Set up XML editor triad
            IXmlView xmlView = view.XmlView;
            this.xmlPresenter = new XmlPresenter(doc, xmlView);

            // Enable and disable menu items
            view.NewProjectCommand.Enabled = true;
            view.OpenProjectCommand.Enabled = true;
            view.CloseProjectCommand.Enabled = false;
            view.SaveProjectCommand.Enabled = false;
            view.SaveProjectAsCommand.Enabled = false;

            // Set up handlers for view events
            view.FormClosing += OnFormClosing;

            view.NewProjectCommand.Execute += CreateNewProject;
            view.OpenProjectCommand.Execute += OpenProject;
            view.SaveProjectCommand.Execute += SaveProject;
            view.SaveProjectAsCommand.Execute += SaveProjectAs;
            view.CloseProjectCommand.Execute += CloseProject;
            view.ActiveViewChanging += this.ValidateActiveViewChange;
            view.ActiveViewChanged += this.ActiveViewChanged;

            // Set up handlers for model events
            doc.ProjectCreated += OnProjectCreated;
            doc.ProjectClosed += OnProjectClosed;
        }

        public void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseProject();
        }

        public bool ValidateActiveViewChange()
        {
            if (doc.IsValid || doc.IsEmpty)
                return true;

            view.SaveProjectCommand.Enabled = false;
            view.SaveProjectAsCommand.Enabled = false;

            return view.SelectedView == SelectedView.XmlView;
        }

        public void ActiveViewChanged()
        {
            switch (view.SelectedView)
            {
                case SelectedView.PropertyView:
                    if (doc.RootNode != null)
                        propertyPresenter.LoadViewFromModel();
                    break;

                case SelectedView.XmlView:
                    xmlPresenter.LoadViewFromModel();
                    break;
            }
        }

        #endregion

        #region Command Event Handlers

        private void CreateNewProject()
        {
            doc.CreateNewProject();
        }

        private void OpenProject()
        {
            string path = view.DialogManager.GetFileOpenPath(
                "Open Project", 
                "Test Projects (*.nunit)|*.nunit",
                null);

            if (path != null)
            {
                try
                {
                    doc.OpenProject(path);
                }
                catch (Exception ex)
                {
                    view.MessageDisplay.Error(ex.Message);
                }
            }
        }

        private void CloseProject()
        {
            if (doc.IsValid && doc.HasUnsavedChanges &&
                view.MessageDisplay.AskYesNoQuestion(string.Format("Do you want to save changes to {0}?", doc.Name)))
                    SaveProject();

            doc.CloseProject();
        }

        private void SaveProject()
        {
            if (IsValidWritableProjectPath(doc.ProjectPath))
            {
                doc.SaveProject();
            }
            else
            {
                this.SaveProjectAs();
            }
        }

        private void SaveProjectAs()
        {
            string path = view.DialogManager.GetSaveAsPath(
                "Save As",
                "Test Projects (*.nunit)|*.nunit");

            if (path != null)
            {
                doc.SaveProject(path);
                view.PropertyView.ProjectPath.Text = doc.ProjectPath;
            }
        }

        #endregion

        #region Model EventHandlers

        private void OnProjectCreated()
        {
            view.CloseProjectCommand.Enabled = true;

            if (doc.IsValid)
            {
                view.SaveProjectCommand.Enabled = true;
                view.SaveProjectAsCommand.Enabled = true;
            }
            else
            {
                view.SaveProjectCommand.Enabled = false;
                view.SaveProjectAsCommand.Enabled = false;
                view.SelectedView = SelectedView.XmlView;
            }
        }

        private void OnProjectClosed()
        {
            view.CloseProjectCommand.Enabled = false;
            view.SaveProjectCommand.Enabled = false;
            view.SaveProjectAsCommand.Enabled = false;
        }

        #endregion

        #region Helper Methods

        private static bool IsValidWritableProjectPath(string path)
        {
            if (!Path.IsPathRooted(path))
                return false;

            if (!ProjectDocument.IsProjectFile(path))
                return false;

            if (!File.Exists(path))
                return true;

            return (File.GetAttributes(path) & FileAttributes.ReadOnly) == 0;
        }

        #endregion
    }
}
