// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if NET_3_5 || NET_4_0 || NET_4_5
using System;
using System.Xml;
using NUnit.Framework;
using NSubstitute;

namespace NUnit.ProjectEditor.Tests.Presenters
{
    [TestFixture, Platform("Net-3.5,Mono-3.5,Net-4.0")]
    public class XmlPresenterTests
    {
        private IProjectDocument doc;
        private IXmlView xmlView;
        private XmlPresenter presenter;

        private static readonly string initialText = "<NUnitProject />";
        private static readonly string changedText = "<NUnitProject processModel=\"Separate\" />";

        [SetUp]
        public void Initialize()
        {
            doc = new ProjectDocument();
            doc.LoadXml(initialText);
            xmlView = Substitute.For<IXmlView>();
            presenter = new XmlPresenter(doc, xmlView);
            presenter.LoadViewFromModel();
        }

        [Test]
        public void XmlText_OnLoad_IsInitializedCorrectly()
        {
            Assert.AreEqual(initialText, xmlView.Xml.Text);
        }

        [Test]
        public void XmlText_WhenChanged_ModelIsUpdated()
        {
            xmlView.Xml.Text = changedText;
            xmlView.Xml.Validated += Raise.Event<ActionDelegate>();
            Assert.AreEqual(changedText, doc.XmlText);
        }

        [Test]
        public void BadXmlSetsException()
        {
            xmlView.Xml.Text = "<NUnitProject>"; // Missing slash
            xmlView.Xml.Validated += Raise.Event<ActionDelegate>();
            
            Assert.AreEqual("<NUnitProject>", doc.XmlText);
            Assert.NotNull(doc.Exception);
            Assert.IsInstanceOf<XmlException>(doc.Exception);

            XmlException ex = doc.Exception as XmlException;
            xmlView.Received().DisplayError(ex.Message, ex.LineNumber, ex.LinePosition);
        }
    }
}
#endif
