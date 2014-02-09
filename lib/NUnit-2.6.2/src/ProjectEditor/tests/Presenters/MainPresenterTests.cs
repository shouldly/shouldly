// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if NET_3_5 || NET_4_0 || NET_4_5
using System;
using NSubstitute;
using NUnit.Framework;
using NUnit.TestUtilities;

namespace NUnit.ProjectEditor.Tests.Presenters
{
    [TestFixture, Platform("Net-3.5,Mono-3.5,Net-4.0")]
    public class MainPresenterTests
    {
        // TODO: Embed project resources
        private static readonly string GOOD_PROJECT = "NUnitTests.nunit";
        private static readonly string BAD_PROJECT = "BadProject.nunit";
        private static readonly string NONEXISTENT_PROJECT = "NonExistent.nunit";

        private IMainView view;
        private IProjectDocument doc;
        private MainPresenter presenter;

        [SetUp]
        public void Initialize()
        {
            view = Substitute.For<IMainView>();
            doc = new ProjectDocument();
            presenter = new MainPresenter(doc, view);
        }

        [Test]
        public void ActiveViewChanged_WhenNoProjectIsOpen_TabViewsRemainHidden()
        {
            view.SelectedView.Returns(SelectedView.XmlView);
            view.ActiveViewChanged += Raise.Event<ActiveViewChangedHandler>();
            Assert.False(view.PropertyView.Visible);
            Assert.False(view.XmlView.Visible);

            view.SelectedView.Returns(SelectedView.PropertyView);
            view.ActiveViewChanged += Raise.Event<ActiveViewChangedHandler>();
            Assert.False(view.PropertyView.Visible);
            Assert.False(view.XmlView.Visible);
        }

        [Test]
        public void ActiveViewChanged_WhenProjectIsOpen_TabViewsAreVisible()
        {
            doc.CreateNewProject();

            view.SelectedView.Returns(SelectedView.XmlView);
            view.ActiveViewChanged += Raise.Event<ActiveViewChangedHandler>();

            Assert.True(view.PropertyView.Visible);
            Assert.True(view.XmlView.Visible);

        }

        [Test]
        public void CloseProject_OnLoad_IsDisabled()
        {
            Assert.False(view.CloseProjectCommand.Enabled);
        }

        [Test]
        public void CloseProject_AfterCreatingNewProject_IsEnabled()
        {
            view.NewProjectCommand.Execute += Raise.Event<CommandDelegate>();

            Assert.True(view.CloseProjectCommand.Enabled);
        }

        [Test]
        public void CloseProject_AfterOpeningGoodProject_IsEnabled()
        {
            using (TempFile file = new TempFile(GOOD_PROJECT))
            {
                view.DialogManager.GetFileOpenPath("", "", "").ReturnsForAnyArgs(file.Path);
                view.OpenProjectCommand.Execute += Raise.Event<CommandDelegate>();

                Assert.True(view.CloseProjectCommand.Enabled);
            }
        }

        [Test]
        public void NewProject_OnLoad_IsEnabled()
        {
            Assert.True(view.NewProjectCommand.Enabled);
        }

        [Test]
        public void NewProject_WhenClicked_CreatesNewProject()
        {
            view.NewProjectCommand.Execute += Raise.Event<CommandDelegate>();

            Assert.IsNotNull(doc.RootNode);
            Assert.That(doc.Name, Is.StringMatching("Project\\d"));
        }

        [Test]
        public void OpenProject_OnLoad_IsEnabled()
        {
            Assert.True(view.OpenProjectCommand.Enabled);
        }

        [Test]
        public void OpenProject_WhenClickedAndProjectIsValid_OpensProject()
        {
            using (TempFile file = new TempFile(GOOD_PROJECT))
            {
                view.DialogManager.GetFileOpenPath("Open", "", "").ReturnsForAnyArgs(file.Path);
                view.OpenProjectCommand.Execute += Raise.Event<CommandDelegate>();

                Assert.NotNull(doc.XmlText);
                Assert.NotNull(doc.RootNode);
                Assert.AreEqual("NUnitTests", doc.Name);
            }
        }

        [Test]
        public void OpenProject_WhenClickedAndProjectXmlIsNotValid_OpensProject()
        {
            using (TempFile file = new TempFile(BAD_PROJECT))
            {
                view.DialogManager.GetFileOpenPath("Open", "", "").ReturnsForAnyArgs(file.Path);
                view.OpenProjectCommand.Execute += Raise.Event<CommandDelegate>();

                Assert.NotNull(doc.XmlText);
                Assert.Null(doc.RootNode);
                Assert.AreEqual("BadProject", doc.Name);

                Assert.AreEqual(SelectedView.XmlView, view.SelectedView);
            }
        }

        [Test]
        public void OpenProject_WhenClickedAndProjectDoesNotExist_DisplaysError()
        {
            view.DialogManager.GetFileOpenPath("Open", "", "").ReturnsForAnyArgs(NONEXISTENT_PROJECT);
            view.OpenProjectCommand.Execute += Raise.Event<CommandDelegate>();

            view.MessageDisplay.Received().Error(Arg.Is((string x) => x.Contains(NONEXISTENT_PROJECT)));

            Assert.Null(doc.XmlText);
            Assert.Null(doc.RootNode);
        }

        [Test]
        public void SaveProject_OnLoad_IsDisabled()
        {
            Assert.False(view.SaveProjectCommand.Enabled);
        }

        [Test]
        public void SaveProject_AfterCreatingNewProject_IsEnabled()
        {
            view.NewProjectCommand.Execute += Raise.Event<CommandDelegate>();

            Assert.True(view.SaveProjectCommand.Enabled);
        }

        [Test]
        public void SaveProject_AfterOpeningGoodProject_IsEnabled()
        {
            using (TempFile file = new TempFile(GOOD_PROJECT))
            {
                view.DialogManager.GetFileOpenPath("", "", "").ReturnsForAnyArgs(file.Path);
                view.OpenProjectCommand.Execute += Raise.Event<CommandDelegate>();

                Assert.True(view.SaveProjectCommand.Enabled);
            }
        }

        [Test]
        public void SaveProjectAs_OnLoad_IsDisabled()
        {
            Assert.False(view.SaveProjectAsCommand.Enabled);
        }

        [Test]
        public void SaveProjectAs_AfterCreatingNewProject_IsEnabled()
        {
            view.NewProjectCommand.Execute += Raise.Event<CommandDelegate>();

            Assert.True(view.SaveProjectAsCommand.Enabled);
        }

        [Test]
        public void SaveProjectAs_AfterOpeningGoodProject_IsEnabled()
        {
            using (TempFile file = new TempFile(GOOD_PROJECT))
            {
                view.DialogManager.GetFileOpenPath("", "", "").ReturnsForAnyArgs(file.Path);
                view.OpenProjectCommand.Execute += Raise.Event<CommandDelegate>();

                Assert.True(view.SaveProjectAsCommand.Enabled);
            }
        }

        private class TempFile : TempResourceFile
        {
            public TempFile(string name) : base(typeof(NUnitProjectXml), "resources." + name, name) { }
        }
    }
}
#endif
