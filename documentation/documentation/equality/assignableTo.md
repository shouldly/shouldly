# ShouldBeAssignableTo

<!-- snippet: ShouldBeAssignableToExamples.ShouldBeAssignableTo.codeSample.approved.cs -->
<a id='59f28373'></a>
```cs
var theSimpsonsDog = new Person { Name = "Santas little helper" };
theSimpsonsDog.ShouldBeAssignableTo<Pet>();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeAssignableToExamples.ShouldBeAssignableTo.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#59f28373' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeAssignableToExamples.ShouldBeAssignableTo.exceptionText.approved.txt -->
```
theSimpsonsDog
    should be assignable to
Simpsons.Pet
    but was
Simpsons.Person
```
<!-- endInclude -->
