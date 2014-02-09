using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.UiException.Tests.data
{
    public class TestResource : NUnit.TestUtilities.TempResourceFile
    {
        public TestResource(string name) 
            : base(typeof( TestResource ), name) 
        {
        }
    }
}
