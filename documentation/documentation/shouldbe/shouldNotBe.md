# ShouldNotBe

`ShouldNotBe` is the inverse of `ShouldBe`.


## Objects

`ShouldNotBe` works on all types and compares using `.Equals`.

<!-- snippet: ShouldNotBeExamples.Objects.codeSample.approved.cs -->
<a id='snippet-ShouldNotBeExamples.Objects.codeSample.approved.cs'></a>
```cs
var theSimpsonsCat = new Cat { Name = "Santas little helper" };
theSimpsonsCat.Name.ShouldNotBe("Santas little helper");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.Objects.codeSample.approved.cs#L1-L2' title='File snippet `ShouldNotBeExamples.Objects.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotBeExamples.Objects.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotBeExamples.Objects.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotBeExamples.Objects.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.Objects.exceptionText.approved.txt -->
```
theSimpsonsCat.Name
    should not be
"Santas little helper"
    but was
```
<!-- endInclude -->


## Numeric

`ShouldNotBe` also allows you to compare numeric values, regardless of their value type.


### Integer

<!-- snippet: ShouldNotBeExamples.NumericInt.codeSample.approved.cs -->
<a id='snippet-ShouldNotBeExamples.NumericInt.codeSample.approved.cs'></a>
```cs
const int one = 1;
one.ShouldNotBe(1);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.NumericInt.codeSample.approved.cs#L1-L2' title='File snippet `ShouldNotBeExamples.NumericInt.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotBeExamples.NumericInt.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotBeExamples.NumericInt.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotBeExamples.NumericInt.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.NumericInt.exceptionText.approved.txt -->
```
one
    should not be
1
    but was
```
<!-- endInclude -->


### Long

<!-- snippet: ShouldNotBeExamples.NumericLong.codeSample.approved.cs -->
<a id='snippet-ShouldNotBeExamples.NumericLong.codeSample.approved.cs'></a>
```cs
const long aLong = 1L;
aLong.ShouldNotBe(1);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.NumericLong.codeSample.approved.cs#L1-L2' title='File snippet `ShouldNotBeExamples.NumericLong.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotBeExamples.NumericLong.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotBeExamples.NumericLong.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotBeExamples.NumericLong.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.NumericLong.exceptionText.approved.txt -->
```
aLong
    should not be
1L
    but was
```
<!-- endInclude -->


## DateTime(Offset)

`ShouldNotBe` DateTime overloads are similar to the numeric overloads and also support tolerances.

<!-- snippet: ShouldNotBeExamples.DateTime.codeSample.approved.cs -->
<a id='snippet-ShouldNotBeExamples.DateTime.codeSample.approved.cs'></a>
```cs
var date = new DateTime(2000, 6, 1);
date.ShouldNotBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1.5));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.DateTime.codeSample.approved.cs#L1-L2' title='File snippet `ShouldNotBeExamples.DateTime.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotBeExamples.DateTime.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotBeExamples.DateTime.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotBeExamples.DateTime.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.DateTime.exceptionText.approved.txt -->
```
date
    should not be within
01:30:00
    of
2000-06-01T01:00:01.0000000
    but was
2000-06-01T00:00:00.0000000
```
<!-- endInclude -->


## TimeSpan

`TimeSpan` also has tolerance overloads

<!-- snippet: ShouldNotBeExamples.TimeSpanExample.codeSample.approved.cs -->
<a id='snippet-ShouldNotBeExamples.TimeSpanExample.codeSample.approved.cs'></a>
```cs
var timeSpan = TimeSpan.FromHours(1);
timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.TimeSpanExample.codeSample.approved.cs#L1-L2' title='File snippet `ShouldNotBeExamples.TimeSpanExample.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotBeExamples.TimeSpanExample.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotBeExamples.TimeSpanExample.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotBeExamples.TimeSpanExample.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.TimeSpanExample.exceptionText.approved.txt -->
```
timeSpan
    should not be within
01:30:00
    of
02:06:00
    but was
01:00:00
```
<!-- endInclude -->
