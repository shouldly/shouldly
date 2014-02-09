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
    public delegate bool ViewClosingDelegate();

    public partial class MainForm : Form, IMainView
    {
        #region Instance Variables

        private IMessageDisplay messageDisplay;
        private IDialogManager dialogManager;

        private ICommand newProjectCommand;
        private ICommand openProjectCommand;
        private ICommand closeProjectCommand;
        private ICommand saveProjectCommand;
        private ICommand saveProjectAsCommand;

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            this.messageDisplay = new MessageDisplay("Nunit Project Editor");
            this.dialogManager = new DialogManager("NUnit Project Editor");

            this.newProjectCommand = new MenuElement(newToolStripMenuItem);
            this.openProjectCommand = new MenuElement(openToolStripMenuItem);
            this.closeProjectCommand = new MenuElement(closeToolStripMenuItem);
            this.saveProjectCommand = new MenuElement(saveToolStripMenuItem);
            this.saveProjectAsCommand = new MenuElement(saveAsToolStripMenuItem);
        }

        #endregion

        #region IMainView Members

        #region Events

        public event ActiveViewChangingHandler ActiveViewChanging;
        public event ActiveViewChangedHandler ActiveViewChanged;

        #endregion

        #region Properties

        public IDialogManager DialogManager 
        {
            get { return dialogManager; }
        }

        public ICommand NewProjectCommand 
        {
            get { return newProjectCommand; }
        }

        public ICommand OpenProjectCommand 
        { 
            get {return openProjectCommand; }
        }

        public ICommand CloseProjectCommand 
        {
            get { return closeProjectCommand; }
        }

        public ICommand SaveProjectCommand 
        {
            get { return saveProjectCommand; }
        }

        public ICommand SaveProjectAsCommand 
        {
            get { return saveProjectAsCommand; }
        }

        public IXmlView XmlView
        {
            get { return xmlView; }
        }

        public IPropertyView PropertyView
        {
            get { return propertyView; }
        }

        public SelectedView SelectedView
        {
            get { return (SelectedView)tabControl1.SelectedIndex; }
            set { tabControl1.SelectedIndex = (int)value; }
        }

        public IMessageDisplay MessageDisplay 
        {
            get { return messageDisplay; }
        }

        #endregion

        #endregion

        #region Event Handlers

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog(this);
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (ActiveViewChanging != null && !ActiveViewChanging())
                e.Cancel = true;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (ActiveViewChanged != null)
                ActiveViewChanged();
        }

        #endregion
    }
}
