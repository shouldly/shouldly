# ShouldBe


## Objects

`ShouldBe` works on all types and compares using `.Equals`.

<!-- snippet: ShouldBeObjects -->
<a id='snippet-ShouldBeObjects'></a>
```cs
var theSimpsonsCat = new Cat { Name = "Santas little helper" };
theSimpsonsCat.Name.ShouldBe("Snowball 2");
```
<sup><a href='/src/DocumentationExamples/ShouldBeExamples.cs#L14-L19' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeObjects' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.Objects.exceptionText.approved.txt -->
```
theSimpsonsCat.Name
    should be
"Snowball 2"
    but was
"Santas little helper"
    difference
Expected: "Snowball 2"
Actual:   "Santas little helper"
```
<!-- endInclude -->

### Which value's `Equals` is called

`ShouldBe` calls `Equals` on the **expected** value and passes the actual value as the argument:

```cs
expected.Equals(actual)
```

So if the type under test has a broken `Equals` that accepts anything, the assertion still can't pass.

A custom `IEqualityComparer<T>` gets the same order: `comparer.Equals(expected, actual)`.

This applies to:

- every `ShouldBe` and `ShouldNotBe` overload
- collections, with or without `ignoreOrder`
- spans and memory
- the leaf values that `ShouldBeEquivalentTo` compares
- `ShouldContain`, `ShouldNotContain`, `ShouldBeOneOf`, `ShouldNotBeOneOf` and `ShouldBeSubsetOf`, without a comparer, where the values passed to the assertion are the expected side

#### Asymmetric equality

Order only matters when equality isn't symmetric, so that `a.Equals(b)` and `b.Equals(a)` disagree. A common case is a type that treats itself as equal to a `string`, when `string` knows nothing about it:

```cs
var path = new FilePath("C:\\file.txt");

path.ShouldBe<object>("C:\\file.txt");   // fails: "C:\\file.txt".Equals(path) is false
```

To compare such a type, either:

- state the type explicitly so its own equality is used: `path.ShouldBe<FilePath>("C:\\file.txt")`
- or pass an `IEqualityComparer<T>` that defines the comparison you want


## Numeric

`ShouldBe` numeric overloads accept tolerances and has overloads for `float`, `double` and `decimal` types.

<!-- snippet: ShouldBeExamples.Numeric.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.Numeric.codeSample.approved.cs'></a>
```cs
const decimal pi = (decimal)Math.PI;
pi.ShouldBe(3.24m, 0.01m);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.Numeric.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.Numeric.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.Numeric.exceptionText.approved.txt -->
```
pi
    should be within
0.01m
    of
3.24m
    but was
3.14159265358979m
```
<!-- endInclude -->


## DateTime(Offset)

DateTime overloads are similar to the numeric overloads and support tolerances.

<!-- snippet: ShouldBeExamples.DateTime.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.DateTime.codeSample.approved.cs'></a>
```cs
var date = new DateTime(2000, 6, 1);
date.ShouldBe(new(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.DateTime.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.DateTime.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.DateTime.exceptionText.approved.txt -->
```
date
    should be within
01:00:00
    of
2000-06-01T01:00:01.0000000
    but was
2000-06-01T00:00:00.0000000
```
<!-- endInclude -->


## TimeSpan

TimeSpan also has tolerance overloads

<!-- snippet: ShouldBeExamples.TimeSpanExample.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.TimeSpanExample.codeSample.approved.cs'></a>
```cs
var timeSpan = TimeSpan.FromHours(1);
timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.TimeSpanExample.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.TimeSpanExample.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.TimeSpanExample.exceptionText.approved.txt -->
```
timeSpan
    should be within
01:00:00
    of
02:06:00
    but was
01:00:00
```
<!-- endInclude -->


## Enumerables

Enumerable comparison is done on the elements in the enumerable, so you can compare an array to a list and have it pass.

<!-- snippet: ShouldBeExamples.Enumerables.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.Enumerables.codeSample.approved.cs'></a>
```cs
var apu = new Person { Name = "Apu" };
var homer = new Person { Name = "Homer" };
var skinner = new Person { Name = "Skinner" };
var barney = new Person { Name = "Barney" };
var theBeSharps = new List<Person> { homer, skinner, barney };
theBeSharps.ShouldBe(new[] { apu, homer, skinner, barney });
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.Enumerables.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.Enumerables.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.Enumerables.exceptionText.approved.txt -->
```
theBeSharps
    should be
[Apu, Homer, Skinner, Barney]
    but was
[Homer, Skinner, Barney]
    difference
[*Homer*, *Skinner*, *Barney*, *]
```
<!-- endInclude -->


### Types with their own equality

A type that implements `IEnumerable<T>` is compared element by element, even when it overrides `Equals`. Its `Equals` is never called, so two values with different identities but the same items pass:

<!-- snippet: IdBag -->
<a id='snippet-IdBag'></a>
```cs
// An enumerable type whose equality is its Id, not its items.
public sealed class IdBag(int id, params int[] items) : IEnumerable<int>
{
    public int Id { get; } = id;

    public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)items).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override bool Equals(object? obj) => obj is IdBag other && other.Id == Id;

    public override int GetHashCode() => Id;
}
```
<sup><a href='/src/DocumentationExamples/EqualityRuleRegistration.cs#L4-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-IdBag' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

To tell Shouldly what equality means for such a type, register an equality rule once, before any test runs, for example from a module initializer in your test project:

<!-- snippet: RegisterEqualityRule -->
<a id='snippet-RegisterEqualityRule'></a>
```cs
[ModuleInitializer]
internal static void Initialize() =>
    ShouldlyConfiguration.UseEquality<IdBag>((expected, actual) => expected.Equals(actual));
```
<sup><a href='/src/DocumentationExamples/EqualityRuleRegistration.cs#L24-L30' title='Snippet source file'>snippet source</a> | <a href='#snippet-RegisterEqualityRule' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

The rule applies wherever Shouldly compares two values that are both instances of the type (or of a subtype): `ShouldBe` and `ShouldNotBe`, collection elements, dictionary keys and values, and `ShouldContain`, `ShouldBeOneOf`, `ShouldBeUnique` and the other membership assertions. A value the rule covers is never walked as a collection (unless you pass `ignoreOrder: true`, which matches its elements in any order, each by the rule), so `bag.ShouldBe(other)` asks the rule even though it binds to the enumerable overload:

<!-- snippet: ShouldBeExamples.EnumerableTypeWithEqualsOverride.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.EnumerableTypeWithEqualsOverride.codeSample.approved.cs'></a>
```cs
var bag = new IdBag(1, 1, 2, 3);
bag.ShouldBe(new IdBag(2, 1, 2, 3));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.EnumerableTypeWithEqualsOverride.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.EnumerableTypeWithEqualsOverride.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.EnumerableTypeWithEqualsOverride.exceptionText.approved.txt -->
```
bag
    should be
[1, 2, 3]
    but was
[1, 2, 3]
    difference
[1, 2, 3]
```
<!-- endInclude -->

The message still lists the values' items, so the two sides can look identical: the failure comes from the rule.

When several rules apply, the one for the most specific type wins. The rule is called with the expected value first, and never with `null`. You can also register an `IEqualityComparer<T>`: `ShouldlyConfiguration.UseEquality(myComparer)`. For a one-off comparison without a global rule, pass a comparer instead: `bag.ShouldBe(other, EqualityComparer<IdBag>.Default)`.

#### Newtonsoft.Json

`JToken` (and so `JObject`, `JArray` and `JValue`) is enumerable, and only `JValue` overrides `Equals`. Compared element by element, two tokens are equal whenever they have the same shape, so `{"a":1}` equals `{"b":2}`. Register `JToken.DeepEquals` to compare them as JSON:

```csharp
[ModuleInitializer]
internal static void Initialize() =>
    ShouldlyConfiguration.UseEquality<JToken>(JToken.DeepEquals);
```

## Enumerables of Numerics

If you have enumerables of `float`, `decimal` or `double` types then you can use the tolerance overloads, similar to the value extensions.

<!-- snippet: ShouldBeExamples.EnumerablesOfNumerics.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.EnumerablesOfNumerics.codeSample.approved.cs'></a>
```cs
var firstSet = new[] { 1.23m, 2.34m, 3.45001m };
var secondSet = new[] { 1.4301m, 2.34m, 3.45m };
firstSet.ShouldBe(secondSet, 0.1m);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.EnumerablesOfNumerics.codeSample.approved.cs#L1-L3' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.EnumerablesOfNumerics.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.EnumerablesOfNumerics.exceptionText.approved.txt -->
```
firstSet
    should be within
0.1m
    of
[1.4301m, 2.34m, 3.45m]
    but was
[1.23m, 2.34m, 3.45001m]
    difference
[*1.23m*, 2.34m, *3.45001m*]
```
<!-- endInclude -->


## Enumerables of Strings

Enumerables of `string` can be compared with a `Case`. Add `ignoreOrder: true` to also ignore the order of the elements.

<!-- snippet: ShouldBeExamples.EnumerablesOfStrings.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.EnumerablesOfStrings.codeSample.approved.cs'></a>
```cs
var simpsons = new[] { "Homer", "marge", "Bart" };
simpsons.ShouldBe(["bart", "HOMER", "Lisa"], Case.Insensitive, ignoreOrder: true);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.EnumerablesOfStrings.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.EnumerablesOfStrings.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.EnumerablesOfStrings.exceptionText.approved.txt -->
```
simpsons
    should be (ignoring order, case insensitive comparison)
["bart", "HOMER", "Lisa"]
    but
simpsons
    is missing
["Lisa"]
    and
["bart", "HOMER", "Lisa"]
    is missing
["marge"]
```
<!-- endInclude -->


## Bools

<!-- snippet: ShouldBeExamples.BooleanExample.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.BooleanExample.codeSample.approved.cs'></a>
```cs
const bool myValue = false;
myValue.ShouldBe(true, "Some additional context");
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.BooleanExample.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.BooleanExample.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.BooleanExample.exceptionText.approved.txt -->
```
myValue
    should be
True
    but was
False

Additional Info:
    Some additional context
```
<!-- endInclude -->
