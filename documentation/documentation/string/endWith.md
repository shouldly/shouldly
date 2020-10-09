# EndWith


## ShouldEndWith

<!-- snippet: StringExamples.ShouldEndWith.codeSample.approved.cs -->
<a id='c9273a3e'></a>
```cs
var target = "Homer";
target.ShouldEndWith("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldEndWith.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#c9273a3e' title='Start of snippet'>anchor</a></sup>
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
<a id='81d25dbe'></a>
```cs
var target = "Homer Simpson";
target.ShouldNotEndWith("Simpson");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotEndWith.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#81d25dbe' title='Start of snippet'>anchor</a></sup>
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
