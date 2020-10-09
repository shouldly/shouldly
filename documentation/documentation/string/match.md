# Match


## ShouldMatch

<!-- snippet: StringExamples.ShouldMatch.codeSample.approved.cs -->
<a id='5a9af5f5'></a>
```cs
var target = "Homer Simpson";
target.ShouldMatch("Bart .*");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldMatch.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#5a9af5f5' title='Start of snippet'>anchor</a></sup>
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
<a id='7b306c38'></a>
```cs
var target = "Homer Simpson";
target.ShouldNotMatch("Homer .*");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotMatch.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#7b306c38' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotMatch.exceptionText.approved.txt -->
```
target should not match "Homer .*" but did
```
<!-- endInclude -->
