# Throw


## ShouldThrowAction

<!-- snippet: ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs -->
<a id='snippet-ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 1;
Should.Throw<DivideByZeroException>(() =>
                {
                    var y = homer.Salary / denominator;
                });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs#L1-L6' title='File snippet `ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs' title='Navigate to start of snippet `ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldThrowExamples.ShouldThrowAction.exceptionText.approved.txt -->
```
`var y = homer.Salary / denominator;`
    should throw
System.DivideByZeroException
    but did not
```
<!-- endInclude -->


## ShouldThrowAsync

<!-- snippet: ShouldThrowAsync -->
<a id='snippet-shouldthrowasync'></a>
```cs
Func<Task> doSomething = async () =>
{
    await Task.Delay(1);
};
var exception = await Should.ThrowAsync<DivideByZeroException>(() => doSomething());
```
<sup><a href='/src/Shouldly.Tests/ShouldThrowAsync/FuncOfTaskScenarioAsync.cs#L106-L112' title='File snippet `shouldthrowasync` was extracted from'>snippet source</a> | <a href='#snippet-shouldthrowasync' title='Navigate to start of snippet `shouldthrowasync`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

Task `doSomething()` <!-- include: FuncOfTaskScenarioAsync.ShouldThrowAsync.approved.txt -->
    should throw
System.DivideByZeroException
    but did not <!-- endInclude -->


## ShouldThrow Action Extension

<!-- snippet: ShouldThrowExamples.ShouldThrowActionExtension.codeSample.approved.cs -->
<a id='snippet-ShouldThrowExamples.ShouldThrowActionExtension.codeSample.approved.cs'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 1;
Action action = () =>
                {
                    var y = homer.Salary / denominator;
                };
action.ShouldThrow<DivideByZeroException>();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowActionExtension.codeSample.approved.cs#L1-L7' title='File snippet `ShouldThrowExamples.ShouldThrowActionExtension.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldThrowExamples.ShouldThrowActionExtension.codeSample.approved.cs' title='Navigate to start of snippet `ShouldThrowExamples.ShouldThrowActionExtension.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldThrowExamples.ShouldThrowActionExtension.exceptionText.approved.txt -->
```
`action()`
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

<!-- include: ShouldThrowExamples.ShouldThrowFunc.exceptionText.approved.txt -->
```
`new Person("Homer")`
    should throw
System.ArgumentNullException
    but did not
```
<!-- endInclude -->


## ShouldThrow Func Extension

<!-- snippet: ShouldThrowExamples.ShouldThrowFuncExtension.codeSample.approved.cs -->
<a id='snippet-ShouldThrowExamples.ShouldThrowFuncExtension.codeSample.approved.cs'></a>
```cs
Func<Person> func = () => new Person("Homer");
func.ShouldThrow<ArgumentNullException>();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowFuncExtension.codeSample.approved.cs#L1-L2' title='File snippet `ShouldThrowExamples.ShouldThrowFuncExtension.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldThrowExamples.ShouldThrowFuncExtension.codeSample.approved.cs' title='Navigate to start of snippet `ShouldThrowExamples.ShouldThrowFuncExtension.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldThrowExamples.ShouldThrowFuncExtension.exceptionText.approved.txt -->
```
`func()`
    should throw
System.ArgumentNullException
    but did not
```
<!-- endInclude -->


## ShouldThrowFuncOfTask

<!-- snippet: ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs -->
<a id='snippet-ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 1;
Should.Throw<DivideByZeroException>(() =>
                {
                    var task = Task.Factory.StartNew(
                        () =>
                        {
                            var y = homer.Salary / denominator;
                        });
                    return task;
                });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs#L1-L11' title='File snippet `ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs' title='Navigate to start of snippet `ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldThrowExamples.ShouldThrowFuncOfTask.exceptionText.approved.txt -->
```
Task `var task = Task.Factory.StartNew( () => { var y = homer.Salary / denominator; }); return task;`
    should throw
System.DivideByZeroException
    but did not
```
<!-- endInclude -->
