# SameAs


## ShouldBeSameAs

<!-- snippet: ShouldBeSameAsExamples.ShouldBeSameAs.codeSample.approved.cs -->
<a id='snippet-ShouldBeSameAsExamples.ShouldBeSameAs.codeSample.approved.cs'></a>
```cs
var principleSkinner = new Person {Name = "Armin Tamzarian"};
var seymourSkinner = new Person {Name = "Seymour Skinner"};
principleSkinner.ShouldBeSameAs(seymourSkinner);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeSameAsExamples.ShouldBeSameAs.codeSample.approved.cs#L1-L3' title='File snippet `ShouldBeSameAsExamples.ShouldBeSameAs.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldBeSameAsExamples.ShouldBeSameAs.codeSample.approved.cs' title='Navigate to start of snippet `ShouldBeSameAsExamples.ShouldBeSameAs.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeSameAsExamples.ShouldBeSameAs.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldBeSameAsExamples.ShouldBeSameAs.exceptionText.approved.txt -->
```
principleSkinner
    should be same as
Seymour Skinner
    but was
Armin Tamzarian
```
<!-- endInclude -->


## ShouldNotBeSameAs

<!-- snippet: ShouldBeSameAsExamples.ShouldNotBeSameAs.codeSample.approved.cs -->
<a id='snippet-ShouldBeSameAsExamples.ShouldNotBeSameAs.codeSample.approved.cs'></a>
```cs
var person = new Person {Name = "Armin Tamzarian"};
person.ShouldNotBeSameAs(person);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeSameAsExamples.ShouldNotBeSameAs.codeSample.approved.cs#L1-L2' title='File snippet `ShouldBeSameAsExamples.ShouldNotBeSameAs.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldBeSameAsExamples.ShouldNotBeSameAs.codeSample.approved.cs' title='Navigate to start of snippet `ShouldBeSameAsExamples.ShouldNotBeSameAs.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeSameAsExamples.ShouldNotBeSameAs.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldBeSameAsExamples.ShouldNotBeSameAs.exceptionText.approved.txt -->
```
person
    should not be same as
Armin Tamzarian
    but was
Armin Tamzarian
```
<!-- endInclude -->
