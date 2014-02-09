// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;

namespace NUnit.ProjectEditor
{
    /// <summary>
    /// Common interface implemented by all modal dialog views used in
    /// the ProjectEditor application
    /// </summary>
    public interface IDialog : IView
    {
        DialogResult ShowDialog();
        void Close();
    }
}
