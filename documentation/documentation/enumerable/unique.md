# ShouldBeUnique

<!-- snippet: EnumerableShouldBeUniqueExamples.ShouldBeUnique.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeUniqueExamples.ShouldBeUnique.codeSample.approved.cs'></a>
```cs
var lisa = new Person { Name = "Lisa" };
var bart = new Person { Name = "Bart" };
var maggie = new Person { Name = "Maggie" };
var simpsonsKids = new List<Person> { bart, lisa, maggie, maggie };
simpsonsKids.ShouldBeUnique();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeUniqueExamples.ShouldBeUnique.codeSample.approved.cs#L1-L5' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeUniqueExamples.ShouldBeUnique.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeUniqueExamples.ShouldBeUnique.exceptionText.approved.txt -->
```
simpsonsKids
    should be unique but
[Maggie]
    was duplicated
```
<!-- endInclude -->
