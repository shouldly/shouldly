// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using NUnit.Framework;

namespace NUnit.ProjectEditor.Tests.Model
{
    [TestFixture]
    public class ProjectDocumentTests
    {
        static readonly string xmlfile = "test.nunit";

        private ProjectDocument doc;
        private bool gotChangeNotice;

        [SetUp]
        public void SetUp()
        {
            doc = new ProjectDocument();
            doc.CreateNewProject();
            doc.ProjectChanged += new ActionDelegate(OnProjectChange);

            gotChangeNotice = false;
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
        public void AddingElementMakesProjectDirty()
        {
            XmlHelper.AddElement(doc.RootNode, "Settings");
            Assert.True(doc.HasUnsavedChanges);
        }

        [Test]
        public void AddingElementFiresChangedEvent()
        {
            XmlHelper.AddElement(doc.RootNode, "Settings");
            Assert.True(gotChangeNotice);
        }

        [Test]
        public void AddingAttributeMakesProjectDirty()
        {
            XmlHelper.AddAttribute(doc.RootNode, "Version", "1.0");
            Assert.True(doc.HasUnsavedChanges);
        }

        [Test]
        public void AddingAttributeFiresChangedEvent()
        {
            XmlHelper.AddAttribute(doc.RootNode, "Version", "1.0");
            Assert.True(gotChangeNotice);
        }

    }
}
