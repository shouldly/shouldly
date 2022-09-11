# Contain


## ShouldContainKeyAndValue

<!-- snippet: DictionaryShouldContainKeyAndValueExamples.ShouldContainKeyAndValue.codeSample.approved.cs -->
<a id='a113757c'></a>
```cs
var websters = new Dictionary<string, string> { { "Cromulent", "I never heard the word before moving to Springfield." } };
websters.ShouldContainKeyAndValue("Cromulent", "Fine, acceptable.");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/DictionaryShouldContainKeyAndValueExamples.ShouldContainKeyAndValue.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#a113757c' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: DictionaryShouldContainKeyAndValueExamples.ShouldContainKeyAndValue.exceptionText.approved.txt -->
```
websters
    should contain key
"Cromulent"
    with value
"Fine, acceptable."
    but value was
"I never heard the word before moving to Springfield."
```
<!-- endInclude -->


## ShouldNotContainKeyAndValue

<!-- snippet: DictionaryShouldContainKeyAndValueExamples.ShouldNotContainKeyAndValue.codeSample.approved.cs -->
<a id='dbf0b7f8'></a>
```cs
var websters = new Dictionary<string, string> { { "Chazzwazzers", "What Australians would have called a bull frog." } };
websters.ShouldNotContainValueForKey("Chazzwazzers", "What Australians would have called a bull frog.");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/DictionaryShouldContainKeyAndValueExamples.ShouldNotContainKeyAndValue.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#dbf0b7f8' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: DictionaryShouldContainKeyAndValueExamples.ShouldNotContainKeyAndValue.exceptionText.approved.txt -->
```
websters
    should not contain key
"Chazzwazzers"
    with value
"What Australians would have called a bull frog."
    but does
```
<!-- endInclude -->
