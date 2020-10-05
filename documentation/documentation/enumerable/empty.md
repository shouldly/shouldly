# BeEmpty


## ShouldBeEmpty

<!-- snippet: EnumerableShouldBeEmptyExamples.ShouldBeEmpty.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeEmptyExamples.ShouldBeEmpty.codeSample.approved.cs'></a>
```cs
var homer = new Person { Name = "Homer" };
var powerPlantOnTheWeekend = new List<Person> { homer };
powerPlantOnTheWeekend.ShouldBeEmpty();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeEmptyExamples.ShouldBeEmpty.codeSample.approved.cs#L1-L3' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeEmptyExamples.ShouldBeEmpty.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeEmptyExamples.ShouldBeEmpty.exceptionText.approved.txt -->
```
powerPlantOnTheWeekend
    should be empty but had
1
    item and was
[Homer]
```
<!-- endInclude -->


## ShouldNotBeEmpty

<!-- snippet: EnumerableShouldBeEmptyExamples.ShouldNotBeEmpty.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeEmptyExamples.ShouldNotBeEmpty.codeSample.approved.cs'></a>
```cs
var moesTavernOnTheWeekend = new List<Person>();
moesTavernOnTheWeekend.ShouldNotBeEmpty();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeEmptyExamples.ShouldNotBeEmpty.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeEmptyExamples.ShouldNotBeEmpty.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeEmptyExamples.ShouldNotBeEmpty.exceptionText.approved.txt -->
```
moesTavernOnTheWeekend
    should not be empty but was
```
<!-- endInclude -->
