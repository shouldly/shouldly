# Match


## ShouldMatch

<!-- snippet: StringExamples.ShouldMatch.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldMatch.codeSample.approved.cs'></a>
```cs
var target = "Homer Simpson";
target.ShouldMatch("Bart .*");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldMatch.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldMatch.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldMatch.exceptionText.approved.txt -->
```
target
    should match
"Bart .*"
    but was
"Homer Simpson"
```
<!-- endInclude -->


## ShouldNotMatch

<!-- snippet: StringExamples.ShouldNotMatch.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldNotMatch.codeSample.approved.cs'></a>
```cs
var target = "Homer Simpson";
target.ShouldNotMatch("Homer .*");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotMatch.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldNotMatch.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotMatch.exceptionText.approved.txt -->
```
target should not match "Homer .*" but did
```
<!-- endInclude -->
