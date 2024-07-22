# ShouldAllBe

<!-- snippet: EnumerableShouldAllBeExamples.ShouldAllBe.codeSample.approved.cs -->
<a id='snippet-EnumerableShouldAllBeExamples.ShouldAllBe.codeSample.approved.cs'></a>
```cs
var mrBurns = new Person { Name = "Mr.Burns", Salary = 3000000 };
var kentBrockman = new Person { Name = "Kent Brockman", Salary = 3000000 };
var homer = new Person { Name = "Homer", Salary = 30000 };
var millionaires = new List<Person> { mrBurns, kentBrockman, homer };
millionaires.ShouldAllBe(m => m.Salary > 1000000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/EnumerableShouldAllBeExamples.ShouldAllBe.codeSample.approved.cs#L1-L5' title='Snippet source file'>snippet source</a> | <a href='#snippet-EnumerableShouldAllBeExamples.ShouldAllBe.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: EnumerableShouldAllBeExamples.ShouldAllBe.exceptionText.approved.txt -->
```
millionaires
    should satisfy the condition
(m.Salary > 1000000)
    but
[Homer]
    do not
```
<!-- endInclude -->

