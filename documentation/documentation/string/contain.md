# Contain


## ShouldContain

<!-- snippet: StringExamples.ShouldContain.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldContain.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldContain("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldContain.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldContain.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldContain.exceptionText.approved.txt -->
```
target
    should contain (case insensitive comparison)
"Bart"
    but was actually
"Homer"
```
<!-- endInclude -->


## ShouldContainWithoutWhitespace

<!-- snippet: StringExamples.ShouldContainWithoutWhitespace.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldContainWithoutWhitespace.codeSample.approved.cs'></a>
```cs
var target = "Homer Simpson";
target.ShouldContainWithoutWhitespace(" Bart Simpson ");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldContainWithoutWhitespace.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldContainWithoutWhitespace.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldContainWithoutWhitespace.exceptionText.approved.txt -->
```
target
    should contain without whitespace
" Bart Simpson "
    but was actually
"Homer Simpson"
```
<!-- endInclude -->


## ShouldNotContain

<!-- snippet: StringExamples.ShouldNotContain.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldNotContain.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldNotContain("Home");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotContain.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldNotContain.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotContain.exceptionText.approved.txt -->
```
target
    should not contain (case insensitive comparison)
"Home"
    but was actually
"Homer"
```
<!-- endInclude -->
