using System;

namespace NUnit.Util.Tests.resources
{
    public class TestResource : NUnit.TestUtilities.TempResourceFile
    {
        public TestResource(string name)
            : base(typeof(TestResource), name)
        {
        }

        public TestResource(string name, string filePath)
            : base(typeof(TestResource), name, filePath)
        {
        }
    }
}
