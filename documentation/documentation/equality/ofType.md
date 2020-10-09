# BeOfType

`ShouldBeOfType` is the inverse of `ShouldNotBeOfType`.


## ShouldBeOfType

<!-- snippet: ShouldBeOfTypeExamples.ShouldBeOfType.codeSample.approved.cs -->
<a id='8b298b2c'></a>
```cs
var theSimpsonsDog = new Cat { Name = "Santas little helper" };
theSimpsonsDog.ShouldBeOfType<Dog>();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeOfTypeExamples.ShouldBeOfType.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#8b298b2c' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeOfTypeExamples.ShouldBeOfType.exceptionText.approved.txt -->
```
theSimpsonsDog
    should be of type
Simpsons.Dog
    but was
Simpsons.Cat
```
<!-- endInclude -->


## ShouldNotBeOfType

<!-- snippet: ShouldBeOfTypeExamples.ShouldNotBeOfType.codeSample.approved.cs -->
<a id='0b5e6f75'></a>
```cs
var theSimpsonsDog = new Cat { Name = "Santas little helper" };
theSimpsonsDog.ShouldNotBeOfType<Cat>();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeOfTypeExamples.ShouldNotBeOfType.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#0b5e6f75' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeOfTypeExamples.ShouldNotBeOfType.exceptionText.approved.txt -->
```
theSimpsonsDog
    should not be of type
Simpsons.Cat
    but was
Santas little helper
```
<!-- endInclude -->
