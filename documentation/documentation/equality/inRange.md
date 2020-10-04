# InRange

`ShouldBeInRange` is the inverse of `ShouldNotBeInRange`.


## ShouldBeInRange

<!-- snippet: ShouldBeInRangeExamples.ShouldBeInRange.codeSample.approved.cs -->
<a id='snippet-ShouldBeInRangeExamples.ShouldBeInRange.codeSample.approved.cs'></a>
```cs
var homer = new Person { Name = "Homer", Salary = 300000000 };
homer.Salary.ShouldBeInRange(30000, 40000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeInRangeExamples.ShouldBeInRange.codeSample.approved.cs#L1-L2' title='File snippet `ShouldBeInRangeExamples.ShouldBeInRange.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldBeInRangeExamples.ShouldBeInRange.codeSample.approved.cs' title='Navigate to start of snippet `ShouldBeInRangeExamples.ShouldBeInRange.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeInRangeExamples.ShouldBeInRange.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldBeInRangeExamples.ShouldBeInRange.exceptionText.approved.txt -->
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
<a id='snippet-ShouldBeInRangeExamples.ShouldNotBeInRange.codeSample.approved.cs'></a>
```cs
var mrBurns = new Person { Name = "Mr. Burns", Salary = 30000 };
mrBurns.Salary.ShouldNotBeInRange(30000, 40000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeInRangeExamples.ShouldNotBeInRange.codeSample.approved.cs#L1-L2' title='File snippet `ShouldBeInRangeExamples.ShouldNotBeInRange.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldBeInRangeExamples.ShouldNotBeInRange.codeSample.approved.cs' title='Navigate to start of snippet `ShouldBeInRangeExamples.ShouldNotBeInRange.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeInRangeExamples.ShouldNotBeInRange.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldBeInRangeExamples.ShouldNotBeInRange.exceptionText.approved.txt -->
```
mrBurns.Salary
    should not be in range
{ from = 30000, to = 40000 }
    but was
30000
```
<!-- endInclude -->
