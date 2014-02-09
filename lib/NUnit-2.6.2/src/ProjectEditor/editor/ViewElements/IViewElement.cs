// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.ProjectEditor
{
    /// <summary>
    /// The IViewElement interface is exposed by the view
    /// for an individual gui element. It is the base of
    /// other more specific interfaces.
    /// </summary>
    public interface IViewElement
    {
        /// <summary>
        /// Gets the name of the element in the view
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the enabled status of the element
        /// </summary>
        bool Enabled { get; set; }
    }
}
