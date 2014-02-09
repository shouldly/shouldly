// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor
{
    public partial class PropertyView : UserControl, IPropertyView
    {
        #region Instance Variables

        private IDialogManager dialogManager;
        private IMessageDisplay messageDisplay;

        private ICommand browseProjectBaseCommand;
        private ICommand editConfigsCommand;
        private ICommand browseConfigBaseCommand;
        private ICommand addAssemblyCommand;
        private ICommand removeAssemblyCommand;
        private ICommand browseAssemblyPathCommand;

        private ITextElement projectPath;
        private ITextElement projectBase;
        private ISelectionList processModel;
        private ISelectionList domainUsage;
        private ISelectionList runtime;
        private IComboBox runtimeVersion;
        private ITextElement activeConfigName;

        private ISelectionList configList;

        private ITextElement applicationBase;
        private ITextElement configurationFile;
        private ISelection binPathType;
        private ITextElement privateBinPath;
        private ISelectionList assemblyList;
        private ITextElement assemblyPath;

        #endregion

        #region Constructor

        public PropertyView()
        {
            InitializeComponent();

            InitializeViewElements();
        }

        private void InitializeViewElements()
        {
            dialogManager = new DialogManager("NUnit Project Editor");
            messageDisplay = new MessageDisplay("NUnit Project Editor");

            browseProjectBaseCommand = new ButtonElement(projectBaseBrowseButton);
            editConfigsCommand = new ButtonElement(editConfigsButton);
            browseConfigBaseCommand = new ButtonElement(configBaseBrowseButton);
            addAssemblyCommand = new ButtonElement(addAssemblyButton);
            removeAssemblyCommand = new ButtonElement(removeAssemblyButton);
            browseAssemblyPathCommand = new ButtonElement(assemblyPathBrowseButton);

            projectPath = new TextElement(projectPathLabel);
            projectBase = new TextElement(projectBaseTextBox);
            processModel = new ComboBoxElement(processModelComboBox);
            domainUsage = new ComboBoxElement(domainUsageComboBox);
            runtime = new ComboBoxElement(runtimeComboBox);
            runtimeVersion = new ComboBoxElement(runtimeVersionComboBox);
            activeConfigName = new TextElement(activeConfigLabel);

            configList = new ComboBoxElement(configComboBox);

            applicationBase = new TextElement(applicationBaseTextBox);
            configurationFile = new TextElement(configFileTextBox);
            binPathType = new RadioButtonGroup("BinPathType", autoBinPathRadioButton, manualBinPathRadioButton, noBinPathRadioButton);
            privateBinPath = new TextElement(privateBinPathTextBox);
            assemblyPath = new TextElement(assemblyPathTextBox);
            assemblyList = new ListBoxElement(assemblyListBox);
        }

        #endregion
        
        #region IPropertyView Members

        public IDialogManager DialogManager 
        {
            get { return dialogManager; }
        }

        public IMessageDisplay MessageDisplay 
        {
            get { return messageDisplay; }
        }

        public IConfigurationEditorDialog ConfigurationEditorDialog 
        {
            get { return new ConfigurationEditorDialog(); }
        }

        public ICommand BrowseProjectBaseCommand 
        {
            get { return browseProjectBaseCommand; }
        }

        public ICommand EditConfigsCommand 
        {
            get { return editConfigsCommand; }
        }

        public ICommand BrowseConfigBaseCommand 
        {
            get { return browseConfigBaseCommand; }
        }

        public ICommand AddAssemblyCommand 
        {
            get { return addAssemblyCommand; }
        }

        public ICommand RemoveAssemblyCommand 
        {
            get { return removeAssemblyCommand; }
        }

        public ICommand BrowseAssemblyPathCommand 
        {
            get { return browseAssemblyPathCommand; }
        }

        public ITextElement ProjectPath
        {
            get { return projectPath; }
        }

        public ITextElement ProjectBase 
        {
            get { return projectBase; }
        }

        public ISelectionList ProcessModel 
        {
            get { return processModel; }
        }

        public ISelectionList DomainUsage 
        {
            get { return domainUsage; }
        }

        public ITextElement ActiveConfigName 
        {
            get { return activeConfigName; }
        }

        public ISelectionList ConfigList 
        {
            get { return configList; }
        }

        public ISelectionList Runtime 
        {
            get { return runtime; }
        }

        public IComboBox RuntimeVersion 
        {
            get { return runtimeVersion; }
        }

        public ITextElement ApplicationBase 
        {
            get { return applicationBase; }
        }

        public ITextElement ConfigurationFile 
        {
            get { return configurationFile; }
        }

        public ISelection BinPathType 
        {
            get { return binPathType; }
        }

        public ITextElement PrivateBinPath 
        {
            get { return privateBinPath; }
        }

        public ISelectionList AssemblyList 
        {
            get { return assemblyList; }
        }

        public ITextElement AssemblyPath 
        {
            get { return assemblyPath; }
        }

        #endregion

        #region Helper Methods

        private string[] GetComboBoxOptions(ComboBox comboBox)
        {
            string[] options = new string[comboBox.Items.Count];

            for (int i = 0; i < comboBox.Items.Count; i++)
                options[i] = comboBox.Items[i].ToString();

            return options;
        }

        private void SetComboBoxOptions(ComboBox comboBox, string[] options)
        {
            comboBox.Items.Clear();

            foreach (object opt in options)
                comboBox.Items.Add(opt);

            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }

        #endregion
    }
}
