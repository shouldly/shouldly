// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor
{
    public delegate bool ActiveViewChangingHandler();
    public delegate void ActiveViewChangedHandler();

    /// <summary>
    /// IMainView represents the top level view for the
    /// Project editor. It provides a menu commands and several
    /// utility methods used in opening and saving files. It
    /// aggregates the property and xml views.
    /// </summary>
    public interface IMainView : IView
    {
        IDialogManager DialogManager { get; }

        ICommand NewProjectCommand { get; }
        ICommand OpenProjectCommand { get; }
        ICommand CloseProjectCommand { get; }
        ICommand SaveProjectCommand { get; }
        ICommand SaveProjectAsCommand { get; }

        event ActiveViewChangingHandler ActiveViewChanging;
        event ActiveViewChangedHandler ActiveViewChanged;

        event FormClosingEventHandler FormClosing;

        IPropertyView PropertyView { get; }
        IXmlView XmlView { get; }

        SelectedView SelectedView { get; set;  }
    }

    public enum SelectedView
    {
        PropertyView = 0,
        XmlView = 1
    }
}
