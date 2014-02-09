// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Xml;

namespace NUnit.ProjectEditor
{
    public class XmlPresenter
    {
        private IProjectDocument doc;
        private IXmlView view;

        public XmlPresenter(IProjectDocument doc, IXmlView view)
        {
            this.doc = doc;
            this.view = view;

            view.Xml.Validated += delegate
            {
                UpdateModelFromView();

                if (!doc.IsValid)
                {
                    XmlException ex = doc.Exception as XmlException;
                    if (ex != null)
                        view.DisplayError(ex.Message, ex.LineNumber, ex.LinePosition);
                    else
                        view.DisplayError(doc.Exception.Message);
                }
            };

            doc.ProjectCreated += delegate
            {
                view.Visible = true;
                LoadViewFromModel();
            };

            doc.ProjectClosed += delegate
            {
                view.Xml.Text = null;
                view.Visible = false;
            };
        }

        public void LoadViewFromModel()
        {
            view.Xml.Text = doc.XmlText;

            if (doc.Exception != null)
            {
                XmlException ex = doc.Exception as XmlException;
                if (ex != null)
                    view.DisplayError(ex.Message, ex.LineNumber, ex.LinePosition);
                else
                    view.DisplayError(doc.Exception.Message);
            }
            else
                view.RemoveError();
        }

        private int GetOffset(int lineNumber, int charPosition)
        {
            int offset = 0;

            for (int lineCount = 1; lineCount < lineNumber; lineCount++ )
            {
                int next = doc.XmlText.IndexOf(Environment.NewLine, offset);
                if (next < 0) break;

                offset = next + Environment.NewLine.Length;
            }

            return offset - lineNumber + charPosition;
        }

        public void UpdateModelFromView()
        {
            doc.XmlText = view.Xml.Text;
        }
    }
}
