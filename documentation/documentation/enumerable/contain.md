# Contain


## ShouldContain

<!-- snippet: EnumerableShouldContainExamples.ShouldContain.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldContainExamples.ShouldContain.codeSample.approved.cs'></a>
```cs
var mrBurns = new Person { Name = "Mr.Burns", Salary = 3000000 };
var kentBrockman = new Person { Name = "Kent Brockman", Salary = 3000000 };
var homer = new Person { Name = "Homer", Salary = 30000 };
var millionaires = new List<Person> { kentBrockman, homer };
millionaires.ShouldContain(mrBurns);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldContainExamples.ShouldContain.codeSample.approved.cs#L1-L5' title='File snippet `EnumerableShouldContainExamples.ShouldContain.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-EnumerableShouldContainExamples.ShouldContain.codeSample.approved.cs' title='Navigate to start of snippet `EnumerableShouldContainExamples.ShouldContain.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldContainExamples.ShouldContain.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/EnumerableShouldContainExamples.ShouldContain.exceptionText.approved.txt -->
```
millionaires
    should contain
Mr.Burns
    but was actually
[Kent Brockman, Homer]
```
<!-- endInclude -->


### With Predicate

<!-- snippet: EnumerableShouldContainExamples.ShouldContain_Predicate.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldContainExamples.ShouldContain_Predicate.codeSample.approved.cs'></a>
```cs
var homer = new Person { Name = "Homer", Salary = 30000 };
var moe = new Person { Name = "Moe", Salary = 20000 };
var barney = new Person { Name = "Barney", Salary = 0 };
var millionaires = new List<Person> { homer, moe, barney };
millionaires.ShouldContain(m => m.Salary > 1000000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldContainExamples.ShouldContain_Predicate.codeSample.approved.cs#L1-L5' title='File snippet `EnumerableShouldContainExamples.ShouldContain_Predicate.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-EnumerableShouldContainExamples.ShouldContain_Predicate.codeSample.approved.cs' title='Navigate to start of snippet `EnumerableShouldContainExamples.ShouldContain_Predicate.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldContainExamples.ShouldContain_Predicate.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/EnumerableShouldContainExamples.ShouldContain_Predicate.exceptionText.approved.txt -->
```
millionaires
    should contain an element satisfying the condition
(m.Salary > 1000000)
    but does not
```
<!-- endInclude -->


## ShouldNotContain

<!-- snippet: EnumerableShouldNotContainExamples.ShouldNotContain.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldNotContainExamples.ShouldNotContain.codeSample.approved.cs'></a>
```cs
var homerSimpson = new Person { Name = "Homer" };
var homerGlumplich = new Person { Name = "Homer" };
var lenny = new Person { Name = "Lenny" };
var carl = new Person { Name = "carl" };
var clubOfNoHomers = new List<Person> { homerSimpson, homerGlumplich, lenny, carl };
clubOfNoHomers.ShouldNotContain(homerSimpson);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldNotContainExamples.ShouldNotContain.codeSample.approved.cs#L1-L6' title='File snippet `EnumerableShouldNotContainExamples.ShouldNotContain.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-EnumerableShouldNotContainExamples.ShouldNotContain.codeSample.approved.cs' title='Navigate to start of snippet `EnumerableShouldNotContainExamples.ShouldNotContain.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldNotContainExamples.ShouldNotContain.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/EnumerableShouldNotContainExamples.ShouldNotContain.exceptionText.approved.txt -->
```
clubOfNoHomers
    should not contain
Homer
    but was actually
[Homer, Homer, Lenny, carl]
```
<!-- endInclude -->


### With Predicate

<!-- snippet: EnumerableShouldNotContainExamples.ShouldNotContain_Predicate.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldNotContainExamples.ShouldNotContain_Predicate.codeSample.approved.cs'></a>
```cs
var mrBurns = new Person { Name = "Mr.Burns", Salary = 3000000 };
var kentBrockman = new Person { Name = "Homer", Salary = 3000000 };
var homer = new Person { Name = "Homer", Salary = 30000 };
var millionaires = new List<Person> { mrBurns, kentBrockman, homer };
millionaires.ShouldNotContain(m => m.Salary < 1000000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldNotContainExamples.ShouldNotContain_Predicate.codeSample.approved.cs#L1-L5' title='File snippet `EnumerableShouldNotContainExamples.ShouldNotContain_Predicate.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-EnumerableShouldNotContainExamples.ShouldNotContain_Predicate.codeSample.approved.cs' title='Navigate to start of snippet `EnumerableShouldNotContainExamples.ShouldNotContain_Predicate.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldNotContainExamples.ShouldNotContain_Predicate.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/EnumerableShouldNotContainExamples.ShouldNotContain_Predicate.exceptionText.approved.txt -->
```
millionaires
    should not contain an element satisfying the condition
(m.Salary < 1000000)
    but does
```
<!-- endInclude -->
