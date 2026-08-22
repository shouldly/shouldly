# Contain


## ShouldContainKeyAndValue

<!-- snippet: DictionaryShouldContainKeyAndValueExamples.ShouldContainKeyAndValue.codeSample.approved.cs -->
<a id='snippet-DictionaryShouldContainKeyAndValueExamples.ShouldContainKeyAndValue.codeSample.approved.cs'></a>
```cs
var websters = new Dictionary<string, string> { { "Cromulent", "I never heard the word before moving to Springfield." } };
websters.ShouldContainKeyAndValue("Cromulent", "Fine, acceptable.");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/DictionaryShouldContainKeyAndValueExamples.ShouldContainKeyAndValue.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-DictionaryShouldContainKeyAndValueExamples.ShouldContainKeyAndValue.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
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
<a id='snippet-DictionaryShouldContainKeyAndValueExamples.ShouldNotContainKeyAndValue.codeSample.approved.cs'></a>
```cs
var websters = new Dictionary<string, string> { { "Chazzwazzers", "What Australians would have called a bull frog." } };
websters.ShouldNotContainValueForKey("Chazzwazzers", "What Australians would have called a bull frog.");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/DictionaryShouldContainKeyAndValueExamples.ShouldNotContainKeyAndValue.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-DictionaryShouldContainKeyAndValueExamples.ShouldNotContainKeyAndValue.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
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


## Value comparison

The value is compared the same way [`ShouldBe`](../equality/shouldBe.md) compares it, not with `object.Equals`. Collection values are compared element-wise, numeric values honour `ShouldlyConfiguration.DefaultFloatingPointTolerance`, and `IEquatable<T>`/`IComparable<T>` are preferred over `Equals`:

```cs
var dictionary = new Dictionary<string, int[]> { ["key"] = [1, 2, 3] };

// Passes — a different array instance with the same contents.
dictionary.ShouldContainKeyAndValue("key", [1, 2, 3]);
```

This is not a member-wise comparison. A value that is neither a collection nor implements `IEquatable<T>`, `IComparable<T>`, or `IComparable` falls through to `object.Equals`, so a reference type that doesn't override it is still compared by reference. For member-wise comparison of a whole object graph, use `ShouldBeEquivalentTo`.

One caveat, shared with `ShouldBe`: the *elements* of a collection value are compared without their static element type, so an element type that implements `IEquatable<T>` but doesn't override `Equals` (which [CA1067](https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca1067) flags) is compared by reference. `ShouldBe` on the value itself knows the element type and does honour it; it loses the same way one level deeper, on nested collections.

The key, by contrast, is always looked up with the dictionary's own key comparer.
