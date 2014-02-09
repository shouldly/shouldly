// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if NET_3_5 || NET_4_0 || NET_4_5
using System;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.ProjectEditor.ViewElements;
using NSubstitute;

namespace NUnit.ProjectEditor.Tests.Presenters
{
    [TestFixture, Platform("Net-3.5,Mono-3.5,Net-4.0")]
    public class ConfigurationEditorTests
    {
        private IConfigurationEditorDialog view;

        private ProjectModel model;
        private ConfigurationEditor editor;

        [SetUp]
        public void Initialize()
        {
            ProjectDocument doc = new ProjectDocument();
            doc.LoadXml(NUnitProjectXml.NormalProject);
            model = new ProjectModel(doc);

            view = Substitute.For<IConfigurationEditorDialog>();

            editor = new ConfigurationEditor(model, view);
        }

        [Test]
        public void AddButton_OnLoad_IsSubscribed()
        {
            view.AddCommand.Received().Execute += editor.AddConfig;
        }

        [Test]
        public void RemoveButton_OnLoad_IsSubscribed()
        {
            view.RemoveCommand.Received().Execute += editor.RemoveConfig;
        }

        [Test]
        public void RenameButton_OnLoad_IsSubscribed()
        {
            view.RenameCommand.Received().Execute += editor.RenameConfig;
        }

        [Test]
        public void ActiveButton_OnLoad_IsSubscribed()
        {
            view.ActiveCommand.Received().Execute += editor.MakeActive;
        }

        [Test]
        public void ConfigList_OnLoad_SelectionChangedIsSubscribed()
        {
            view.ConfigList.Received().SelectionChanged += editor.SelectedConfigChanged;
        }

        [Test]
        public void ConfigList_OnLoad_IsCorrectlyInitialized()
        {
            Assert.That(view.ConfigList.SelectionList, Is.EqualTo( new string[] { "Debug (active)", "Release" }));
        }

        [Test]
        public void AddButton_OnLoad_IsEnabled()
        {
            Assert.True(view.AddCommand.Enabled);
        }

        [Test]
        public void RemoveButton_OnLoad_IsEnabled()
        {
            Assert.True(view.RemoveCommand.Enabled);
        }

        [Test]
        public void RenameButton_OnLoad_IsEnabled()
        {
            Assert.True(view.RenameCommand.Enabled);
        }

        [Test]
        public void ActiveButton_OnLoad_IsDisabled()
        {
            Assert.False(view.ActiveCommand.Enabled);
        }

        [Test]
        public void AddButton_WhenClicked_AddsNewConfig()
        {
            view.AddConfigurationDialog.ShowDialog().Returns(delegate
            {
                view.AddConfigurationDialog.OkButton.Execute += Raise.Event<CommandDelegate>();
                return DialogResult.OK;
            });
            view.AddConfigurationDialog.ConfigToCreate.Returns("New");
            view.AddConfigurationDialog.ConfigToCopy.Returns("Release");

            view.AddCommand.Execute += Raise.Event<CommandDelegate>();

            Assert.That(model.Configs.Count, Is.EqualTo(3));
            Assert.That(model.ConfigNames, Is.EqualTo(new string[] { "Debug", "Release", "New" }));
        }

        [Test]
        public void RemoveButton_WhenClicked_RemovesConfig()
        {
            view.RemoveCommand.Execute += Raise.Event<CommandDelegate>();

            Assert.That(model.Configs.Count, Is.EqualTo(1));
            Assert.That(model.Configs[0].Name, Is.EqualTo("Release"));
        }

        private void RaiseExecute(ICommand command)
        {
            command.Execute += Raise.Event<CommandDelegate>();
        }

        [Test]
        public void RenameButton_WhenClicked_PerformsRename()
        {
            view.ConfigList.SelectedItem.Returns("Debug");
            view.RenameConfigurationDialog.ShowDialog().Returns(delegate
            {
                view.RenameConfigurationDialog.ConfigurationName.Text = "NewName";
                view.RenameConfigurationDialog.OkButton.Execute += Raise.Event<CommandDelegate>();
                return DialogResult.OK;
            });

            view.RenameCommand.Execute += Raise.Event<CommandDelegate>();

            Assert.That(model.Configs[0].Name, Is.EqualTo("NewName"));
        }

        [Test]
        public void ActiveButton_WhenClicked_MakesConfigActive()
        {
            view.ConfigList.SelectedItem = "Release";
            RaiseExecute(view.ActiveCommand);
            Assert.That(model.ActiveConfigName, Is.EqualTo("Release"));
        }
    }
}
#endif
