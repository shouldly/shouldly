using Shouldly.Internals;

namespace Shouldly
{
    internal class TestEnvironment
    {
        public bool DeterminedOriginatingFrame { get; set; }
        public string ShouldMethod { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public TestContext TestContext { get; set; }
    }
}