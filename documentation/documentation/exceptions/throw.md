# Throw

## ShouldThrowAction

<!-- snippet: ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs -->
<a id='snippet-ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs'></a>
```cs
var homer = new Person { Name = "Homer", Salary = 30000 };
var denominator = 1;
Should.Throw<DivideByZeroException>(() =>
                {
                    var y = homer.Salary / denominator;
                });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs#L1-L6' title='File snippet `ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs' title='Navigate to start of snippet `ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldThrowExamples.ShouldThrowAction.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowAction.exceptionText.approved.txt -->
```
`var y = homer.Salary / denominator;`
    should throw
System.DivideByZeroException
    but did not
```
<!-- endInclude -->


## ShouldThrowFunc

<!-- snippet: ShouldThrowExamples.ShouldThrowFunc.codeSample.approved.cs -->
<a id='snippet-ShouldThrowExamples.ShouldThrowFunc.codeSample.approved.cs'></a>
```cs
Should.Throw<ArgumentNullException>(() => new Person("Homer"));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowFunc.codeSample.approved.cs#L1-L1' title='File snippet `ShouldThrowExamples.ShouldThrowFunc.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldThrowExamples.ShouldThrowFunc.codeSample.approved.cs' title='Navigate to start of snippet `ShouldThrowExamples.ShouldThrowFunc.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldThrowExamples.ShouldThrowFunc.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowFunc.exceptionText.approved.txt -->
```
`new Person("Homer")`
    should throw
System.ArgumentNullException
    but did not
```
<!-- endInclude -->


## ShouldThrowFuncOfTask

<!-- snippet: ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs -->
<a id='snippet-ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs'></a>
```cs
var homer = new Person { Name = "Homer", Salary = 30000 };
var denominator = 1;
Should.Throw<DivideByZeroException>(() =>
                {
                    var task = Task.Factory.StartNew(() => { var y = homer.Salary / denominator; });
                    return task;
                });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs#L1-L7' title='File snippet `ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs' title='Navigate to start of snippet `ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldThrowExamples.ShouldThrowFuncOfTask.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowFuncOfTask.exceptionText.approved.txt -->
```
Task `var task = Task.Factory.StartNew(() => { var y = homer.Salary / denominator; }); return task;`
    should throw
System.DivideByZeroException
    but did not
```
<!-- endInclude -->
