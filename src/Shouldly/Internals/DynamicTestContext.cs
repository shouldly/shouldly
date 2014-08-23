using System.Diagnostics;

namespace Shouldly.Internals
{
    public class DynamicTestContext : TestContext
    {
        public dynamic DynamicTestObject { get; set; }
        public string HavePropertyName { get; set; }
        public StackFrame CallingMethodStackFrame { get; set; }
    }
}