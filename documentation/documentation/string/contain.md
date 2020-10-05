# Contain


## ShouldContain

<!-- snippet: StringExamples.ShouldContain.codeSample.approved.cs -->
<a id='40b7a014'></a>
```cs
var target = "Homer";
target.ShouldContain("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldContain.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#40b7a014' title='Start of snippet'>anchor</a></sup>
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
<a id='dd608b5d'></a>
```cs
var target = "Homer Simpson";
target.ShouldContainWithoutWhitespace(" Bart Simpson ");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldContainWithoutWhitespace.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#dd608b5d' title='Start of snippet'>anchor</a></sup>
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
<a id='296dcfb0'></a>
```cs
var target = "Homer";
target.ShouldNotContain("Home");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotContain.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#296dcfb0' title='Start of snippet'>anchor</a></sup>
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
