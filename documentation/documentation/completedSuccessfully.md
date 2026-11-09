# ShouldBeCompletedSuccessfully

`ShouldBeCompletedSuccessfully` asserts that a `Task` has already completed successfully (`TaskStatus.RanToCompletion`). On a `Task<T>` it returns the task's result, so you can assert on it straight away.

It checks the task's status at the moment it is called and **does not wait** for the task to finish. Use it when the code under test should have completed synchronously, such as a cache hit or a stubbed dependency that returns a completed task. To wait for a task to finish, `await` it or use [`Should.CompleteIn`](completeIn.md).

If the task faulted, the message includes the exception that faulted it. If it was canceled, or had not yet completed, the message says so.

<!-- snippet: ShouldBeCompletedSuccessfullyExamples.ShouldBeCompletedSuccessfully.codeSample.approved.cs -->
<a id='snippet-ShouldBeCompletedSuccessfullyExamples.ShouldBeCompletedSuccessfully.codeSample.approved.cs'></a>
```cs
var lookup = Task.FromException<int>(new InvalidOperationException("Cache miss"));
var value = lookup.ShouldBeCompletedSuccessfully();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeCompletedSuccessfullyExamples.ShouldBeCompletedSuccessfully.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeCompletedSuccessfullyExamples.ShouldBeCompletedSuccessfully.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: ShouldBeCompletedSuccessfullyExamples.ShouldBeCompletedSuccessfully.exceptionText.approved.txt -->
```
lookup
    should be completed successfully but faulted with
System.InvalidOperationException: Cache miss
```
<!-- endInclude -->
