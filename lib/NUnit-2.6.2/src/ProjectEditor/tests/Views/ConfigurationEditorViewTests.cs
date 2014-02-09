// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.ProjectEditor.Tests.Views
{
    public class ConfigurationEditorViewTests
    {
        [Test]
        public void AllViewElementsAreWrapped()
        {
            ConfigurationEditorDialog view = new ConfigurationEditorDialog();

            Assert.NotNull(view.AddCommand);
            Assert.NotNull(view.RemoveCommand);
            Assert.NotNull(view.RenameCommand);
            Assert.NotNull(view.ActiveCommand);

            Assert.NotNull(view.ConfigList);
        }
    }
}
