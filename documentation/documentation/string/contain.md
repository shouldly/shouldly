# Contain


## ShouldContain

<!-- snippet: StringExamples.ShouldContain.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldContain.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldContain("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldContain.codeSample.approved.cs#L1-L2' title='File snippet `StringExamples.ShouldContain.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-StringExamples.ShouldContain.codeSample.approved.cs' title='Navigate to start of snippet `StringExamples.ShouldContain.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldContain.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/StringExamples.ShouldContain.exceptionText.approved.txt -->
```
target
    should contain (case insensitive comparison)
"Bart"
    but was actually
"Homer"
```
<!-- endInclude -->


## ShouldNotContain

<!-- snippet: StringExamples.ShouldNotContain.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldNotContain.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldNotContain("Home");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotContain.codeSample.approved.cs#L1-L2' title='File snippet `StringExamples.ShouldNotContain.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-StringExamples.ShouldNotContain.codeSample.approved.cs' title='Navigate to start of snippet `StringExamples.ShouldNotContain.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotContain.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotContain.exceptionText.approved.txt -->
```
target
    should not contain (case insensitive comparison)
"Home"
    but was actually
"Homer"
```
<!-- endInclude -->
