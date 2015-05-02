using System;
using System.Diagnostics;
using System.Reflection;

namespace Shouldly
{
    internal interface IShouldlyAssertionContext
    {
        bool DeterminedOriginatingFrame { get; set; }
        string ShouldMethod { get; set; }
        string FileName { get; set; }
        int LineNumber { get; set; }
        string CodePart { get; set; }
        StackFrame OriginatingFrame  { get; set; }
        MethodBase UnderlyingShouldMethod { get; set; }
        object Key { get; set; }
        object Expected { get; set; }
        object Actual { get; set; }
        object Tolerance { get; set; }
        TimeSpan? Timeout { get; set; }
        bool IgnoreOrder { get; set; }
        Case CaseSensitivity { get; set; }

        // For now, this property cannot just check to see if "Actual != null". The term is overloaded. 
        // In some cases it means the "Actual" value is not relevant (eg: "dictionary.ContainsKey(key)") and in some
        // cases it means that the value is relevant, but during execution we got a null. (eg: Foo.ShouldBe(bar) where 
        // Foo is null). So for now, it is a flag needs to be set externally to determine whether or not the "Actual" value
        // is relevant.
        bool HasRelevantActual { get; set; }
        bool HasRelevantKey { get; set; }

        bool IsNegatedAssertion { get; }
        string CustomMessage { get; set; }
        Case? CaseSensitivity { get; set; }
    }
}