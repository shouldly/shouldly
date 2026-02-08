# ShouldBe


## Objects

`ShouldBeExamples` works on all types and compares using `.Equals`.

<!-- snippet: ShouldBeObjects -->
<a id='snippet-ShouldBeObjects'></a>
```cs
var theSimpsonsCat = new Cat { Name = "Santas little helper" };
theSimpsonsCat.Name.ShouldBe("Snowball 2");
```
<sup><a href='/src/DocumentationExamples/ShouldBeExamples.cs#L14-L19' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeObjects' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.Objects.exceptionText.approved.txt -->
```
theSimpsonsCat.Name
    should be
"Snowball 2"
    but was
"Santas little helper"
    difference
            ▼▼▼▼▼▼ ▼▼
Expected: "Snowball 2"
Actual:   "Santas little helper"
            ▲▲▲▲▲▲ ▲▲▲▲▲▲▲▲▲▲▲▲
```
<!-- endInclude -->


## Numeric

`ShouldBe` numeric overloads accept tolerances and has overloads for `float`, `double` and `decimal` types.

<!-- snippet: ShouldBeExamples.Numeric.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.Numeric.codeSample.approved.cs'></a>
```cs
const decimal pi = (decimal)Math.PI;
pi.ShouldBe(3.24m, 0.01m);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.Numeric.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.Numeric.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.Numeric.exceptionText.approved.txt -->
```
pi
    should be within
0.01m
    of
3.24m
    but was
3.14159265358979m
```
<!-- endInclude -->


## DateTime(Offset)

DateTime overloads are similar to the numeric overloads and support tolerances.

<!-- snippet: ShouldBeExamples.DateTime.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.DateTime.codeSample.approved.cs'></a>
```cs
var date = new DateTime(2000, 6, 1);
date.ShouldBe(new(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.DateTime.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.DateTime.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.DateTime.exceptionText.approved.txt -->
```
date
    should be within
01:00:00
    of
2000-06-01T01:00:01.0000000
    but was
2000-06-01T00:00:00.0000000
```
<!-- endInclude -->


## TimeSpan

TimeSpan also has tolerance overloads

<!-- snippet: ShouldBeExamples.TimeSpanExample.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.TimeSpanExample.codeSample.approved.cs'></a>
```cs
var timeSpan = TimeSpan.FromHours(1);
timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.TimeSpanExample.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.TimeSpanExample.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.TimeSpanExample.exceptionText.approved.txt -->
```
timeSpan
    should be within
01:00:00
    of
02:06:00
    but was
01:00:00
```
<!-- endInclude -->


## Enumerables

Enumerable comparison is done on the elements in the enumerable, so you can compare an array to a list and have it pass.

<!-- snippet: ShouldBeExamples.Enumerables.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.Enumerables.codeSample.approved.cs'></a>
```cs
var apu = new Person { Name = "Apu" };
var homer = new Person { Name = "Homer" };
var skinner = new Person { Name = "Skinner" };
var barney = new Person { Name = "Barney" };
var theBeSharps = new List<Person> { homer, skinner, barney };
theBeSharps.ShouldBe(new[] { apu, homer, skinner, barney });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.Enumerables.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.Enumerables.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.Enumerables.exceptionText.approved.txt -->
```
theBeSharps
    should be
[Apu, Homer, Skinner, Barney]
    but was
[Homer, Skinner, Barney]
    difference
[*Homer*, *Skinner*, *Barney*, *]
```
<!-- endInclude -->


## Enumerables of Numerics

If you have enumerables of `float`, `decimal` or `double` types then you can use the tolerance overloads, similar to the value extensions.

<!-- snippet: ShouldBeExamples.EnumerablesOfNumerics.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.EnumerablesOfNumerics.codeSample.approved.cs'></a>
```cs
var firstSet = new[] { 1.23m, 2.34m, 3.45001m };
var secondSet = new[] { 1.4301m, 2.34m, 3.45m };
firstSet.ShouldBe(secondSet, 0.1m);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.EnumerablesOfNumerics.codeSample.approved.cs#L1-L3' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.EnumerablesOfNumerics.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.EnumerablesOfNumerics.exceptionText.approved.txt -->
```
firstSet
    should be within
0.1m
    of
[1.4301m, 2.34m, 3.45m]
    but was
[1.23m, 2.34m, 3.45001m]
    difference
[*1.23m*, 2.34m, *3.45001m*]
```
<!-- endInclude -->


## Bools

<!-- snippet: ShouldBeExamples.BooleanExample.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.BooleanExample.codeSample.approved.cs'></a>
```cs
const bool myValue = false;
myValue.ShouldBe(true, "Some additional context");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.BooleanExample.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.BooleanExample.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.BooleanExample.exceptionText.approved.txt -->
```
myValue
    should be
True
    but was
False

Additional Info:
    Some additional context
```
<!-- endInclude -->
