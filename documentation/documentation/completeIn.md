# ShouldCompleteIn

<!-- snippet: ShouldCompleteInExamples.ShouldCompleteIn.codeSample.approved.cs -->
<a id='snippet-ShouldCompleteInExamples.ShouldCompleteIn.codeSample.approved.cs'></a>
```cs
Should.CompleteIn(
                    action: () => { Thread.Sleep(TimeSpan.FromSeconds(15)); },
                    timeout: TimeSpan.FromSeconds(0.5),
                    customMessage: "Some additional context");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldCompleteInExamples.ShouldCompleteIn.codeSample.approved.cs#L1-L4' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldCompleteInExamples.ShouldCompleteIn.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: ShouldCompleteInExamples.ShouldCompleteIn.exceptionText.approved.txt -->
```

Delegate
    should complete in
00:00:00.5000000
    but did not

Additional Info:
    Some additional context
```
<!-- endInclude -->
