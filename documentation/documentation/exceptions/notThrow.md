# NotThrow


## ShouldNotThrowAction

<!-- snippet: ShouldNotThrowExamples.ShouldNotThrowAction.codeSample.approved.cs -->
<a id='16bf37fa'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 0;
Should.NotThrow(() =>
                {
                    var y = homer.Salary / denominator;
                });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowAction.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#16bf37fa' title='Start of snippet'>anchor</a></sup>
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
<a id='cc57c8e4'></a>
```cs
var homer = new Person {Name = "Homer", Salary = 30000};
var denominator = 0;
Action action = () =>
                {
                    var y = homer.Salary / denominator;
                };
action.ShouldNotThrow();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowActionExtension.codeSample.approved.cs#L1-L7' title='Snippet source file'>snippet source</a> | <a href='#cc57c8e4' title='Start of snippet'>anchor</a></sup>
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
<a id='7e8ffad2'></a>
```cs
string? name = null;
Should.NotThrow(() => new Person(name!));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowFunc.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#7e8ffad2' title='Start of snippet'>anchor</a></sup>
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
<a id='7ff02816'></a>
```cs
string? name = null;
Func<Person> func = () => new Person(name!);
func.ShouldNotThrow();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowFuncExtension.codeSample.approved.cs#L1-L3' title='Snippet source file'>snippet source</a> | <a href='#7ff02816' title='Start of snippet'>anchor</a></sup>
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
<a id='22f6b427'></a>
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
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldNotThrowExamples.ShouldNotThrowFuncOfTask.codeSample.approved.cs#L1-L11' title='Snippet source file'>snippet source</a> | <a href='#22f6b427' title='Start of snippet'>anchor</a></sup>
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
