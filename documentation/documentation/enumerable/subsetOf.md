# ShouldBeSubsetOf

Without a comparer, elements are matched the way [`ShouldBe`](../equality/shouldBe.md) compares values. Shouldly's comparison can't hash, so the elements are matched pairwise, except for `string`, enums and the built-in integral, `decimal`, `bool`, `char`, `Guid`, `DateTime`, `DateTimeOffset` and `TimeSpan` types (and their nullable forms), whose hashing agrees with it. Pass an `IEqualityComparer<T>` to compare elements differently; it is used through its `GetHashCode`.
<!-- snippet: EnumerableShouldBeSubsetOfExamples.ShouldBeSubsetOf.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeSubsetOfExamples.ShouldBeSubsetOf.codeSample.approved.cs'></a>
```cs
var lisa = new Person { Name = "Lisa" };
var bart = new Person { Name = "Bart" };
var maggie = new Person { Name = "Maggie" };
var homer = new Person { Name = "Homer" };
var marge = new Person { Name = "Marge" };
var ralph = new Person { Name = "Ralph" };
var simpsonsKids = new List<Person> { bart, lisa, maggie, ralph };
var simpsonsFamily = new List<Person> { lisa, bart, maggie, homer, marge };
simpsonsKids.ShouldBeSubsetOf(simpsonsFamily);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeSubsetOfExamples.ShouldBeSubsetOf.codeSample.approved.cs#L1-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeSubsetOfExamples.ShouldBeSubsetOf.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeSubsetOfExamples.ShouldBeSubsetOf.exceptionText.approved.txt -->
```
simpsonsKids
    should be subset of
[Lisa, Bart, Maggie, Homer, Marge]
    but
[Ralph]
    is outside subset
```
<!-- endInclude -->
