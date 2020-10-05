# Throw


## ShouldThrowAction

<!-- snippet: ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs -->
<a id='55961712'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 1;
Should.Throw<DivideByZeroException>(() =>
                {
                    var y = homer.Salary / denominator;
                });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowAction.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#55961712' title='Start of snippet'>anchor</a></sup>
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
<a id='29d6a5af'></a>
```cs
Func<Task> doSomething = async () =>
{
    await Task.Delay(1);
};
var exception = await Should.ThrowAsync<DivideByZeroException>(() => doSomething());
```
<sup><a href='/src/Shouldly.Tests/ShouldThrowAsync/FuncOfTaskScenarioAsync.cs#L106-L112' title='Snippet source file'>snippet source</a> | <a href='#29d6a5af' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

Task `doSomething()` <!-- include: FuncOfTaskScenarioAsync.ShouldThrowAsync.approved.txt -->
    should throw
System.DivideByZeroException
    but did not <!-- endInclude -->


## ShouldThrow Action Extension

<!-- snippet: ShouldThrowExamples.ShouldThrowActionExtension.codeSample.approved.cs -->
<a id='de1acfa0'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 1;
Action action = () =>
                {
                    var y = homer.Salary / denominator;
                };
action.ShouldThrow<DivideByZeroException>();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowActionExtension.codeSample.approved.cs#L1-L7' title='Snippet source file'>snippet source</a> | <a href='#de1acfa0' title='Start of snippet'>anchor</a></sup>
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
<a id='79612a9b'></a>
```cs
Should.Throw<ArgumentNullException>(() => new Person("Homer"));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowFunc.codeSample.approved.cs#L1-L1' title='Snippet source file'>snippet source</a> | <a href='#79612a9b' title='Start of snippet'>anchor</a></sup>
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
<a id='3dda9edc'></a>
```cs
Func<Person> func = () => new Person("Homer");
func.ShouldThrow<ArgumentNullException>();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowFuncExtension.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#3dda9edc' title='Start of snippet'>anchor</a></sup>
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
<a id='c98370e9'></a>
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
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldThrowExamples.ShouldThrowFuncOfTask.codeSample.approved.cs#L1-L11' title='Snippet source file'>snippet source</a> | <a href='#c98370e9' title='Start of snippet'>anchor</a></sup>
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
