# NotThrow


## ShouldNotThrowAction

<!-- snippet: ShouldNotThrowExamples.ShouldNotThrowAction.codeSample.approved.cs -->
<a id='snippet-ShouldNotThrowExamples.ShouldNotThrowAction.codeSample.approved.cs'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 0;
Should.NotThrow(() =>
                {
                    var y = homer.Salary / denominator;
                });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowAction.codeSample.approved.cs#L1-L6' title='File snippet `ShouldNotThrowExamples.ShouldNotThrowAction.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotThrowExamples.ShouldNotThrowAction.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotThrowExamples.ShouldNotThrowAction.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotThrowExamples.ShouldNotThrowAction.exceptionText.approved.txt -->
```
`var y = homer.Salary / denominator;`
    should not throw but threw
System.DivideByZeroException
    with message
"Attempted to divide by zero."
```
<!-- endInclude -->

## ShouldNotThrow Action Extension

<!-- snippet: ShouldNotThrowExamples.ShouldNotThrowActionExtension.codeSample.approved.cs -->
<a id='snippet-ShouldNotThrowExamples.ShouldNotThrowActionExtension.codeSample.approved.cs'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 0;
Action action = () =>
                {
                    var y = homer.Salary / denominator;
                };
action.ShouldNotThrow();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowActionExtension.codeSample.approved.cs#L1-L7' title='File snippet `ShouldNotThrowExamples.ShouldNotThrowActionExtension.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotThrowExamples.ShouldNotThrowActionExtension.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotThrowExamples.ShouldNotThrowActionExtension.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotThrowExamples.ShouldNotThrowActionExtension.exceptionText.approved.txt -->
```
`action()`
    should not throw but threw
System.DivideByZeroException
    with message
"Attempted to divide by zero."
```
<!-- endInclude -->

## ShouldNotThrowFunc

<!-- snippet: ShouldNotThrowExamples.ShouldNotThrowFunc.codeSample.approved.cs -->
<a id='snippet-ShouldNotThrowExamples.ShouldNotThrowFunc.codeSample.approved.cs'></a>
```cs
string? name = null;
Should.NotThrow(() => new Person(name!));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowFunc.codeSample.approved.cs#L1-L2' title='File snippet `ShouldNotThrowExamples.ShouldNotThrowFunc.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotThrowExamples.ShouldNotThrowFunc.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotThrowExamples.ShouldNotThrowFunc.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotThrowExamples.ShouldNotThrowFunc.exceptionText.approved.txt -->
```
`new Person(name!)`
    should not throw but threw
System.ArgumentNullException
    with message
"Value cannot be null. (Parameter 'name')"
```
<!-- endInclude -->


## ShouldNotThrow Func Extension

<!-- snippet: ShouldNotThrowExamples.ShouldNotThrowFuncExtension.codeSample.approved.cs -->
<a id='snippet-ShouldNotThrowExamples.ShouldNotThrowFuncExtension.codeSample.approved.cs'></a>
```cs
string? name = null;
Func<Person> func = () => new Person(name!);
func.ShouldNotThrow();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowFuncExtension.codeSample.approved.cs#L1-L3' title='File snippet `ShouldNotThrowExamples.ShouldNotThrowFuncExtension.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotThrowExamples.ShouldNotThrowFuncExtension.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotThrowExamples.ShouldNotThrowFuncExtension.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotThrowExamples.ShouldNotThrowFuncExtension.exceptionText.approved.txt -->
```
`func()`
    should not throw but threw
System.ArgumentNullException
    with message
"Value cannot be null. (Parameter 'name')"
```
<!-- endInclude -->

## ShouldNotThrowFuncOfTask

<!-- snippet: ShouldNotThrowExamples.ShouldNotThrowFuncOfTask.codeSample.approved.cs -->
<a id='snippet-ShouldNotThrowExamples.ShouldNotThrowFuncOfTask.codeSample.approved.cs'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 0;
Should.NotThrow(() =>
                {
                    var task = Task.Factory.StartNew(
                        () =>
                        {
                            var y = homer.Salary / denominator;
                        });
                    return task;
                });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowFuncOfTask.codeSample.approved.cs#L1-L11' title='File snippet `ShouldNotThrowExamples.ShouldNotThrowFuncOfTask.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-ShouldNotThrowExamples.ShouldNotThrowFuncOfTask.codeSample.approved.cs' title='Navigate to start of snippet `ShouldNotThrowExamples.ShouldNotThrowFuncOfTask.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldNotThrowExamples.ShouldNotThrowFuncOfTask.exceptionText.approved.txt -->
```
`var task = Task.Factory.StartNew( () => { var y = homer.Salary / denominator; }); return task;`
    should not throw but threw
System.DivideByZeroException
    with message
"Attempted to divide by zero."
```
<!-- endInclude -->
