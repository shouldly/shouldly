// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor
{
    public interface IConfigurationEditorDialog : IDialog
    {
        ICommand AddCommand { get; }
        ICommand RenameCommand { get; }
        ICommand RemoveCommand { get; }
        ICommand ActiveCommand { get; }

        ISelectionList ConfigList { get; }

        IAddConfigurationDialog AddConfigurationDialog { get; }
        IRenameConfigurationDialog RenameConfigurationDialog { get; }
    }
}
