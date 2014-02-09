// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if NET_3_5 || NET_4_0 || NET_4_5
using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace NUnit.ProjectEditor.Tests.Presenters
{
    [TestFixture, Platform("Net-3.5,Mono-3.5,Net-4.0")]
    public class RenameConfigurationPresenterTests
    {
        IProjectModel model;
        IRenameConfigurationDialog dlg;
        RenameConfigurationPresenter presenter;

        [SetUp]
        public void Initialize()
        {
            IProjectDocument doc = new ProjectDocument();
            doc.LoadXml(NUnitProjectXml.EmptyConfigs);
            model = new ProjectModel(doc);
            dlg = Substitute.For<IRenameConfigurationDialog>();
            presenter = new RenameConfigurationPresenter(model, dlg, "Debug");
        }

        [Test]
        public void ConfigurationName_OnLoad_IsSetToOriginalName()
        {
            Assert.AreEqual("Debug", dlg.ConfigurationName.Text);
        }

        [Test]
        public void ConfigurationName_OnLoad_OriginalNameIsSelected()
        {
            dlg.ConfigurationName.Received().Select(0,5);
        }

        [Test]
        public void OkButton_OnLoad_IsDisabled()
        {
            Assert.False(dlg.OkButton.Enabled);
        }

        [Test]
        public void ConfigurationName_WhenSetToNewName_OkButtonIsEnabled()
        {
            dlg.ConfigurationName.Text = "New";
            dlg.ConfigurationName.Changed += Raise.Event<ActionDelegate>();

            Assert.True(dlg.OkButton.Enabled);
        }

        [Test]
        public void ConfigurationName_WhenSetToOriginalName_OkButtonIsDisabled()
        {
            dlg.ConfigurationName.Text = "Debug";
            dlg.ConfigurationName.Changed += Raise.Event<ActionDelegate>();

            Assert.False(dlg.OkButton.Enabled);
        }

        [Test]
        public void ConfigurationName_WhenCleared_OkButtonIsDisabled()
        {
            dlg.ConfigurationName.Text = string.Empty;
            dlg.ConfigurationName.Changed += Raise.Event<ActionDelegate>();

            Assert.False(dlg.OkButton.Enabled);
        }

        [Test]
        public void OkButton_WhenClicked_PerformsRename()
        {
            dlg.ConfigurationName.Text = "New";
            dlg.OkButton.Execute += Raise.Event<CommandDelegate>();

            Assert.That(model.ConfigNames, Is.EqualTo(new string[] { "New", "Release" }));
        }

        [Test]
        public void Dialog_WhenClosedWithoutClickingOK_LeavesConfigsUnchanged()
        {
            dlg.ConfigurationName.Text = "New";
            dlg.Close();

            Assert.That(model.ConfigNames, Is.EqualTo(new string[] { "Debug", "Release" }));
        }
    }
}
#endif
