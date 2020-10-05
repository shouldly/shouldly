# Null


## ShouldBeNull

<!-- snippet: StringExamples.ShouldBeNull.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldBeNull.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldBeNull();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldBeNull.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldBeNull.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldBeNull.exceptionText.approved.txt -->
```
target
    should be null but was
"Homer"
```
<!-- endInclude -->


## ShouldBeNullOrEmpty

<!-- snippet: StringExamples.ShouldBeNullOrEmpty.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldBeNullOrEmpty.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldBeNullOrEmpty();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldBeNullOrEmpty.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldBeNullOrEmpty.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldBeNullOrEmpty.exceptionText.approved.txt -->
```
target ("Homer")
    should be null or empty
```
<!-- endInclude -->


## ShouldBeEmpty

<!-- snippet: StringExamples.ShouldBeEmpty.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldBeEmpty.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldBeEmpty();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldBeEmpty.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldBeEmpty.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldBeEmpty.exceptionText.approved.txt -->
```
target
    should be empty but was
"Homer"
```
<!-- endInclude -->


## ShouldNotBeNull

<!-- snippet: StringExamples.ShouldNotBeNull.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldNotBeNull.codeSample.approved.cs'></a>
```cs
string target = null;
target.ShouldNotBeNull();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotBeNull.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldNotBeNull.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotBeNull.exceptionText.approved.txt -->
```
target
    should not be null but was
```
<!-- endInclude -->


## ShouldNotBeNullOrEmpty

<!-- snippet: StringExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs'></a>
```cs
var target = "";
target.ShouldNotBeNullOrEmpty();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotBeNullOrEmpty.exceptionText.approved.txt -->
```
target ("")
    should not be null or empty
```
<!-- endInclude -->


## ShouldNotBeEmpty

<!-- snippet: StringExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs'></a>
```cs
var target = "";
target.ShouldNotBeNullOrEmpty();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldNotBeNullOrEmpty.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotBeEmpty.exceptionText.approved.txt -->
```
target
    should not be empty but was
```
<!-- endInclude -->

