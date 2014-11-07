using System.Diagnostics;
using System;
using System.IO;
using System.Linq;

namespace Shouldly
{
    internal class TestEnvironment
    {
        public bool DeterminedOriginatingFrame { get; set; }
        public string ShouldMethod { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public StackFrame OriginatingFrame  { get; set; }

        public object Key { get; set; }
        public object Expected { get; set; }
        public object Actual { get; set; }

        // For now, this property cannot just check to see if "Actual != null". The term is overloaded. 
        // In some cases it means the "Actual" value is not relevant (eg: "dictionary.ContainsKey(key)") and in some
        // cases it means that the value is relevant, but during execution we got a null. (eg: Foo.ShouldBe(bar) where 
        // Foo is null). So for now, it is a flag needs to be set externally to determine whether or not the "Actual" value
        // is relevant.
        public bool HasActual { get; set; }
        public bool HasKey { get; set; }

        public bool IsNegatedAssertion { get { return ShouldMethod.Contains("Not"); } }

        public string GetCodePart()
        {
            var codePart = "The provided expression";

            if (DeterminedOriginatingFrame)
            {
                var codeLines = String.Join("\n", File.ReadAllLines(FileName).Skip(LineNumber).ToArray());

                codePart = codeLines.Substring(0, codeLines.IndexOf(ShouldMethod) - 1).Trim();
            }
            return codePart;
        }


    }
}