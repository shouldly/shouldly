# ShouldBe

<!-- snippet: StringExamples.ShouldBe.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldBe.codeSample.approved.cs'></a>
```cs
var target = "Homer";
target.ShouldBe("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldBe.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldBe.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldBe.exceptionText.approved.txt -->
```
target
    should be
"Bart"
    but was
"Homer"
    difference
Expected: "Bart"
Actual:   "Homer"
```
<!-- endInclude -->

> **A note on alignment.** The `▼`/`▲` markers point at the differing grapheme clusters using estimated terminal widths. On terminals or fonts that render emoji, CJK, or other wide characters at different widths than expected, markers may shift by a column. When the difference involves a combining mark, zero-width character, flag emoji, or right-to-left script, a `Difference at index N: U+XXXX vs U+YYYY` line is appended so the codepoints are unambiguous regardless of how your terminal renders the glyphs.

> **Unicode normalization.** Strings that render identically but use different Unicode normalization forms (for example a precomposed `é` U+00E9 versus `e` followed by the combining acute accent U+0301, as produced by macOS file names or copy-and-paste) get markers even when the strings are short (small differences also get the codepoint line above), plus a hint naming each side's form, such as `Strings differ only in Unicode normalization: expected is NFC, actual is NFD`. Normalize both values (for example with `string.Normalize()`) before comparing if the form doesn't matter to you.


## ShouldNotBe

<!-- snippet: StringExamples.ShouldNotBe.codeSample.approved.cs -->
<a id='snippet-StringExamples.ShouldNotBe.codeSample.approved.cs'></a>
```cs
var target = "Bart";
target.ShouldNotBe("Bart");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/StringExamples.ShouldNotBe.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-StringExamples.ShouldNotBe.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: StringExamples.ShouldNotBe.exceptionText.approved.txt -->
```
target
    should not be
"Bart"
    but was
```
<!-- endInclude -->
