# ShouldBeSupersetOf

<!-- snippet: EnumerableShouldBeSupersetOfExamples.ShouldBeSupersetOf.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeSupersetOfExamples.ShouldBeSupersetOf.codeSample.approved.cs'></a>
```cs
var lisa = new Person { Name = "Lisa" };
var bart = new Person { Name = "Bart" };
var maggie = new Person { Name = "Maggie" };
var homer = new Person { Name = "Homer" };
var marge = new Person { Name = "Marge" };
var ralph = new Person { Name = "Ralph" };
var simpsonsKids = new List<Person> { bart, lisa, maggie, ralph };
var simpsonsFamily = new List<Person> { lisa, bart, maggie, homer, marge };
simpsonsFamily.ShouldBeSupersetOf(simpsonsKids);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeSupersetOfExamples.ShouldBeSupersetOf.codeSample.approved.cs#L1-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeSupersetOfExamples.ShouldBeSupersetOf.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeSupersetOfExamples.ShouldBeSupersetOf.exceptionText.approved.txt -->
```
simpsonsFamily
    should be superset of
[Lisa, Bart, Maggie, Homer, Marge]
    but
[Ralph]
    is outside superset
```
<!-- endInclude -->
