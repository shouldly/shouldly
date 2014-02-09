// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;

namespace NUnit.ProjectEditor.ViewElements
{
    public class DialogManager : IDialogManager
    {
        string caption;

        #region Constructor

        public DialogManager(string defaultCaption)
        {
            this.caption = defaultCaption;
        }

        #endregion

        #region IDialogManager Members

        public string GetFileOpenPath(string title, string filter, string initialDirectory)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = title;
            dlg.Filter = filter;
            if (initialDirectory != null)
                dlg.InitialDirectory = initialDirectory;
            dlg.FilterIndex = 1;
            dlg.FileName = "";
            dlg.Multiselect = false;

            return dlg.ShowDialog() == DialogResult.OK
                ? dlg.FileNames[0]
                : null;
        }

        public string GetSaveAsPath(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Title = title;
            dlg.Filter = filter;
            dlg.FilterIndex = 1;
            dlg.FileName = "";

            return dlg.ShowDialog() == DialogResult.OK
                ? dlg.FileName
                : null;
        }

        public string GetFolderPath(string message, string initialPath)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.Description = message;
            browser.SelectedPath = initialPath;
            return browser.ShowDialog() == DialogResult.OK
                ? browser.SelectedPath
                : null;
        }

        #endregion
    }
}
