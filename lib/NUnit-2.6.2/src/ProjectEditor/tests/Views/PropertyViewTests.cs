// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Reflection;
using NUnit.Framework;

namespace NUnit.ProjectEditor.Tests.Views
{
    public class PropertyViewTests
    {
        [Test]
        public void AllViewElementsAreInitialized()
        {
            PropertyView view = new PropertyView();

            foreach (PropertyInfo prop in typeof(PropertyView).GetProperties())
            {
                if (typeof(IViewElement).IsAssignableFrom(prop.PropertyType))
                {
                    if (prop.GetValue(view, new object[0]) == null)
                        Assert.Fail("{0} was not initialized", prop.Name);
                }
            }
        }
    }
}
