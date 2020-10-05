# StartWith


## ShouldStartWith

<!-- snippet: StringExamples.ShouldStartWith.codeSample.approved.cs -->
<a id='5589bd40'></a>
```cs
var target = "Homer";
target.ShouldStartWith("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldStartWith.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#5589bd40' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldStartWith.exceptionText.approved.txt -->
```
target
    should start with
"Bart"
    but was
"Homer"
```
<!-- endInclude -->


## ShouldNotStartWith

<!-- snippet: StringExamples.ShouldNotStartWith.codeSample.approved.cs -->
<a id='96c7dd1f'></a>
```cs
var target = "Homer Simpson";
target.ShouldNotStartWith("Homer");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotStartWith.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#96c7dd1f' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotStartWith.exceptionText.approved.txt -->
```
target
    should not start with
"Homer"
    but was
"Homer Simpson"
```
<!-- endInclude -->
