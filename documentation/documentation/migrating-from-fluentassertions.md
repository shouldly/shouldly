# Migrating from FluentAssertions

This guide is for teams moving from [FluentAssertions](https://fluentassertions.com/) to Shouldly,
and targets Shouldly 5.x.

Most of a migration is mechanical: `value.Should().Be(x)` becomes `value.ShouldBe(x)`. The value
is in the handful of places where the two libraries behave differently. Those are the cases that
make a green FA test fail, or silently pass, after a rename. This guide focuses on them.


## The mental model

| | FluentAssertions | Shouldly |
|---|---|---|
| Import | `using FluentAssertions;` | `using Shouldly;` |
| Entry point | `value.Should().Be(x)` | `value.ShouldBe(x)` |
| Failure type | the host test framework's assert exception (or `AssertionFailedException`) | always `ShouldAssertException` |
| Chaining | `.And` / `.Which` fluent chain | separate statements (some assertions return a value) |

There is no `.Should()` gateway. Every assertion is an extension method directly on the value,
named `Should…`. Most return `void`. A few return something useful for a follow-up assertion:

```csharp
InvalidOperationException ex = action.ShouldThrow<InvalidOperationException>();
Cat cat = animal.ShouldBeOfType<Cat>();          // also ShouldBeAssignableTo<T>
string name = maybeNull.ShouldNotBeNull();       // returns the non-null value
Order only = orders.ShouldHaveSingleItem();      // returns the single element
```

Shouldly puts the code you asserted on into the failure message. In v5 that expression is captured
by the compiler (`CallerArgumentExpression`), so you get the source text in the message without any
runtime source lookup:

```csharp
var result = Add(2, 2);
result.ShouldBe(5);
// -> result should be 5 but was 4
```


## Quick reference

Direct name mappings. Where a mapping is not a plain rename, a note in parentheses says what
changed.

### Equality and identity

| FluentAssertions | Shouldly |
|---|---|
| `x.Should().Be(y)` | `x.ShouldBe(y)` |
| `x.Should().NotBe(y)` | `x.ShouldNotBe(y)` |
| `x.Should().BeNull()` | `x.ShouldBeNull()` |
| `x.Should().NotBeNull()` | `x.ShouldNotBeNull()` |
| `x.Should().BeTrue()` | `x.ShouldBeTrue()` |
| `x.Should().BeFalse()` | `x.ShouldBeFalse()` |
| `x.Should().BeSameAs(y)` | `x.ShouldBeSameAs(y)` |
| `x.Should().NotBeSameAs(y)` | `x.ShouldNotBeSameAs(y)` |
| `x.Should().BeOfType<T>()` | `x.ShouldBeOfType<T>()` |
| `x.Should().NotBeOfType<T>()` | `x.ShouldNotBeOfType<T>()` |
| `x.Should().BeAssignableTo<T>()` | `x.ShouldBeAssignableTo<T>()` |
| `x.Should().BeOneOf(a, b)` | `x.ShouldBeOneOf([a, b])` (takes an array, not `params`) |
| `x.Should().BeEquivalentTo(y)` | `x.ShouldBeEquivalentTo(y)` (see [caveats](#object-equivalence-shouldbeequivalentto)) |

### Comparisons and ranges

| FluentAssertions | Shouldly |
|---|---|
| `x.Should().BeGreaterThan(y)` | `x.ShouldBeGreaterThan(y)` |
| `x.Should().BeGreaterThanOrEqualTo(y)` | `x.ShouldBeGreaterThanOrEqualTo(y)` |
| `x.Should().BeLessThan(y)` | `x.ShouldBeLessThan(y)` |
| `x.Should().BeLessThanOrEqualTo(y)` | `x.ShouldBeLessThanOrEqualTo(y)` |
| `x.Should().BePositive()` | `x.ShouldBePositive()` |
| `x.Should().BeNegative()` | `x.ShouldBeNegative()` |
| `x.Should().BeInRange(lo, hi)` | `x.ShouldBeInRange(lo, hi)` |
| `x.Should().NotBeInRange(lo, hi)` | `x.ShouldNotBeInRange(lo, hi)` |
| `date.Should().BeCloseTo(y, precision)` | `date.ShouldBe(y, tolerance)` |
| `date.Should().BeAfter(y)` | `date.ShouldBeGreaterThan(y)` |
| `date.Should().BeBefore(y)` | `date.ShouldBeLessThan(y)` |
| `num.Should().BeApproximately(y, tol)` | `num.ShouldBe(y, tol)` |

### Collections

| FluentAssertions | Shouldly |
|---|---|
| `c.Should().Contain(item)` | `c.ShouldContain(item)` |
| `c.Should().NotContain(item)` | `c.ShouldNotContain(item)` |
| `c.Should().Contain(x => …)` | `c.ShouldContain(x => …)` |
| `c.Should().BeEmpty()` | `c.ShouldBeEmpty()` |
| `c.Should().NotBeEmpty()` | `c.ShouldNotBeEmpty()` |
| `c.Should().HaveCount(n)` | `c.ShouldHaveCount(n)` |
| `c.Should().HaveCountGreaterThan(n)` | `c.Count.ShouldBeGreaterThan(n)` (no `ShouldHaveCountGreaterThan`) |
| `c.Should().Equal(a, b, c)` | `c.ShouldBe([a, b, c])` (order-sensitive) |
| `c.Should().BeEquivalentTo(other)` | `c.ShouldBeEquivalentTo(other, new EquivalencyOptions { IgnoreOrder = true })` (structural + unordered; [see below](#collections-order-and-element-equality)) |
| `c.Should().OnlyHaveUniqueItems()` | `c.ShouldBeUnique()` (not `ShouldAllBeUnique`) |
| `c.Should().OnlyContain(x => …)` | `c.ShouldAllBe(x => …)` |
| `c.Should().AllBeAssignableTo<T>()` | `c.ShouldAllBe(x => x is T)` (no `ShouldAllBeAssignableTo`) |
| `c.Should().AllSatisfy(x => x.Should()…)` | `Should.Satisfy([.. c.Select(x => new Action(() => …))])` (assertion per element, all failures aggregated; [see below](#per-element-assertions-allsatisfy)) |
| `c.Should().ContainSingle()` | `c.ShouldHaveSingleItem()` |
| `c.Should().ContainSingle().Which.Should().Be(v)` | `c.ShouldHaveSingleItem().ShouldBe(v)` |
| `c.Should().BeSubsetOf(other)` | `c.ShouldBeSubsetOf(other)` |
| `c.Should().BeInAscendingOrder()` | `c.ShouldBeInOrder()` |
| `c.Should().BeInDescendingOrder()` | `c.ShouldBeInOrder(SortDirection.Descending)` |
| `c.Should().ContainInOrder(a, b)` | no equivalent; [see below](#no-drop-in-for-containinorder-or-containequivalentof) |
| `c.Should().ContainEquivalentOf(item)` | no equivalent for collections; [see below](#no-drop-in-for-containinorder-or-containequivalentof) |

### Dictionaries

| FluentAssertions | Shouldly |
|---|---|
| `d.Should().ContainKey(k)` | `d.ShouldContainKey(k)` |
| `d.Should().NotContainKey(k)` | `d.ShouldNotContainKey(k)` |
| `d.Should().Contain(k, v)` | `d.ShouldContainKeyAndValue(k, v)` |
| `d.Should().ContainValue(v)` | `d.Values.ShouldContain(v)` (no `ShouldContainValue`) |
| `d.Should().NotContainValue(v)` | `d.Values.ShouldNotContain(v)` (no `ShouldNotContainValue`) |

### Strings

| FluentAssertions | Shouldly |
|---|---|
| `s.Should().Be("x")` | `s.ShouldBe("x")` (exact, case-sensitive) |
| `s.Should().Contain("x")` | `s.ShouldContain("x")` ([case-sensitive by default](#strings-are-case-sensitive-by-default), matches FA) |
| `s.Should().ContainEquivalentOf("x")` | `s.ShouldContain("x", Case.Insensitive)` (FA ignores case, so pass `Case.Insensitive`) |
| `s.Should().StartWith("x")` | `s.ShouldStartWith("x")` (case-sensitive by default, matches FA) |
| `s.Should().EndWith("x")` | `s.ShouldEndWith("x")` (case-sensitive by default, matches FA) |
| `s.Should().Match("re*ex")` | `s.ShouldMatch(regex)` (Shouldly takes a regex, FA `Match` takes a wildcard) |
| `s.Should().MatchRegex("re.ex")` | `s.ShouldMatch("re.ex")` |
| `s.Should().BeNullOrEmpty()` | `s.ShouldBeNullOrEmpty()` |
| `s.Should().BeNullOrWhiteSpace()` | `s.ShouldBeNullOrWhiteSpace()` |

### Exceptions

| FluentAssertions | Shouldly |
|---|---|
| `act.Should().Throw<T>()` | `act.ShouldThrow<T>()` |
| `act.Should().ThrowExactly<T>()` | `act.ShouldThrow<T>()` then `ex.ShouldBeOfType<T>()` (no `ShouldThrowExactly`) |
| `act.Should().NotThrow()` | `act.ShouldNotThrow()` |
| `await act.Should().NotThrowAsync()` | `await act.ShouldNotThrowAsync()` |
| `(await act.Should().ThrowAsync<T>())` | `await act.ShouldThrowAsync<T>()` |
| `act.Should().Throw<T>().WithMessage("x*")` | `act.ShouldThrow<T>().Message.ShouldContain("x")` (no `WithMessage`) |


## Behavioral differences to watch for

These are the traps. Read them before you trust a bulk find-and-replace.

### Strings are case-sensitive by default

Shouldly's string `ShouldContain`, `ShouldStartWith`, `ShouldEndWith` (and their `Not…` forms)
are case-sensitive by default, matching FluentAssertions' `Contain`, `StartWith`, and `EndWith`,
so a straight rename preserves the behavior:

```csharp
"Hello".ShouldContain("hello");     // fails, case matters
"Hello".ShouldStartWith("HELLO");   // fails, case matters
```

```text
"Hello"
    should contain
"hello"
    but did not
```

When you do want a case-insensitive comparison — for example FA's `ContainEquivalentOf`, which
ignores case — pass `Case.Insensitive`:

```csharp
"Hello".Should().ContainEquivalentOf("hello");    // FA, ignores case
"Hello".ShouldContain("hello", Case.Insensitive); // Shouldly equivalent
```

### `ShouldBe` is strongly typed

FluentAssertions compares through `object`, so it happily accepts mismatched types and converts
them. Shouldly's `ShouldBe<T>` requires the actual and expected values to be the same type, so
mismatches are caught by the compiler, not at runtime:

```csharp
int i = 1;
i.ShouldBe(1L);            // does NOT compile: int vs long

decimal d = 1m;
decimal? n = 1m;
d.ShouldBe(n);             // does NOT compile: decimal vs decimal?
```

This is usually a good thing, since it turns sloppy tests into compile errors, but it means some FA
assertions that compiled will not. Fix the types (cast, or make both sides `T?`) rather than
fighting it.

### Collections: order and element equality

`ShouldBe` on a collection is order-sensitive and compares elements with their normal equality:

```csharp
new[] { 1, 2, 3 }.ShouldBe([3, 2, 1]);                    // fails: order differs
new[] { 1, 2, 3 }.ShouldBe([3, 2, 1], ignoreOrder: true); // passes
```

So the FluentAssertions collection methods map like this:

- `Should().Equal(…)` (ordered, element equality) becomes `ShouldBe(…)`
- `Should().BeEquivalentTo(…)` (unordered, structural) becomes
  `ShouldBeEquivalentTo(…, new EquivalencyOptions { IgnoreOrder = true })`

`ShouldBe(…, ignoreOrder: true)` also compares order-insensitively, but it matches elements by their
`Equals`, so it only lines up with FA's collection `BeEquivalentTo` when the elements are values (or
override equality). For collections of reference types that should be compared *structurally*, use
`ShouldBeEquivalentTo` with `IgnoreOrder` (see the
[object equivalence](#object-equivalence-shouldbeequivalentto) section).

Because `ShouldBe` uses each element's `Equals`, a collection of reference types that don't override
equality is compared by reference:

```csharp
var a = new List<Dog> { new Dog { Name = "Rex" } };
var b = new List<Dog> { new Dog { Name = "Rex" } };
a.ShouldBe(b);              // fails: different instances
a.ShouldBeEquivalentTo(b); // passes: compares member by member
```

### Chaining: `.And` and `.Which`

FluentAssertions chains with `.And` and drills in with `.Which`. Shouldly has neither. Split a
`.And` chain into separate statements, and for `.Which` use the value that some assertions return.

```csharp
// FA
markup.Should().Contain("mud-card").And.Contain("mud-elevation-1");
result.Should().ContainSingle().Which.Should().Be(5);

// Shouldly
markup.ShouldContain("mud-card");
markup.ShouldContain("mud-elevation-1");
result.ShouldHaveSingleItem().ShouldBe(5);
```

(`ShouldContain` on a string is case-sensitive, matching FA's `.Contain`; add `Case.Insensitive`
if you want to ignore case.)

### Custom messages (`because`)

Every FluentAssertions assertion accepts a reason with format arguments, which FA weaves into the
failure sentence. The Shouldly counterpart is the `customMessage` parameter, a plain string with
no format arguments, so use interpolation:

```csharp
// FA
count.Should().Be(3, "the cache warms {0} entries on startup", entries);

// Shouldly
count.ShouldBe(3, $"the cache warms {entries} entries on startup");
```

The message is appended to the failure output under "Additional Info":

```text
count
    should be
3
    but was
2

Additional Info:
    the cache warms 42 entries on startup
```

### Per-element assertions (`AllSatisfy`)

FA's `AllSatisfy` runs an assertion action against every element and reports every failing
element at once. It is not the same as `ShouldAllBe`, which takes a boolean predicate. Map each
one to the right tool:

```csharp
// FA, a boolean predicate: use ShouldAllBe
items.Should().OnlyContain(x => x.IsActive);
items.ShouldAllBe(x => x.IsActive);

// FA, an assertion action per element: project each element into a condition and pass them to
// Should.Satisfy, which runs them all and aggregates every failure just like AllSatisfy.
inputs.Should().AllSatisfy(i => i.GetAttribute("type").Should().Be("checkbox"));
Should.Satisfy([.. inputs.Select(i => new Action(() => i.GetAttribute("type").ShouldBe("checkbox")))]);
```

If you do not need the aggregated report, a plain `foreach` of assertions also works, but it stops
at the first failing element rather than listing them all:

```csharp
foreach (var i in inputs)
    i.GetAttribute("type").ShouldBe("checkbox");
```

### No drop-in for `ContainInOrder` or `ContainEquivalentOf`

Two collection assertions have no Shouldly counterpart:

- `ContainInOrder(a, b, c)` asserts the items appear in that relative order (gaps allowed). There
  is no built-in; a small local helper covers it:
  ```csharp
  static void ShouldContainInOrder<T>(IEnumerable<T> actual, params T[] expected)
  {
      var list = actual.ToList();
      var idx = -1;
      foreach (var e in expected)
      {
          var next = list.FindIndex(idx + 1, x => EqualityComparer<T>.Default.Equals(x, e));
          next.ShouldBeGreaterThan(idx, $"expected '{e}' after index {idx}, in order");
          idx = next;
      }
  }
  ```
- `ContainEquivalentOf(item)` on a collection (structural match of an element) has no equivalent.
  Assert with `ShouldContain(x => …)` on the members you care about, or loop. On a string it just
  means a case-insensitive substring, which is `ShouldContain(x, Case.Insensitive)`.


## Object equivalence: `ShouldBeEquivalentTo`

`ShouldBeEquivalentTo` walks the object graph and compares public fields and properties recursively.
Like FA's `BeEquivalentTo`, it is **direction-sensitive**: comparison is driven by the *expected*
value's members, and any extra members the actual value carries are ignored. It does **not** require
the two sides to be the same type, so two structurally identical objects of different types are
equivalent:

```csharp
var actual   = new Dto      { Name = "Bob", Age = 30 };
var expected = new Customer { Name = "Bob", Age = 30 };

actual.ShouldBeEquivalentTo(expected);   // passes: same members, same values
```

Because the expectation drives member selection, you assert a **subset** by passing an anonymous
type (or any type) that carries only the members you care about. This is the direct replacement for
FA's "project to a shape and compare" pattern:

```csharp
var person = new Person { Name = "Bob", Age = 30, Internal = "secret" };

person.ShouldBeEquivalentTo(new { Name = "Bob", Age = 30 });   // passes: Internal is not checked
```

A member present on the expected value but missing on the actual value is a failure, and every
difference is collected rather than stopping at the first:

```csharp
var dto  = new Dto            { Name = "Bob", Age = 30 };
var full = new PersonExtended { Name = "Bob", Age = 30, Extra = "x" };

full.ShouldBeEquivalentTo(dto);   // passes: dto's members all match; full's Extra is ignored
dto.ShouldBeEquivalentTo(full);   // fails: full expects an Extra member dto doesn't have
```

```text
Comparing object equivalence, at path:
dto [PersonExtended]
    Extra

    Expected a public member named
"Extra"
    but was not found on
Dto
```

### Options

A second overload takes an `EquivalencyOptions`:

```csharp
actual.ShouldBeEquivalentTo(expected, new EquivalencyOptions
{
    IgnoreOrder = true,                          // compare collections order-insensitively
    MembersToIgnore = { "CreatedAt", "Id" },     // skip these members anywhere in the graph
});
```

- `MembersToIgnore` is the counterpart to FA's `.Excluding(...)`, matched by member name anywhere in
  the graph.
- `IgnoreOrder` makes sequences compare order-insensitively; sets and dictionaries are always
  compared unordered/by key.

### What still differs from FA

- **Sequences are ordered by default.** Opt into `IgnoreOrder` for FA's unordered collection
  behavior.
- **`Equals` overrides on complex types are ignored** — comparison is always member-wise, so a type
  with a custom `Equals` is still compared property-by-property. (Well-known value-semantic types
  such as `string`, `Guid`, `DateTime`, and `Uri` are treated as leaves and compared with `Equals`.)
- **Comparers and tolerances** — a fluent `.WithAutoConversion()` switch, `.Using<T>(...)` custom
  comparers, or approximate numeric/`DateTime` matching — are not exposed as options yet. Numeric
  *leaves* are auto-converted across kinds, so `int` `5` is equivalent to `long` `5` or `double`
  `5.0`.

For the rare test that needs an FA feature with no counterpart today (custom comparison rules,
member selection by predicate), assert the members individually with
[`ShouldSatisfy`](satisfyAllConditions.md) so you still get every failure at once, or keep a
dedicated equivalence library for those cases.


## Exceptions and messages

`ShouldThrow<T>()` returns the caught exception, so assert on its message directly. There is no
`WithMessage`:

```csharp
var ex = Should.Throw<InvalidOperationException>(() => Widget.Spin());
ex.Message.ShouldContain("jammed");     // substring, like FA's WithMessage("*jammed*")
ex.Message.ShouldBe("Widget jammed");   // exact match
```

Async is the same shape. `ShouldThrowAsync<T>` returns a `Task<T>`, so `await` it:

```csharp
var ex = await Should.ThrowAsync<InvalidOperationException>(() => widget.SpinAsync());
ex.Message.ShouldContain("jammed");
```

Like FA's `Throw<T>`, `ShouldThrow<T>` matches derived exception types: asserting
`ShouldThrow<ArgumentException>()` is satisfied by an `ArgumentNullException`. There is no
`ThrowExactly`; if you need an exact type, check it explicitly:

```csharp
var ex = act.ShouldThrow<ArgumentException>();
ex.ShouldBeOfType<ArgumentException>();   // fails for the derived ArgumentNullException
```


## Grouping assertions (FA's `AssertionScope`)

FluentAssertions uses `using (new AssertionScope())` to report several failures together. Shouldly
does not have assertion scopes; use [`ShouldSatisfy`](satisfyAllConditions.md) (or the static
`Should.Satisfy` for unrelated conditions), which runs every condition and reports all failures at
once:

```csharp
person.ShouldSatisfy(
[
    p => p.Name.ShouldBe("Alice"),
    p => p.Age.ShouldBeGreaterThan(0),
]);
```

```text
person
    should satisfy all the conditions specified, but does not.
The following errors were found ...
---------------- Error 1 ----------------
    p.Name
        should be
    "Alice"
        but was
    "Bob"
...
```

`ShouldSatisfyAllConditions` still exists but is obsolete in v5: it can't capture the asserted
expression and isn't trimming/AOT-safe. Prefer `ShouldSatisfy` or `Should.Satisfy`.


## Features without a direct equivalent

| FluentAssertions | Shouldly today |
|---|---|
| `BeEquivalentTo(…).Excluding(…)` | `ShouldBeEquivalentTo(…, new EquivalencyOptions { MembersToIgnore = { … } })` |
| `BeEquivalentTo(…)` with custom comparers / `.WithAutoConversion` | No fluent equivalent yet; assert members with `ShouldSatisfy`, or use an anonymous-type subset ([see above](#object-equivalence-shouldbeequivalentto)) |
| `ThrowExactly<T>()` | `ShouldThrow<T>()` + `ex.ShouldBeOfType<T>()` |
| `.WithMessage("x*")` | `ex.Message.ShouldContain("x")` or `ShouldMatch(regex)` |
| `AssertionScope` | `ShouldSatisfy` or `Should.Satisfy` |
| `.And` / `.Which` chaining | Separate statements, or use the value a `Should…` returns |
| `SatisfyRespectively(…)` | `ShouldSatisfy` with one condition per element (indexed manually) |
| `ContainInOrder(…)` / `ContainEquivalentOf(…)` | [see above](#no-drop-in-for-containinorder-or-containequivalentof) |
| `Implement<TInterface>()` | `typeof(IFoo).IsAssignableFrom(typeof(MyType)).ShouldBeTrue()` |
