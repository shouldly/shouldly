// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.ProjectEditor.ViewElements
{
    public interface ITextElement : IViewElement
    {
        /// <summary>
        /// Gets or sets the text of the element
        /// </summary>
        string Text { get; set; }

        void Select(int offset, int length);

        /// <summary>
        /// Changed event is raised whenever the text changes
        /// </summary>
        event ActionDelegate Changed;
        
        /// <summary>
        /// Validated event is raised when the text has been
        /// changed and focus has left the UI element.
        /// </summary>
        event ActionDelegate Validated;
    }
}
