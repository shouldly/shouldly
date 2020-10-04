# ContainKey

## ShouldContainKey

<!-- snippet: DictionaryShouldContainKeyExamples.ShouldContainKey.codeSample.approved.cs -->
<a id='snippet-DictionaryShouldContainKeyExamples.ShouldContainKey.codeSample.approved.cs'></a>
```cs
var websters = new Dictionary<string, string> { { "Embiggen", "To empower or embolden." } };
websters.ShouldContainKey("Cromulent");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/DictionaryShouldContainKeyExamples.ShouldContainKey.codeSample.approved.cs#L1-L2' title='File snippet `DictionaryShouldContainKeyExamples.ShouldContainKey.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-DictionaryShouldContainKeyExamples.ShouldContainKey.codeSample.approved.cs' title='Navigate to start of snippet `DictionaryShouldContainKeyExamples.ShouldContainKey.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: DictionaryShouldContainKeyExamples.ShouldContainKey.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/DictionaryShouldContainKeyExamples.ShouldContainKey.exceptionText.approved.txt -->
```
websters
    should contain key
"Cromulent"
    but does not
```
<!-- endInclude -->


## ShouldNotContainKey

<!-- snippet: DictionaryShouldContainKeyExamples.ShouldNotContainKey.codeSample.approved.cs -->
<a id='snippet-DictionaryShouldContainKeyExamples.ShouldNotContainKey.codeSample.approved.cs'></a>
```cs
var websters = new Dictionary<string, string> { { "Chazzwazzers", "What Australians would have called a bull frog." } };
websters.ShouldNotContainKey("Chazzwazzers");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/DictionaryShouldContainKeyExamples.ShouldNotContainKey.codeSample.approved.cs#L1-L2' title='File snippet `DictionaryShouldContainKeyExamples.ShouldNotContainKey.codeSample.approved.cs` was extracted from'>snippet source</a> | <a href='#snippet-DictionaryShouldContainKeyExamples.ShouldNotContainKey.codeSample.approved.cs' title='Navigate to start of snippet `DictionaryShouldContainKeyExamples.ShouldNotContainKey.codeSample.approved.cs`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: DictionaryShouldContainKeyExamples.ShouldNotContainKey.exceptionText.approved.txt. path: /src/DocumentationExamples/CodeExamples/DictionaryShouldContainKeyExamples.ShouldNotContainKey.exceptionText.approved.txt -->
```
websters
    should not contain key
"Chazzwazzers"
    but does
```
<!-- endInclude -->
