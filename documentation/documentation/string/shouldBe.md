# ShouldBe

<!-- snippet: StringExamples.ShouldBe.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldBe.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldBe("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldBe.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldBe.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldBe.exceptionText.approved.txt -->
```
target
    should be
"Bart"
    but was
"Homer"
    difference
Expected: "Bart"
Actual:   "Homer"
```
<!-- endInclude -->


## ShouldNotBe

<!-- snippet: StringExamples.ShouldNotBe.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldNotBe.codeSample.approved.cs'></a>
```cs
var target = "Bart";
target.ShouldNotBe("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotBe.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldNotBe.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotBe.exceptionText.approved.txt -->
```
target
    should not be
"Bart"
    but was
```
<!-- endInclude -->
