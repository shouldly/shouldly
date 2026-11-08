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


## ShouldBeNullOrEmpty

<!-- snippet: EnumerableShouldBeEmptyExamples.ShouldBeNullOrEmpty.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeEmptyExamples.ShouldBeNullOrEmpty.codeSample.approved.cs'></a>
```cs
var bart = new Person { Name = "Bart" };
var detentionOnTheLastDayOfSchool = new List<Person> { bart };
detentionOnTheLastDayOfSchool.ShouldBeNullOrEmpty();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeEmptyExamples.ShouldBeNullOrEmpty.codeSample.approved.cs#L1-L3' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeEmptyExamples.ShouldBeNullOrEmpty.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeEmptyExamples.ShouldBeNullOrEmpty.exceptionText.approved.txt -->
```
detentionOnTheLastDayOfSchool
    should be null or empty but had
1
    item and was
[Bart]
```
<!-- endInclude -->


## ShouldNotBeNullOrEmpty

Equivalent to `ShouldNotBeEmpty`, which also fails for null.

<!-- snippet: EnumerableShouldBeEmptyExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeEmptyExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs'></a>
```cs
List<Person>? springfieldNuclearSafetyInspections = null;
springfieldNuclearSafetyInspections.ShouldNotBeNullOrEmpty();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeEmptyExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldBeEmptyExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeEmptyExamples.ShouldNotBeNullOrEmpty.exceptionText.approved.txt -->
```
springfieldNuclearSafetyInspections (null)
    should not be null or empty
```
<!-- endInclude -->
