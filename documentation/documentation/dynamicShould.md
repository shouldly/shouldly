# DynamicShould


## HaveProperty

<!-- snippet: DynamicShouldExamples.HaveProperty.codeSample.approved.cs -->
<a id='7b868925'></a>
```cs
dynamic theFuture = new ExpandoObject();
DynamicShould.HaveProperty(theFuture, "RobotTeachers");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/DynamicShouldExamples.HaveProperty.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#7b868925' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: DynamicShouldExamples.HaveProperty.exceptionText.approved.txt -->
```
Dynamic object "theFuture" should contain property "RobotTeachers" but does not.
```
<!-- endInclude -->
