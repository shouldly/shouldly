// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Xml;
using NUnit.Framework;

namespace NUnit.ProjectEditor.Tests.Model
{
    [TestFixture]
    public class ProjectCreationTests
    {
        private ProjectDocument doc;
        private ProjectModel project;
        private string xmlfile;

        private bool gotChangeNotice;

        [SetUp]
        public void SetUp()
        {
            doc = new ProjectDocument();
            doc.CreateNewProject();
            project = new ProjectModel(doc);

            doc.ProjectChanged += OnProjectChange;
            gotChangeNotice = false;

            xmlfile = Path.ChangeExtension(Path.GetTempFileName(), ".nunit");
        }

        [TearDown]
        public void EraseFile()
        {
            if (File.Exists(xmlfile))
                File.Delete(xmlfile);
        }

        private void OnProjectChange()
        {
            gotChangeNotice = true;
        }

        [Test]
        public void IsNotDirty()
        {
            Assert.IsFalse(doc.HasUnsavedChanges);
        }

        [Test]
        public void ProjectPathIsSameAsName()
        {
            Assert.AreEqual(Path.GetFullPath(doc.Name), doc.ProjectPath);
        }

        [Test]
        public void NameIsUnique()
        {
            ProjectDocument anotherProject = new ProjectDocument(xmlfile);
            Assert.AreNotEqual(doc.Name, anotherProject.Name);
        }

        [Test]
        public void RootElementIsNUnitProject()
        {
            Assert.AreEqual("NUnitProject", doc.RootNode.Name);
        }

        [Test]
        public void ProjectNodeHasNoChildren()
        {
            Assert.AreEqual(0, doc.RootNode.ChildNodes.Count);
        }

        [Test]
        public void ProjectNodeHasNoAttributes()
        {
            Assert.AreEqual(0, doc.RootNode.Attributes.Count);
        }

        [Test]
        public void NewProjectHasNoConfigs()
        {
            Assert.AreEqual(0, project.Configs.Count);
            Assert.IsNull(project.ActiveConfigName);
        }

        [Test]
        public void SaveMakesProjectNotDirty()
        {
            project.AddConfig("Debug");
            doc.Save(xmlfile);
            Assert.IsFalse(doc.HasUnsavedChanges);
        }

        [Test]
        public void SaveSetsProjectPathAndName()
        {
            doc.Save(xmlfile);
            Assert.AreEqual(Path.GetFullPath(xmlfile), doc.ProjectPath);
            Assert.AreEqual(Path.GetFileNameWithoutExtension(xmlfile), doc.Name);
        }

        [Test]
        public void DefaultProjectName()
        {
            Assert.That(doc.Name, Is.StringMatching(@"Project\d"));
        }

        [Test]
        public void CanSetAppBase()
        {
            project.BasePath = "..";
            Assert.AreEqual("..", project.BasePath);
        }

        [Test]
        public void CanAddConfigs()
        {
            project.AddConfig("Debug");
            project.AddConfig("Release");
            Assert.AreEqual(2, project.Configs.Count);
        }

        [Test]
        public void LoadMakesProjectNotDirty()
        {
            project.AddConfig("Debug");
            doc.Save(xmlfile);
            ProjectDocument doc2 = new ProjectDocument(xmlfile);
            doc2.Load();
            Assert.IsFalse(doc2.HasUnsavedChanges);
        }

        [Test]
        public void AddConfigMakesProjectDirty()
        {
            project.AddConfig("Debug");
            Assert.IsTrue(doc.HasUnsavedChanges);
        }

        [Test]
        public void AddConfigFiresChangedEvent()
        {
            project.AddConfig("Debug");
            Assert.IsTrue(gotChangeNotice);
        }
    }
}
