# True/False

`ShouldBeTrue` and `ShouldBeFalse` work on boolean values.


## ShouldBeTrue

<!-- snippet: ShouldBeTrueFalseExamples.ShouldBeTrue.codeSample.approved.cs -->
<a id='2fccec9b'></a>
```cs
var myValue = false;
myValue.ShouldBeTrue();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeTrueFalseExamples.ShouldBeTrue.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#2fccec9b' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeTrueFalseExamples.ShouldBeTrue.exceptionText.approved.txt -->
```
myValue
    should be
True
    but was
False
```
<!-- endInclude -->


## ShouldBeFalse

<!-- snippet: ShouldBeTrueFalseExamples.ShouldBeFalse.codeSample.approved.cs -->
<a id='287b8d22'></a>
```cs
var myValue = true;
myValue.ShouldBeFalse();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeTrueFalseExamples.ShouldBeFalse.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#287b8d22' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeTrueFalseExamples.ShouldBeFalse.exceptionText.approved.txt -->
```
myValue
    should be
False
    but was
True
```
<!-- endInclude -->
