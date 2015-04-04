using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shouldly.Tests.TestHelpers
{
    public class TestShouldlyAssertionContext : IShouldlyAssertionContext
    {
        public bool DeterminedOriginatingFrame { get; set; }
        public string ShouldMethod { get; set; } 
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public string CodePart { get; set; }
        public System.Diagnostics.StackFrame OriginatingFrame { get; set; }
        public System.Reflection.MethodBase UnderlyingShouldMethod { get; set; }
        public object Key { get; set; }
        public object Expected { get; set; }
        public object Actual { get; set; }
        public object Tolerance { get; set; }
        public bool IgnoreOrder { get; set; }
        public bool HasRelevantActual { get; set; }
        public bool HasRelevantKey { get; set; }
        public Case CaseSensitivity { get; set; }

        public bool IsNegatedAssertion { get { return ShouldMethod.Contains("Not"); } }

        internal TestShouldlyAssertionContext(object expected, object actual = null)
        {
            Expected = expected;
            Actual = actual;
        }


    }
}
