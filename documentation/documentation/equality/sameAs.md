# ShouldBeSameAs

<!-- snippet: ShouldBeSameAsExamples.ShouldBeSameAs.codeSample.approved.cs -->
<a id='snippet-ShouldBeSameAsExamples.ShouldBeSameAs.codeSample.approved.cs'></a>
```cs
var principleSkinner = new Person { Name = "Armin Tamzarian"};
var seymourSkinner = new Person { Name = "Seymour Skinner" };
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
