# ShouldBe

<!-- snippet: EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs'></a>
```cs
var apu = new Person { Name = "Apu" };
var homer = new Person { Name = "Homer" };
var skinner = new Person { Name = "Skinner" };
var barney = new Person { Name = "Barney" };
var theBeSharps = new List<Person> { homer, skinner, barney };
theBeSharps.ShouldBe(new[] { apu, homer, skinner, barney });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs#L1-L6' title='File snippet `EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs' title='Navigate to start of snippet `EnumerableShouldBeExamples.ShouldBe.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldBeExamples.ShouldBe.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/EnumerableShouldBeExamples.ShouldBe.exceptionText.approved.txt -->
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
