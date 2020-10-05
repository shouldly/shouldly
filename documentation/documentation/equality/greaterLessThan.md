# GreaterThan / LessThan

`ShouldBeGreaterThan` is the inverse of `ShouldBeLessThan`.


## ShouldBeGreaterThan

<!-- snippet: ShouldBeGreater_LessThanExamples.ShouldBeGreaterThan.codeSample.approved.cs -->
<a id='a7b1d7d8'></a>
```cs
var mrBurns = new Person { Name = "Mr. Burns", Salary = 30000 };
mrBurns.Salary.ShouldBeGreaterThan(300000000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeGreater_LessThanExamples.ShouldBeGreaterThan.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#a7b1d7d8' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeGreater_LessThanExamples.ShouldBeGreaterThan.exceptionText.approved.txt -->
```
mrBurns.Salary
    should be greater than
300000000
    but was
30000
```
<!-- endInclude -->


## ShouldBeGreaterThanOrEqualTo

<!-- snippet: ShouldBeGreater_LessThanExamples.ShouldBeGreaterThanOrEqualTo.codeSample.approved.cs -->
<a id='41f8b71f'></a>
```cs
var mrBurns = new Person { Name = "Mr. Burns", Salary = 299999999 };
mrBurns.Salary.ShouldBeGreaterThanOrEqualTo(300000000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeGreater_LessThanExamples.ShouldBeGreaterThanOrEqualTo.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#41f8b71f' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeGreater_LessThanExamples.ShouldBeGreaterThanOrEqualTo.exceptionText.approved.txt -->
```
mrBurns.Salary
    should be greater than or equal to
300000000
    but was
299999999
```
<!-- endInclude -->


## ShouldBeLessThan

<!-- snippet: ShouldBeGreater_LessThanExamples.ShouldBeLessThan.codeSample.approved.cs -->
<a id='3c77c61e'></a>
```cs
var homer = new Person { Name = "Homer", Salary = 300000000 };
homer.Salary.ShouldBeLessThan(30000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeGreater_LessThanExamples.ShouldBeLessThan.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#3c77c61e' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeGreater_LessThanExamples.ShouldBeLessThan.exceptionText.approved.txt -->
```
homer.Salary
    should be less than
30000
    but was
300000000
```
<!-- endInclude -->


## ShouldBeLessThanOrEqualTo

<!-- snippet: ShouldBeGreater_LessThanExamples.ShouldBeLessThanOrEqualTo.codeSample.approved.cs -->
<a id='e9da0562'></a>
```cs
var homer = new Person { Name = "Homer", Salary = 30001 };
homer.Salary.ShouldBeLessThanOrEqualTo(30000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeGreater_LessThanExamples.ShouldBeLessThanOrEqualTo.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#e9da0562' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeGreater_LessThanExamples.ShouldBeLessThanOrEqualTo.exceptionText.approved.txt -->
```
homer.Salary
    should be less than or equal to
30000
    but was
30001
```
<!-- endInclude -->
