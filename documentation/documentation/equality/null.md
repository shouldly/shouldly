# BeNull

`ShouldBeNull` and `ShouldNotBeNull` allow you to check whether or not a type's reference is null.


## ShouldBeNull

<!-- snippet: ShouldBeNullNotNullExamples.ShouldBeNull.codeSample.approved.cs -->
<a id='17c561a5'></a>
```cs
var myRef = "Hello World";
myRef.ShouldBeNull();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeNullNotNullExamples.ShouldBeNull.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#17c561a5' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeNullNotNullExamples.ShouldBeNull.exceptionText.approved.txt -->
```
myRef
    should be null but was
"Hello World"
```
<!-- endInclude -->


## ShouldNotBeNull

<!-- snippet: ShouldBeNullNotNullExamples.ShouldNotBeNull.codeSample.approved.cs -->
<a id='083920a2'></a>
```cs
string? myRef = null;
myRef.ShouldNotBeNull();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeNullNotNullExamples.ShouldNotBeNull.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#083920a2' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeNullNotNullExamples.ShouldNotBeNull.exceptionText.approved.txt -->
```
myRef
    should not be null but was
```
<!-- endInclude -->
