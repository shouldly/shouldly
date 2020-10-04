# ShouldBeAssignableTo

<!-- snippet: ShouldBeAssignableToExamples.ShouldBeAssignableTo.codeSample.approved.cs -->
<a id='snippet-ShouldBeAssignableToExamples.ShouldBeAssignableTo.codeSample.approved.cs'></a>
```cs
var theSimpsonsDog = new Person { Name = "Santas little helper" };
theSimpsonsDog.ShouldBeAssignableTo<Pet>();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeAssignableToExamples.ShouldBeAssignableTo.codeSample.approved.cs#L1-L2' title='File snippet `ShouldBeAssignableToExamples.ShouldBeAssignableTo.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldBeAssignableToExamples.ShouldBeAssignableTo.codeSample.approved.cs' title='Navigate to start of snippet `ShouldBeAssignableToExamples.ShouldBeAssignableTo.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeAssignableToExamples.ShouldBeAssignableTo.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldBeAssignableToExamples.ShouldBeAssignableTo.exceptionText.approved.txt -->
```
theSimpsonsDog
    should be assignable to
Simpsons.Pet
    but was
Simpsons.Person
```
<!-- endInclude -->
