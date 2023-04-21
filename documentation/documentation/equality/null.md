# BeNull

`ShouldBeNull` and `ShouldNotBeNull` allow you to check whether a value is null.

`ShouldNotBeNull` returns the non-null value if it succeeds so that further assertions can be chained. When used with a reference type, the returned value is the same reference annotated as non-null. Equivalently, when used on a `System.Nullable<T>` expression, the returned value is the unwrapped `T` value.

## ShouldBeNull

<!-- snippet: ShouldBeNullNotNullExamples.ShouldBeNull.codeSample.approved.cs -->
<a id='snippet-ShouldBeNullNotNullExamples.ShouldBeNull.codeSample.approved.cs'></a>
```cs
var myRef = "Hello World";
myRef.ShouldBeNull();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeNullNotNullExamples.ShouldBeNull.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeNullNotNullExamples.ShouldBeNull.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeNullNotNullExamples.ShouldBeNull.exceptionText.approved.txt -->
```
myRef
    should be null but was
"Hello World"
```
<!-- endInclude -->

### ShouldBeNull (nullable value type)

<!-- snippet: ShouldBeNullNotNullExamples.NullableValueShouldBeNull.codeSample.approved.cs -->
<a id='snippet-ShouldBeNullNotNullExamples.NullableValueShouldBeNull.codeSample.approved.cs'></a>
```cs
int? nullableValue = 42;
nullableValue.ShouldBeNull();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeNullNotNullExamples.NullableValueShouldBeNull.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeNullNotNullExamples.NullableValueShouldBeNull.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeNullNotNullExamples.NullableValueShouldBeNull.exceptionText.approved.txt -->
```
nullableValue
    should be null but was
42
```
<!-- endInclude -->


## ShouldNotBeNull

<!-- snippet: ShouldBeNullNotNullExamples.ShouldNotBeNull.codeSample.approved.cs -->
<a id='snippet-ShouldBeNullNotNullExamples.ShouldNotBeNull.codeSample.approved.cs'></a>
```cs
string? myRef = null;
myRef.ShouldNotBeNull();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeNullNotNullExamples.ShouldNotBeNull.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeNullNotNullExamples.ShouldNotBeNull.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeNullNotNullExamples.ShouldNotBeNull.exceptionText.approved.txt -->
```
myRef
    should not be null but was
```
<!-- endInclude -->

### ShouldNotBeNull (nullable value type)

<!-- snippet: ShouldBeNullNotNullExamples.NullableValueShouldNotBeNull.codeSample.approved.cs -->
<a id='snippet-ShouldBeNullNotNullExamples.NullableValueShouldNotBeNull.codeSample.approved.cs'></a>
```cs
int? myRef = null;
myRef.ShouldNotBeNull();
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeNullNotNullExamples.NullableValueShouldNotBeNull.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeNullNotNullExamples.NullableValueShouldNotBeNull.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeNullNotNullExamples.NullableValueShouldNotBeNull.exceptionText.approved.txt -->
```
myRef
    should not be null but was
```
<!-- endInclude -->

## ShouldNotBeNull with chaining

<!-- snippet: ShouldBeNullNotNullExamples.ShouldNotBeNullWithChaining.codeSample.approved.cs -->
<a id='snippet-ShouldBeNullNotNullExamples.ShouldNotBeNullWithChaining.codeSample.approved.cs'></a>
```cs
var myRef = (string?)"1234";
myRef.ShouldNotBeNull().Length.ShouldBe(5);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeNullNotNullExamples.ShouldNotBeNullWithChaining.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeNullNotNullExamples.ShouldNotBeNullWithChaining.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeNullNotNullExamples.ShouldNotBeNullWithChaining.exceptionText.approved.txt -->
```
myRef.ShouldNotBeNull().Length
    should be
5
    but was
4
```
<!-- endInclude -->

### ShouldNotBeNull with chaining  (nullable value type)

<!-- snippet: ShouldBeNullNotNullExamples.NullableValueShouldNotBeNullWithChaining.codeSample.approved.cs -->
<a id='snippet-ShouldBeNullNotNullExamples.NullableValueShouldNotBeNullWithChaining.codeSample.approved.cs'></a>
```cs
SomeStruct? nullableValue = new SomeStruct { IntProperty = 41 };
nullableValue.ShouldNotBeNull().IntProperty.ShouldBe(42);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeNullNotNullExamples.NullableValueShouldNotBeNullWithChaining.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeNullNotNullExamples.NullableValueShouldNotBeNullWithChaining.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeNullNotNullExamples.NullableValueShouldNotBeNullWithChaining.exceptionText.approved.txt -->
```
nullableValue.ShouldNotBeNull().IntProperty
    should be
42
    but was
41
```
<!-- endInclude -->
