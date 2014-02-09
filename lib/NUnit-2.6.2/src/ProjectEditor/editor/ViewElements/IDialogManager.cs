// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.ProjectEditor.ViewElements
{
    public interface IDialogManager
    {
        string GetFileOpenPath(string title, string filter, string initialDirectory);

        string GetSaveAsPath(string title, string filter);

        string GetFolderPath(string message, string initialPath);
    }
}
