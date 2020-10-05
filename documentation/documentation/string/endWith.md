# EndWith


## ShouldEndWith

<!-- snippet: StringExamples.ShouldEndWith.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldEndWith.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldEndWith("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldEndWith.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldEndWith.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldEndWith.exceptionText.approved.txt -->
```
target
    should end with
"Bart"
    but was
"Homer"
```
<!-- endInclude -->


## ShouldNotEndWith

<!-- snippet: StringExamples.ShouldNotEndWith.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldNotEndWith.codeSample.approved.cs'></a>
```cs
var target = "Homer Simpson";
target.ShouldNotEndWith("Simpson");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotEndWith.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldNotEndWith.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotEndWith.exceptionText.approved.txt -->
```
target
    should not end with
"Simpson"
    but was
"Homer Simpson"
```
<!-- endInclude -->
