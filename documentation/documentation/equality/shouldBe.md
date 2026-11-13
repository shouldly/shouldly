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

### Which equality decides

A value is compared with the equality its type defines. Strings, numbers, collections and dictionaries are handled first (see below), and an [equality rule](../configuration.md#useequality) decides before anything else. Otherwise Shouldly asks the type, in this order:

1. its `IEquatable<T>`
2. its overridden `Equals(object)`
3. its `IComparable<T>` or `IComparable`, only when the type defines neither of the above
4. `object.Equals`, which for a class is reference equality

`CompareTo` says how to sort, so a type that also defines `Equals` is compared with `Equals`. A common case sorts by one key and is equal by all its fields:

```cs
class Person(string name, int age) : IComparable<Person>
{
    public int CompareTo(Person? other) => string.Compare(name, other?.Name, StringComparison.Ordinal);
    public override bool Equals(object? obj) => obj is Person p && p.Name == name && p.Age == age;
    // ...
}

new Person("Bob", 30).ShouldBe(new Person("Bob", 40));           // fails: Equals is false
((object)new Person("Bob", 30)).ShouldBe(new Person("Bob", 40)); // fails the same way
```

`IEquatable<T>` and `Equals` are found on the value itself, so for a type that defines its equality the answer doesn't depend on the type the assertion is made at: `ShouldBe<Person>`, `ShouldBe<object>` and a `Person[]` all agree.

### Tuples and key/value pairs

A tuple (`ValueTuple` or `Tuple`, of any arity) and a `KeyValuePair` are compared item by item, each item the way `ShouldBe` compares a single value, rather than with their own `Equals`. So an item that is a collection is compared element by element, and numbers compare across types:

```cs
(1, new[] { 1 }).ShouldBe((1, new[] { 1 }));   // passes
((object)(1, 2)).ShouldBe((object)(1L, 2L));   // passes
```

Records, anonymous types and structs are compared with their own `Equals`, so a record's collection member is compared by reference. Use `ShouldBeEquivalentTo` to compare them member by member.

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

### When both values read the same

Two values can print the same text and still be unequal. A record prints a list member as its type name, a boxed `BigInteger` prints like an `int`, and a class with no `Equals` is only ever equal to itself. When that happens, the message closes with a line that says why they are unequal. Here `Order` is `record Order(int Id, List<string> Lines)`:

<!-- snippet: ShouldBeExamples.ValuesThatReadTheSame.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.ValuesThatReadTheSame.codeSample.approved.cs'></a>
```cs
var order = new Order(1, ["Duff Beer"]);
order.ShouldBe(new Order(1, ["Duff Beer"]));
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.ValuesThatReadTheSame.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.ValuesThatReadTheSame.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.ValuesThatReadTheSame.exceptionText.approved.txt -->
```
order
    should be
Order { Id = 1, Lines = System.Collections.Generic.List`1[System.String] }
    but was
Order { Id = 1, Lines = System.Collections.Generic.List`1[System.String] }
    but at Lines, they are different instances, and List<String> compares by reference; use ShouldBeEquivalentTo to compare members
```
<!-- endInclude -->

The line names whatever decided the comparison:

| The message closes with | When |
| --- | --- |
| `but they are different types (expected System.Int32, actual System.Numerics.BigInteger)` | The two values have different runtime types. |
| `but they are different instances, and Cat compares by reference; use ShouldBeEquivalentTo to compare members` | The type defines no equality of its own, so a value only equals itself. The advice is left out for a type that has no public members for `ShouldBeEquivalentTo` to compare. |
| `but Money.Equals returned false` | The type's own `Equals` said so. |
| `but at Lines, …` | A record, tuple, anonymous type or struct without an `Equals` override differs in that member, for the reason that follows. |
| `but the equality rule for IdBag says they are not equal` | A [registered equality rule](#types-with-their-own-equality) decided. |
| `but the custom comparer says they are not equal` | The `IEqualityComparer<T>` you passed decided. |

Values that already read differently get no such line.


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

Enumerable comparison is done on the elements in the enumerable, so you can compare an array to a list and have it pass. A dictionary or a set is compared without regard to the order it enumerates in, against another one or against a list: a dictionary's entries are matched by key. A sorted or ordered one is compared in order against a list. See [Dictionaries and sets](../enumerable/shouldBe.md#dictionaries-and-sets).

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


### Where two enumerables first differ

The `difference` section marks each item that differs. When the first of them is past the tenth item, the message also says where it is, so you don't have to count:

<!-- snippet: ShouldBeExamples.LongEnumerables.codeSample.approved.cs -->
<a id='snippet-ShouldBeExamples.LongEnumerables.codeSample.approved.cs'></a>
```cs
var episodes = Enumerable.Range(1, 12).ToList();
episodes.ShouldBe([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13]);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldBeExamples.LongEnumerables.codeSample.approved.cs#L1-L2' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldBeExamples.LongEnumerables.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

<!-- include: ShouldBeExamples.LongEnumerables.exceptionText.approved.txt -->
```
episodes
    should be
[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13]
    but was
[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
    difference
[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, *12*]
    first difference at index 11
```
<!-- endInclude -->

When the two items there read the same, the line goes on to say [why they are unequal](#when-both-values-read-the-same), wherever in the enumerable they are: `first difference at index 0: at Lines, they are different instances, …`.

A message shows at most [`MaxEnumerableItemsInMessages`](../configuration.md#maxenumerableitemsinmessages) items. When the first difference is further on than that, the `difference` section would have nothing to mark, so the message leaves it out and says `the first difference is after the 1000 items shown`.

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

The same goes for the type's other members: a collection type that adds state of its own, such as a paged list's total count, is compared by its items alone, so two pages with the same items and different totals are equal. For a type that derives from one of .NET's collection classes, such as a `List<T>` subclass, `ShouldBeEquivalentTo` compares those members as well as the items.

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

The rule applies wherever these assertions compare two values that are both instances of the type (or of a subtype): `ShouldBe` and `ShouldNotBe`, collection elements, dictionary keys and values, and `ShouldContain`, `ShouldBeOneOf`, `ShouldBeUnique` and the other membership assertions. [`ShouldBeEquivalentTo` asks it too](#equality-rules-in-shouldbeequivalentto). A value the rule covers is never walked as a collection (unless you pass `ignoreOrder: true`, which matches its elements in any order, each by the rule), so `bag.ShouldBe(other)` asks the rule even though it binds to the enumerable overload:

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
    but the equality rule for IdBag says they are not equal
```
<!-- endInclude -->

The message lists the values' items, which can read the same on both sides, so it closes by saying that the rule decided.

When several rules apply, the one for the most specific type wins. The rule is called with the expected value first, and never with `null`. You can also register an `IEqualityComparer<T>`: `ShouldlyConfiguration.UseEquality(myComparer)`. For a one-off comparison without a global rule, pass a comparer instead: `bag.ShouldBe(other, EqualityComparer<IdBag>.Default)`.

#### Equality rules in `ShouldBeEquivalentTo`

`ShouldBeEquivalentTo` compares member by member, and ignores a type's own `Equals`, unless the type has no public members to walk, or ships with .NET (a `CultureInfo`, an `IPAddress`): then its own equality (`IEquatable<T>` or an `Equals` override) is what it is compared by. When values differ member by member but their type's own equality calls them equal, the failure ends by saying so, and names the rule that would compare the type by its own equality. When the type's own equality agrees that they differ, and they differ in several members, the failure reports them as one difference: the two values as the type writes them (`Thursday, 2 January 2020` for a NodaTime `LocalDate`), and the members that differ, rather than a difference for each of `Day`, `DayOfWeek` and `DayOfYear`. A record, a type without a `ToString` of its own, and values of two different types are reported member by member. An equality rule is the other exception: two values that a rule covers are compared by the rule, as one value, wherever they sit in the graph. That is how to compare a type that has no public members and no equality of its own, or that isn't what its members say it is:

```csharp
ShouldlyConfiguration.UseEquality<Money>((expected, actual) => expected.Equals(actual));

order.ShouldBeEquivalentTo(expectedOrder);   // order.Total, a Money, is compared by the rule
```

A failure names the rule:

```
Comparing object equivalence, at path:
order [Order]
    Total [Money]

    Expected value to be
6 GBP
    but was
5 GBP
    and the equality rule for Money says they are not equal
```

A rule replaces the member-by-member comparison of its type, everywhere, so it decides how much of a value is compared. A rule that calls two customers equal when their IDs match leaves their names and addresses uncompared in every graph that holds a customer. `IgnoreOrder` and `MembersToIgnore` don't apply inside a value a rule decides. When a rule covers only one of the two values, such as an entity compared with an anonymous type, they are compared member by member as usual.

#### Newtonsoft.Json

`JToken` (and so `JObject`, `JArray` and `JValue`) is enumerable, and only `JValue` overrides `Equals`. Compared element by element, two tokens are equal whenever they have the same shape, so `{"a":1}` equals `{"b":2}`. Register `JToken.DeepEquals` to compare them as JSON:

```csharp
[ModuleInitializer]
internal static void Initialize() =>
    ShouldlyConfiguration.UseEquality<JToken>(JToken.DeepEquals);
```

[`ShouldBeEquivalentTo` uses the rule too](#equality-rules-in-shouldbeequivalentto), so a token is compared as JSON wherever it sits in an object graph. Without the rule it can't compare a `JToken`.

#### System.Text.Json

`JsonNode` (and so `JsonObject`, `JsonArray` and `JsonValue`) and `JsonElement` need no rule. On .NET 8 and later, `ShouldBe`, `ShouldBeEquivalentTo` and the other assertions compare two of them as JSON, wherever they sit: on their own, as a member, in a collection. Property order and whitespace don't count, and a failure message shows the JSON:

```csharp
JsonNode.Parse("""{"name":"Alice","roles":["admin"]}""")
    .ShouldBe(JsonNode.Parse("""{ "roles": ["admin"], "name": "Alice" }"""));  // passes
```

```
actual
    should be
{"name":"Bob","roles":["guest"]}
    but was
{"name":"Alice","roles":["admin"]}
    difference
["name"] should be "Bob" but was "Alice"
["roles"] should be ["guest"] but was ["admin"]
```

Strings and numbers are compared as the runtime compares them: .NET 9 and later take `1.0` and `1` for the same number, and .NET 8 doesn't.

Only two JSON values are compared this way. Against a collection that isn't JSON, a `JsonArray` or a `JsonObject` is the list or dictionary of nodes it also is: `ShouldBe` compares a `JsonArray` with a `List<JsonNode>` item by item, each pair of nodes as JSON, and `ShouldBeEquivalentTo`, which takes a JSON value for a single value, says they differ.

A rule registered for `JsonNode` or `JsonElement` still decides instead. On .NET Framework, where `System.Text.Json` is a package Shouldly doesn't reference, register `JsonNode.DeepEquals` and `JsonElement.DeepEquals` as above for `ShouldBe`, and compare JSON values with `ShouldBe` rather than `ShouldBeEquivalentTo`.

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
[*1.23m*, 2.34m, 3.45001m]
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
