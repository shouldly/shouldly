using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shouldly.Internals;

namespace Shouldly
{
    public class ShouldlyAssertionContext : IShouldlyAssertionContext
    {
        public string ShouldMethod { get; set; }
        public string? CodePart { get; set; }
        public string? FileName { get; set; }
        public int? LineNumber { get; set; }

        public object? Key { get; set; }
        public object? Expected { get; set; }
        public object? Actual { get; set; }
        public object? Tolerance { get; set; }
        public Case? CaseSensitivity { get; set; }
        public bool CodePartMatchesActual => CodePart == Actual.ToStringAwesomely();

        public TimeSpan? Timeout { get; set; }

        public bool IgnoreOrder { get; set; }

        // For now, this property cannot just check to see if "Actual != null". The term is overloaded.
        // In some cases it means the "Actual" value is not relevant (eg: "dictionary.ContainsKey(key)") and in some
        // cases it means that the value is relevant, but during execution we got a null. (eg: Foo.ShouldBe(bar) where
        // Foo is null). So for now, it is a flag needs to be set externally to determine whether or not the "Actual" value
        // is relevant.
        public bool HasRelevantActual { get; set; }
        public bool HasRelevantKey { get; set; }

        public bool IsNegatedAssertion => ShouldMethod.Contains("Not");
        public string? CustomMessage { get; set; }
        public Expression? Filter { get; set; }
        public int? MatchCount { get; set; }
        public SortDirection SortDirection { get; set; }
        public int OutOfOrderIndex { get; set; }
        public object? OutOfOrderObject { get; set; }
        public IEnumerable<string>? Path { get; set; }

#if StackTrace
        public ShouldlyAssertionContext(
            string shouldlyMethod, object? expected = null, object? actual = null,
            System.Diagnostics.StackTrace? stackTrace = null)
        {
            var actualCodeGetter = new ActualCodeTextGetter();
            Expected = expected;
            Actual = actual;
            ShouldMethod = shouldlyMethod;

            CodePart = actualCodeGetter.GetCodeText(actual, stackTrace);
            FileName = actualCodeGetter.FileName;
            LineNumber = actualCodeGetter.LineNumber;
        }
#else
        public ShouldlyAssertionContext(string shouldlyMethod, object? expected = null, object? actual = null)
        {
            var actualCodeGetter = new ActualCodeTextGetter();
            Expected = expected;
            Actual = actual;
            ShouldMethod = shouldlyMethod;

            CodePart = actualCodeGetter.GetCodeText(actual);
        }
#endif
    }
}
