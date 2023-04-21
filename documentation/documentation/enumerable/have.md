# Have

## ShouldHaveSingleItem

<!-- snippet: EnumerableShouldHaveSingleItemExamples.ShouldHaveSingleItem.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldHaveSingleItemExamples.ShouldHaveSingleItem.codeSample.approved.cs'></a>
```cs
var maggie = new Person { Name = "Maggie" };
var homer = new Person { Name = "Homer" };
var simpsonsBabies = new List<Person> { homer, maggie };
simpsonsBabies.ShouldHaveSingleItem();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldHaveSingleItemExamples.ShouldHaveSingleItem.codeSample.approved.cs#L1-L4' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldHaveSingleItemExamples.ShouldHaveSingleItem.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldHaveSingleItemExamples.ShouldHaveSingleItem.exceptionText.approved.txt -->
```
simpsonsBabies
    should have single item but had
2
    items and was
[Homer, Maggie]
```
<!-- endInclude -->
