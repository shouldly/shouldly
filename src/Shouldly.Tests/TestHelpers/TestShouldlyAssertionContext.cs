using System.Diagnostics;
using System.Reflection;

namespace Shouldly.Tests.TestHelpers
{
    public class TestShouldlyAssertionContext : IShouldlyAssertionContext
    {
        public bool DeterminedOriginatingFrame { get; set; }
        public string ShouldMethod { get; set; } 
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public string CodePart { get; set; }
        public StackFrame OriginatingFrame { get; set; }
        public MethodBase UnderlyingShouldMethod { get; set; }
        public object Key { get; set; }
        public object Expected { get; set; }
        public object Actual { get; set; }
        public object Tolerance { get; set; }
        public bool IgnoreOrder { get; set; }
        public bool HasRelevantActual { get; set; }
        public bool HasRelevantKey { get; set; }

        public bool IsNegatedAssertion { get { return ShouldMethod.Contains("Not"); } }

        public string CustomMessage { get; set; }

        internal TestShouldlyAssertionContext(object expected, object actual = null)
        {
            Expected = expected;
            Actual = actual;
        }
    }
}
