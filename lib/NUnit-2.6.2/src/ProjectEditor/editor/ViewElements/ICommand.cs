// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.ProjectEditor.ViewElements
{
    public interface ICommand : IViewElement
    {
        /// <summary>
        /// Execute event is raised to signal the presenter
        /// to execute the command with which this menu
        /// item is associated.
        /// </summary>
        event CommandDelegate Execute;
    }
}
