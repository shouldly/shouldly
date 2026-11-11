# ShouldBe

<!-- snippet: EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs'></a>
```cs
var apu = new Person { Name = "Apu" };
var homer = new Person { Name = "Homer" };
var skinner = new Person { Name = "Skinner" };
var barney = new Person { Name = "Barney" };
var theBeSharps = new List<Person> { homer, skinner, barney };
theBeSharps.ShouldBe(new[] { apu, homer, skinner, barney });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeExamples.ShouldBe.exceptionText.approved.txt -->
```
theBeSharps
    should be
[Apu, Homer, Skinner, Barney]
    but was
[Homer, Skinner, Barney]
    difference
[*Homer*, *Skinner*, *Barney*, *]
```
<!-- endInclude -->

## Dictionaries and sets

The order a dictionary or a set enumerates in isn't part of its value, so when both sides are dictionaries (`IDictionary<TKey, TValue>`, `IReadOnlyDictionary<TKey, TValue>` or `IDictionary`), or both are sets (`ISet<T>` or `IReadOnlySet<T>`), `ShouldBe` and `ShouldNotBe` compare them in any order. A `Dictionary` equals a `SortedDictionary` with the same entries, and a `HashSet` equals a `SortedSet` with the same elements. This applies at any depth, so a dictionary inside a list, or a set inside a dictionary, is compared the same way.

A dictionary's entries are matched by key, and their values compared, each the way `ShouldBe` compares a single value, as `ShouldBeEquivalentTo` matches them. Neither dictionary's own key comparer takes part, so a dictionary created with `StringComparer.OrdinalIgnoreCase` holding `"A"` doesn't equal one holding `"a"`. The failure message lists each key that is missing, isn't expected, or has a different value:

<!-- snippet: EnumerableShouldBeExamples.ShouldBeDictionary.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeExamples.ShouldBeDictionary.codeSample.approved.cs'></a>
```cs
var duffStock = new Dictionary<string, int> { ["Duff"] = 12, ["Duff Lite"] = 6, ["Duff Dry"] = 3 };
duffStock.ShouldBe(new Dictionary<string, int> { ["Duff Dry"] = 3, ["Duff"] = 24, ["Raspberry Duff"] = 1 });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeExamples.ShouldBeDictionary.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeExamples.ShouldBeDictionary.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeExamples.ShouldBeDictionary.exceptionText.approved.txt -->
```
duffStock
    should be
[["Duff Dry" => 3], ["Duff" => 24], ["Raspberry Duff" => 1]]
    but was
[["Duff" => 12], ["Duff Lite" => 6], ["Duff Dry" => 3]]
    difference
["Duff"] should be 24 but was 12
["Raspberry Duff"] is missing
["Duff Lite"] is not expected
```
<!-- endInclude -->

A set's elements are matched one for one, also when you pass a `Case` for strings, a tolerance for numbers, or a comparer:

<!-- snippet: EnumerableShouldBeExamples.ShouldBeSet.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeExamples.ShouldBeSet.codeSample.approved.cs'></a>
```cs
var bowlingTeam = new HashSet<string> { "Homer", "Moe", "Otto" };
bowlingTeam.ShouldBe(new HashSet<string> { "Apu", "Homer", "Moe" });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeExamples.ShouldBeSet.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeExamples.ShouldBeSet.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeExamples.ShouldBeSet.exceptionText.approved.txt -->
```
bowlingTeam
    should be
["Apu", "Homer", "Moe"]
    but was (case sensitive comparison)
["Homer", "Moe", "Otto"]
    difference
"Apu" is missing
"Otto" is not expected
```
<!-- endInclude -->

When only one side is a dictionary or a set, such as a dictionary compared with a `List<KeyValuePair<TKey, TValue>>`, the two are compared in order, like any other sequences. Pass `ignoreOrder: true` to compare them in any order.
