using System.Collections.Generic;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class PredicateScenario
    {
        [Fact]
        public void PredicateScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { 1, 2, 3 }.ShouldNotContain(i => i < 3, "Some additional context"),

errorWithSource:
@"new[] { 1, 2, 3 }
    should not contain an element satisfying the condition
(i < 3)
    but does
        [[0 => 1], [1 => 2]]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3]
    should not contain an element satisfying the condition
(i < 3)
    but does
        [[0 => 1], [1 => 2]]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void PredicateScenarioShouldFailWithNamedIEnumerable()
        {
            IEnumerable<int> iEnumerable = new List<int> {1, 2, 3, 4, 2};
            Verify.ShouldFail(() =>
iEnumerable.ShouldNotContain(i => i == 2, "Some additional context"),

errorWithSource:
@"iEnumerable
    should not contain an element satisfying the condition
(i == 2)
    but does
        [[1 => 2], [4 => 2]]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3, 4, 2]
    should not contain an element satisfying the condition
(i == 2)
    but does
        [[1 => 2], [4 => 2]]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void PredicateScenarioShouldFailWithNamedIEnumerableAndStringPredicate()
        {
            IEnumerable<string> testResults = new List<string> {"pass", "not fail", "an error", "all good", "Error: oops"};
            Verify.ShouldFail(() =>
testResults.ShouldNotContain(result => result.ToUpper().Contains("ERROR"), "From a Github issue example"),

errorWithSource:
@"testResults
    should not contain an element satisfying the condition
result.ToUpper().Contains(""ERROR"")
    but does
        [[2 => ""an error""], [4 => ""Error: oops""]]

Additional Info:
    From a Github issue example",

errorWithoutSource:
            @"[""pass"", ""not fail"", ""an error"", ""all good"", ""Error: oops""]
    should not contain an element satisfying the condition
result.ToUpper().Contains(""ERROR"")
    but does
        [[2 => ""an error""], [4 => ""Error: oops""]]

Additional Info:
    From a Github issue example");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { 1, 2, 3 }.ShouldNotContain(i => i > 3);
        }
    }
}