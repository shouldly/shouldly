# InRange

`ShouldBeInRange` is the inverse of `ShouldNotBeInRange`.


## ShouldBeInRange

<!-- snippet: ShouldBeInRangeExamples.ShouldBeInRange.codeSample.approved.cs -->
<a id='3cc19e37'></a>
```cs
var homer = new Person { Name = "Homer", Salary = 300000000 };
homer.Salary.ShouldBeInRange(30000, 40000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeInRangeExamples.ShouldBeInRange.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#3cc19e37' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeInRangeExamples.ShouldBeInRange.exceptionText.approved.txt -->
```
homer.Salary
    should be in range
{ from = 30000, to = 40000 }
    but was
300000000
```
<!-- endInclude -->


## ShouldNotBeInRange

<!-- snippet: ShouldBeInRangeExamples.ShouldNotBeInRange.codeSample.approved.cs -->
<a id='a164a613'></a>
```cs
var mrBurns = new Person { Name = "Mr. Burns", Salary = 30000 };
mrBurns.Salary.ShouldNotBeInRange(30000, 40000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeInRangeExamples.ShouldNotBeInRange.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#a164a613' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeInRangeExamples.ShouldNotBeInRange.exceptionText.approved.txt -->
```
mrBurns.Salary
    should not be in range
{ from = 30000, to = 40000 }
    but was
30000
```
<!-- endInclude -->
