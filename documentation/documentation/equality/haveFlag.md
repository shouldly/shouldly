# HaveFlag

`ShouldHaveFlag` allows you to assert whether an object is an enum and has a flag specified.

Conversely `ShouldNotHaveFlag` allows you to assert the opposite; that an object is an enum but does not have a flag specified.


## ShouldHaveFlag

<!-- snippet: ShouldHaveFlagNotHaveFlagExamples.ShouldHaveFlag.codeSample.approved.cs -->
<a id='snippet-ShouldHaveFlagNotHaveFlagExamples.ShouldHaveFlag.codeSample.approved.cs'></a>
```cs
var actual = TestEnum.FlagTwo;
var value = TestEnum.FlagOne;
actual.ShouldHaveFlag(value);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldHaveFlagNotHaveFlagExamples.ShouldHaveFlag.codeSample.approved.cs#L1-L3' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldHaveFlagNotHaveFlagExamples.ShouldHaveFlag.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldHaveFlagNotHaveFlagExamples.ShouldHaveFlag.exceptionText.approved.txt -->
```
actual
    should have flag
TestEnum.FlagOne
    but had
TestEnum.FlagTwo
```
<!-- endInclude -->


## ShouldNotHaveFlag

<!-- snippet: ShouldHaveFlagNotHaveFlagExamples.ShouldNotHaveFlag.codeSample.approved.cs -->
<a id='snippet-ShouldHaveFlagNotHaveFlagExamples.ShouldNotHaveFlag.codeSample.approved.cs'></a>
```cs
var actual = TestEnum.FlagOne;
var value = TestEnum.FlagOne;
actual.ShouldNotHaveFlag(value);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldHaveFlagNotHaveFlagExamples.ShouldNotHaveFlag.codeSample.approved.cs#L1-L3' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldHaveFlagNotHaveFlagExamples.ShouldNotHaveFlag.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldHaveFlagNotHaveFlagExamples.ShouldNotHaveFlag.exceptionText.approved.txt -->
```
actual
    should not have flag
TestEnum.FlagOne
    but it had
TestEnum.FlagOne
```
<!-- endInclude -->
