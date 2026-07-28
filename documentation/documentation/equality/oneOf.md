# BeOneOf

`ShouldNotBeOneOf` is the inverse of `ShouldBeOneOf`.

The candidates are passed as a single collection, not as individual arguments:

```cs
status.ShouldBeOneOf([Status.Active, Status.Pending]);
```

This was a `params` array in v4 — see the [4 to 5 upgrade guide](../upgrade/4to5.md).


## ShouldBeOneOf

<!-- snippet: ShouldBeOneOfExamples.ShouldBeOneOf.codeSample.approved.cs -->
<a id='snippet-ShouldBeOneOfExamples.ShouldBeOneOf.codeSample.approved.cs'></a>
```cs
var apu = new Person { Name = "Apu" };
var homer = new Person { Name = "Homer" };
var skinner = new Person { Name = "Skinner" };
var barney = new Person { Name = "Barney" };
var theBeSharps = new List<Person> { homer, skinner, barney };
apu.ShouldBeOneOf(theBeSharps.ToArray());
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeOneOfExamples.ShouldBeOneOf.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeOneOfExamples.ShouldBeOneOf.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeOneOfExamples.ShouldBeOneOf.exceptionText.approved.txt -->
```
apu
    should be one of
[Homer, Skinner, Barney]
    but was
Apu
```
<!-- endInclude -->


## ShouldNotBeOneOf

<!-- snippet: ShouldBeOneOfExamples.ShouldNotBeOneOf.codeSample.approved.cs -->
<a id='snippet-ShouldBeOneOfExamples.ShouldNotBeOneOf.codeSample.approved.cs'></a>
```cs
var apu = new Person { Name = "Apu" };
var homer = new Person { Name = "Homer" };
var skinner = new Person { Name = "Skinner" };
var barney = new Person { Name = "Barney" };
var wiggum = new Person { Name = "Wiggum" };
var theBeSharps = new List<Person> { apu, homer, skinner, barney, wiggum };
wiggum.ShouldNotBeOneOf(theBeSharps.ToArray());
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeOneOfExamples.ShouldNotBeOneOf.codeSample.approved.cs#L1-L7' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeOneOfExamples.ShouldNotBeOneOf.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeOneOfExamples.ShouldNotBeOneOf.exceptionText.approved.txt -->
```
wiggum
    should not be one of
[Apu, Homer, Skinner, Barney, Wiggum]
    but was
Wiggum
```
<!-- endInclude -->
