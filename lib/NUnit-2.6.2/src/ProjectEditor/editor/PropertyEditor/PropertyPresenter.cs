// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace NUnit.ProjectEditor
{
    /// <summary>
    /// The ProjectPresenter handles presentation of the doc as
    /// a set of properties, which the ProjectView is expected to
    /// display.
    /// </summary>
    public class PropertyPresenter
    {
        private IProjectModel model;
        private IProjectConfig selectedConfig;
        private IPropertyView view;

        public PropertyPresenter(IProjectModel model, IPropertyView view)
        {
            this.model = model;
            this.view = view;

            view.ProcessModel.SelectionList = new string[] { "Default", "Single", "Separate", "Multiple" };
            view.DomainUsage.SelectionList = new string[] { "Default", "Single", "Multiple" };
            view.Runtime.SelectionList = new string[] { "Any", "Net", "Mono" };
            view.RuntimeVersion.SelectionList = new string[] { "1.0.3705", "1.1.4322", "2.0.50727", "4.0.21006" };

            view.BrowseProjectBaseCommand.Execute += BrowseForProjectBase;
            view.BrowseConfigBaseCommand.Execute += BrowseForConfigBase;
            view.EditConfigsCommand.Execute += EditConfigs;

            view.AddAssemblyCommand.Execute += AddAssembly;
            view.RemoveAssemblyCommand.Execute += RemoveAssembly;
            view.BrowseAssemblyPathCommand.Execute += BrowseForAssemblyPath;

            view.ProjectBase.Validated += OnProjectBaseChange;
            view.ProcessModel.SelectionChanged += OnProcessModelChange;
            view.DomainUsage.SelectionChanged += OnDomainUsageChange;
            view.ConfigList.SelectionChanged += OnSelectedConfigChange;

            view.Runtime.SelectionChanged += OnRuntimeChange;
            view.RuntimeVersion.TextValidated += OnRuntimeVersionChange;
            view.ApplicationBase.Validated += OnApplicationBaseChange;
            view.ConfigurationFile.Validated += OnConfigurationFileChange;
            view.BinPathType.SelectionChanged += OnBinPathTypeChange;
            view.PrivateBinPath.Validated += OnPrivateBinPathChange;
            view.AssemblyList.SelectionChanged += OnSelectedAssemblyChange;
            view.AssemblyPath.Validated += OnAssemblyPathChange;

            model.Document.ProjectCreated += OnProjectCreated;
            model.Document.ProjectClosed += OnProjectClosed;
        }

        public void LoadViewFromModel()
        {
            view.Visible = true;

            view.ProjectPath.Text = model.ProjectPath;
            view.ProjectBase.Text = model.EffectiveBasePath;
            view.ActiveConfigName.Text = model.ActiveConfigName;

            view.ProcessModel.SelectedItem = model.ProcessModel;
            view.DomainUsage.SelectedItem = model.DomainUsage;

            view.ConfigList.SelectionList = model.ConfigNames;
            if (model.ConfigNames.Length > 0)
            {
                view.ConfigList.SelectedIndex = 0;
                selectedConfig = model.Configs[0];
            }
            else
            {
                view.ConfigList.SelectedIndex = -1;
                selectedConfig = null;
            }

            //OnSelectedConfigChange();
        }

        #region Command Events

        private void BrowseForProjectBase()
        {
            string message = "Select ApplicationBase for the model as a whole.";
            string projectBase = view.DialogManager.GetFolderPath(message, view.ProjectBase.Text);
            if (projectBase != null && projectBase != model.BasePath)
                view.ProjectBase.Text = model.BasePath = projectBase;
        }

        private void BrowseForConfigBase()
        {
            string message = string.Format(
                "Select ApplicationBase for the {0} configuration, if different from the model as a whole.",
                model.Configs[view.ConfigList.SelectedIndex].Name);
            string initialFolder = view.ApplicationBase.Text;
            if (initialFolder == string.Empty)
                initialFolder = view.ProjectBase.Text;

            string appbase = view.DialogManager.GetFolderPath(message, initialFolder);
            if (appbase != null && appbase != view.ApplicationBase.Text)
                UpdateApplicationBase(appbase);
        }

        private void EditConfigs()
        {
            IConfigurationEditorDialog editorView = view.ConfigurationEditorDialog;
            new ConfigurationEditor(model, editorView);
            editorView.ShowDialog();

            string selectedConfig = view.ConfigList.SelectedItem;
            string[] configs = model.ConfigNames;

            view.ConfigList.SelectionList = configs;

            if (configs.Length > 0)
            {
                view.ConfigList.SelectedIndex = 0;
                foreach (string config in configs)
                {
                    if (config == selectedConfig)
                        view.ConfigList.SelectedItem = config;
                }
            }

            view.ActiveConfigName.Text = model.ActiveConfigName;
        }

        private void AddAssembly()
        {
            string assemblyPath = view.DialogManager.GetFileOpenPath(
                "Select Assembly",
                "Assemblies (*.dll,*.exe)|*.dll;*.exe|All Files (*.*)|*.*",
                view.AssemblyPath.Text);

            if (assemblyPath != null)
            {
                assemblyPath = PathUtils.RelativePath(selectedConfig.EffectiveBasePath, assemblyPath);
                selectedConfig.Assemblies.Add(assemblyPath);
                SetAssemblyList();
            }
        }

        private void RemoveAssembly()
        {
            string question = string.Format("Remove {0} from project?", view.AssemblyList.SelectedItem);
            if (view.MessageDisplay.AskYesNoQuestion(question))
            {
                selectedConfig.Assemblies.Remove(view.AssemblyList.SelectedItem);
                SetAssemblyList();
            }
        }

        private void BrowseForAssemblyPath()
        {
            string assemblyPath = view.DialogManager.GetFileOpenPath(
                "Select Assembly",
                "Assemblies (*.dll,*.exe)|*.dll;*.exe|All Files (*.*)|*.*",
                view.AssemblyPath.Text);

            if (assemblyPath != null)
            {
                selectedConfig.Assemblies[view.AssemblyList.SelectedIndex] = assemblyPath;
                SetAssemblyList();
            }
        }

        #endregion

        #region View Change Events

        private void OnProjectBaseChange()
        {
            string projectBase = view.ProjectBase.Text;

            if (projectBase == string.Empty)
                view.ProjectBase.Text = projectBase = Path.GetDirectoryName(model.ProjectPath);

            if (ValidateDirectoryPath("ProjectBase", projectBase))
                model.BasePath = projectBase;
        }

        private void OnProcessModelChange()
        {
            model.ProcessModel = view.ProcessModel.SelectedItem;
            view.DomainUsage.SelectionList = view.ProcessModel.SelectedItem == "Multiple"
                ? new string[] { "Default", "Single" }
                : new string[] { "Default", "Single", "Multiple" };
        }

        private void OnDomainUsageChange()
        {
            model.DomainUsage = view.DomainUsage.SelectedItem;
        }

        private void OnSelectedConfigChange()
        {
            IProjectConfig selectedConfig = view.ConfigList.SelectedIndex >= 0
                ? model.Configs[view.ConfigList.SelectedIndex]
                : null;

            if (selectedConfig != null)
            {
                RuntimeFramework framework = selectedConfig.RuntimeFramework;
                view.Runtime.SelectedItem = framework.Runtime.ToString();
                view.RuntimeVersion.Text = framework.Version == new Version()
                    ? string.Empty
                    : framework.Version.ToString();

                view.ApplicationBase.Text = selectedConfig.RelativeBasePath;
                view.ConfigurationFile.Text = selectedConfig.ConfigurationFile;
                view.BinPathType.SelectedIndex = (int)selectedConfig.BinPathType;
                if (selectedConfig.BinPathType == BinPathType.Manual)
                    view.PrivateBinPath.Text = selectedConfig.PrivateBinPath;
                else
                    view.PrivateBinPath.Text = string.Empty;

                SetAssemblyList();
            }
            else
            {
                view.Runtime.SelectedItem = "Any";
                view.RuntimeVersion.Text = string.Empty;

                view.ApplicationBase.Text = null;
                view.ConfigurationFile.Text = string.Empty;
                view.PrivateBinPath.Text = string.Empty;
                view.BinPathType.SelectedIndex = (int)BinPathType.Auto;

                view.AssemblyList.SelectionList = new string[0];
                view.AssemblyPath.Text = string.Empty;
            }
        }

        #region Changes Pertaining to Selected Config

        private void OnRuntimeChange()
        {
            try
            {
                if (selectedConfig != null)
                    selectedConfig.RuntimeFramework = new RuntimeFramework(
                        (RuntimeType)Enum.Parse(typeof(RuntimeType), view.Runtime.SelectedItem),
                        selectedConfig.RuntimeFramework.Version);
            }
            catch(Exception ex)
            {
                // Note: Should not be called with an invalid value,
                // but we catch and report the error in any case
                view.MessageDisplay.Error("Invalid Runtime: " + ex.Message);
            }
        }

        private void OnRuntimeVersionChange()
        {
            if (selectedConfig != null)
            {
                try
                {
                    Version version = string.IsNullOrEmpty(view.RuntimeVersion.Text)
                        ? new Version()
                        : new Version(view.RuntimeVersion.Text);
                    selectedConfig.RuntimeFramework = new RuntimeFramework(
                        selectedConfig.RuntimeFramework.Runtime,
                        version);
                }
                catch (Exception ex)
                {
                    // User entered an bad value for the version
                    view.MessageDisplay.Error("Invalid RuntimeVersion: " + ex.Message);
                }
            }
        }

        private void OnApplicationBaseChange()
        {
            if (selectedConfig != null)
            {
                string basePath = null;

                if (view.ApplicationBase.Text != String.Empty)
                {
                    if (!ValidateDirectoryPath("ApplicationBase", view.ApplicationBase.Text))
                        return;

                    basePath = Path.Combine(model.EffectiveBasePath, view.ApplicationBase.Text);
                    if (PathUtils.SamePath(model.EffectiveBasePath, basePath))
                        basePath = null;
                }

                selectedConfig.BasePath = basePath;

                // TODO: Test what happens if we set it the same as doc base
                //if (index.RelativeBasePath == null)
                //    view.ApplicationBase.Text = string.Empty;
                //else
                //    view.ApplicationBase.Text = index.RelativeBasePath;
            }
        }

        private void OnConfigurationFileChange()
        {
            if (selectedConfig != null)
            {
                string configFile = view.ConfigurationFile.Text;

                if (configFile == string.Empty)
                    selectedConfig.ConfigurationFile = null;
                else if (ValidateFilePath("DefaultConfigurationFile", configFile))
                    selectedConfig.ConfigurationFile = view.ConfigurationFile.Text;
            }
        }

        private void OnBinPathTypeChange()
        {
            if (selectedConfig != null)
                selectedConfig.BinPathType = (BinPathType)view.BinPathType.SelectedIndex;
            view.PrivateBinPath.Enabled = view.BinPathType.SelectedIndex == (int)BinPathType.Manual;
        }

        private void OnPrivateBinPathChange()
        {
            if (selectedConfig != null)
            {
                if (view.PrivateBinPath.Text == string.Empty)
                    selectedConfig.PrivateBinPath = null;
                else
                {
                    foreach (string dir in view.PrivateBinPath.Text.Split(Path.PathSeparator))
                    {
                        if (!ValidateDirectoryPath("PrivateBinPath", dir))
                            return;
                        if (Path.IsPathRooted(dir))
                        {
                            view.MessageDisplay.Error("Path " + dir + " is an absolute path. PrivateBinPath components must all be relative paths.");
                            return;
                        }
                    }

                    selectedConfig.PrivateBinPath = view.PrivateBinPath.Text;
                }
            }
        }

        private void OnSelectedAssemblyChange()
        {
            if (view.AssemblyList.SelectedIndex == -1)
            {
                view.AssemblyPath.Text = null;
                view.AddAssemblyCommand.Enabled = true;
                view.RemoveAssemblyCommand.Enabled = false;
                view.BrowseAssemblyPathCommand.Enabled = false;
            }
            else if (selectedConfig != null)
            {
                view.AssemblyPath.Text =
                    selectedConfig.Assemblies[view.AssemblyList.SelectedIndex];
                view.AddAssemblyCommand.Enabled = true;
                view.RemoveAssemblyCommand.Enabled = true;
                view.BrowseAssemblyPathCommand.Enabled = true;
            }
        }

        private void OnAssemblyPathChange()
        {
            if (selectedConfig != null && ValidateFilePath("AssemblyPath", view.AssemblyPath.Text))
            {
                selectedConfig.Assemblies[view.AssemblyList.SelectedIndex] = view.AssemblyPath.Text;
                SetAssemblyList();
            }
        }

        #endregion

        #endregion

        #region Model Change Events

        private void OnProjectCreated()
        {
            view.Visible = true;
            if (model.Document.RootNode != null)
                LoadViewFromModel();
        }

        private void OnProjectClosed()
        {
            view.Visible = false;
        }

        #endregion

        #region Helper Methods

        private void UpdateApplicationBase(string appbase)
        {
            string basePath = null;

            if (appbase != String.Empty)
            {
                basePath = Path.Combine(model.BasePath, appbase);
                if (PathUtils.SamePath(model.BasePath, basePath))
                    basePath = null;
            }

            IProjectConfig selectedConfig = model.Configs[view.ConfigList.SelectedIndex];
            view.ApplicationBase.Text = selectedConfig.BasePath = basePath;

            // TODO: Test what happens if we set it the same as doc base
            //if (index.RelativeBasePath == null)
            //    applicationBaseTextBox.Text = string.Empty;
            //else
            //    applicationBaseTextBox.Text = index.RelativeBasePath;
        }

        private void SetAssemblyList()
        {
            IProjectConfig config = model.Configs[view.ConfigList.SelectedIndex];
            string[] list = new string[config.Assemblies.Count];
            
            for (int i = 0; i < list.Length; i++)
                list[i] = config.Assemblies[i];

            view.AssemblyList.SelectionList = list;
            if (list.Length > 0)
            {
                view.AssemblyList.SelectedIndex = 0;
                view.AssemblyPath.Text = list[0];
            }
            else
            {
                view.AssemblyList.SelectedIndex = -1;
                view.AssemblyPath.Text = string.Empty;
            }
        }

        private bool ValidateDirectoryPath(string property, string path)
        {
            try
            {
                new DirectoryInfo(path);
                return true;
            }
            catch (Exception ex)
            {
                view.MessageDisplay.Error(string.Format("Invalid directory path for {0}: {1}", property, ex.Message));
                return false;
            }
        }

        private bool ValidateFilePath(string property, string path)
        {
            try
            {
                new FileInfo(path);
                return true;
            }
            catch (Exception ex)
            {
                view.MessageDisplay.Error(string.Format("Invalid file path for {0}: {1}", property, ex.Message));
                return false;
            }
        }

        public string[] ConfigNames
        {
            get
            {
                ConfigList configs = model.Configs;

                string[] configList = new string[configs.Count];
                for (int i = 0; i < configs.Count; i++)
                    configList[i] = configs[i].Name;

                return configList;
            }
        }
        
        #endregion
    }
}
