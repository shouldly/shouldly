// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor
{
    public interface IPropertyView : IView
    {
        #region Properties

        IDialogManager DialogManager { get; }
        IConfigurationEditorDialog ConfigurationEditorDialog { get; }

        #region Command Elements

        ICommand BrowseProjectBaseCommand { get; }
        ICommand EditConfigsCommand { get; }
        ICommand BrowseConfigBaseCommand { get; }

        ICommand AddAssemblyCommand { get; }
        ICommand RemoveAssemblyCommand { get; }
        ICommand BrowseAssemblyPathCommand { get; }

        #endregion

        #region Properties of the Model as a Whole

        ITextElement ProjectPath { get; }
        ITextElement ProjectBase { get; }
        ISelectionList ProcessModel { get; }
        ISelectionList DomainUsage { get; }
        ITextElement ActiveConfigName { get; }

        ISelectionList ConfigList { get; }

        #endregion

        #region Properties of the Selected Config

        ISelectionList Runtime { get; }
        IComboBox RuntimeVersion { get; }
        ITextElement ApplicationBase { get; }
        ITextElement ConfigurationFile { get; }

        ISelection BinPathType { get; }
        ITextElement PrivateBinPath { get; }

        ISelectionList AssemblyList { get; }
        ITextElement AssemblyPath { get; }

        #endregion

        #endregion
    }
}
