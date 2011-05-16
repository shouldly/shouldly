// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Mocks;
using NUnit.UiException.Controls;

namespace NUnit.UiException.Tests
{
    public class MockHelper
    {
        public static DynamicMock NewMockIErrorRenderer(string name, int hashcode)
        {
            DynamicMock res;

            res = new DynamicMock(name, typeof(IErrorDisplay));
            res.SetReturnValue("Equals", true);
            res.SetReturnValue("GetHashCode", hashcode);

            return (res);
        }
    }
}
