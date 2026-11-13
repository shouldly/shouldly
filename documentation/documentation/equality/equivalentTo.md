# ShouldBeEquivalentTo

`ShouldBeEquivalentTo` compares two values member by member, all the way down the object graph. Where [`ShouldBe`](shouldBe.md) asks a type's own `Equals`, `ShouldBeEquivalentTo` ignores it and compares what the two values hold:

```csharp
var actual = new Customer { Name = "Homer", Address = new Address { Town = "Springfield" } };
var expected = new Customer { Name = "Homer", Address = new Address { Town = "Springfield" } };

actual.ShouldBe(expected);             // fails: Customer compares by reference
actual.ShouldBeEquivalentTo(expected); // passes: the members hold the same values
```

## How each value is compared

A comparison strategy is selected per node in the object graph:

| Node type | Strategy |
| --- | --- |
| Leaf values: primitives, enums, `string` (ordinal), `Guid`, `DateTime`/`DateTimeOffset`/`TimeSpan`, `Uri`, `Type`, delegates, any other type that ships with .NET and defines its own equality (`CultureInfo`, `IPAddress`, `Encoding`, `Complex`, `Vector3`; tuples and `Memory<T>` excepted, which are compared by their items), `System.Text.Json`'s `JsonNode` and `JsonElement` ([compared as JSON](shouldBe.md#systemtextjson)), and a type with no public members that defines its own equality (`IEquatable<T>` or an `Equals` override, such as NodaTime's `Instant`) | Compared with `Equals` on the expected value, with [exact cross-numeric equality](../upgrade/4to5.md#numbers-of-different-types-are-compared-exactly) (`int 1` ≡ `long 1` ≡ `double 1.0`, but `decimal 0.1` ≢ `double 0.1`); `float`/`double` honour [`DefaultFloatingPointTolerance`](../configuration.md#defaultfloatingpointtolerance) |
| Two values of a type with an [equality rule](shouldBe.md#equality-rules-in-shouldbeequivalentto) | Compared by the rule, as one value, whatever the type is |
| Dictionaries (`IDictionary`, `IDictionary<,>`, `IReadOnlyDictionary<,>`, immutable variants) | Matched **by key**, recursing into values |
| Sets (`ISet<>`, `IReadOnlySet<>`, immutable variants) | Order-insensitive |
| Other collections | Order-sensitive, element-by-element, recursing; container type ignored (array ≡ `List<T>`); multidimensional arrays compared by rank and dimensions. Order-insensitive against a dictionary or set, [as in `ShouldBe`](../enumerable/shouldBe.md#dictionaries-and-sets): a `HashSet<T>` has no order to compare with a list's, while a sorted or ordered collection is compared in order |
| Everything else — classes *and* structs | Member-wise and recursive over public properties and fields; indexers skipped |

A type that derives from one of .NET's collection classes and adds members of its own, such as a `List<T>` subclass with a `TotalCount`, has those members compared as well as its items. So does a grouping's `Key`. Members that .NET's own collection types and interfaces declare (`Count`, `Capacity`, `Comparer`, `Keys`, an array's `Length`) are not compared. Neither are the members of a collection type that implements the collection interfaces itself, which is compared by its items alone: nothing tells its state apart from how it holds its items or from views of them, such as a buffer's `Capacity` or a linked list's `Head` and `Tail`.

Cyclic graphs are compared without configuration, and recursion depth is unbounded.

## The expected value decides which members are compared

Member selection uses **the expectation's members, with subset semantics**: every member on the expectation must exist and match on the actual object, and extra members on the actual object are ignored. The two values needn't be the same type. So an anonymous type asserts on just the members it carries:

```csharp
person.ShouldBeEquivalentTo(new { Name = "John" });
```

To assert on the type as well, add [`ShouldBeOfType`](ofType.md).

Each expected value is compared by the members of the type it is, whatever type the member or collection holding it declares. A `Circle` in a `List<Shape>` is compared by a circle's members:

```csharp
new List<Shape> { new Circle { Label = "c", Radius = 1 } }
    .ShouldBeEquivalentTo(new List<Shape> { new Circle { Label = "c", Radius = 500 } }); // fails, at Radius
```

The strategy in the table above is chosen the same way, so a `HashSet<T>` held in a member declared as `IEnumerable<T>` is compared as a set. To ask less of a value, expect less: an anonymous type, or an instance of the base type, carries only the members to compare.

## Every difference is reported

A failure collects every difference and reports each with its path, rather than stopping at the first:

```csharp
customer.ShouldBeEquivalentTo(new { Name = "Marge", Age = 36 });
```

```
Comparing object equivalence, found 2 differences:

at path:
customer [<anonymous type>]
    Name [System.String]

    Expected value to be
"Marge"
    but was
"Homer"

at path:
customer [<anonymous type>]
    Age [System.Int32]

    Expected value to be
36
    but was
38
```

Several differences in the members of one value whose type defines its own equality, and agrees that the values differ, are reported as one difference that shows the two values as the type writes them and names the members that differ: a NodaTime `LocalDate` differs once, not at `Day`, `DayOfWeek` and `DayOfYear`. A record, a type without a `ToString` of its own, and values of two different types are reported member by member.

When values differ member by member but their type's own equality calls them equal, the failure ends by saying so, and names the [equality rule](shouldBe.md#equality-rules-in-shouldbeequivalentto) that would compare the type by its own equality.

## A comparison with nothing to compare fails

If a node has no members to compare, such as `new object()`, a struct with no public members and no equality of its own, or a type whose every member is ignored through the options, the assertion fails instead of silently passing:

```
Comparing object equivalence, at path:
new object() [System.Object]

    Found no public members to compare on
System.Object
    which would make the assertion vacuously pass; assert on specific members instead
```

A type with no public members that defines its own equality (`IEquatable<T>` or an `Equals` override) is compared by that equality instead. Otherwise, an [equality rule](shouldBe.md#equality-rules-in-shouldbeequivalentto) for the type says how to compare it.

## `EquivalencyOptions`

```csharp
actual.ShouldBeEquivalentTo(expected, new EquivalencyOptions
{
    IgnoreOrder = true,                    // order-insensitive collection comparison
    MembersToIgnore = { "Id", "Created" }, // skip these members wherever they appear
});
```

Lists and arrays are compared in order unless `IgnoreOrder` is set. Dictionaries and sets are matched by key and in any order either way.

## Equality rules

A type's own `Equals` is ignored, unless the type has no public members to walk or ships with .NET, but an equality rule registered with `ShouldlyConfiguration.UseEquality` never is: two values a rule covers are compared by the rule, as one value, wherever they sit in the graph. See [Equality rules in `ShouldBeEquivalentTo`](shouldBe.md#equality-rules-in-shouldbeequivalentto).

## Coming from FluentAssertions

[Migrating from FluentAssertions](../migrating-from-fluentassertions.md#object-equivalence-shouldbeequivalentto) lists where `ShouldBeEquivalentTo` deliberately differs from `BeEquivalentTo`.

## Trimming and AOT

`ShouldBeEquivalentTo` doesn't carry `[RequiresUnreferencedCode]`. The reflection-based comparison sits behind the `Shouldly.Equivalency.IsReflectionEnabledByDefault` feature switch (the same pattern as `System.Text.Json`); a companion source-generation package that registers AOT-safe type shapes is planned, at which point trimmed publishes can disable the switch and drop the reflection path entirely. Generated shapes cover the types known when the tests are compiled, so a derived type that only turns up at run time in a base-typed member will need registering with it, much as `[JsonDerivedType]` does for `System.Text.Json`. A value of a type with no shape will throw, naming the type, and is never compared as its base type instead.
