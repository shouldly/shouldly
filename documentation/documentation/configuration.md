# Configuration

Shouldly has a few configuration options:


## DefaultFloatingPointTolerance

Allows specifying a floating point tolerance for all assertions

**Default value:** 0.0d


## DefaultTaskTimeout

`Should.Throw(Func<Task>)` blocks, the timeout is a safeguard for deadlocks.

Shouldly runs the lambda without a synchronisation context, but deadlocks are still possible. Use `Should.ThrowAsync` to be safe then await the returned task to prevent possible deadlocks.

**Default value:** 10 seconds


## CompareAsObjectTypes

Full names of enumerable types that `ShouldBe` compares with their own `Equals` rather than element by element.

An example is `Newtonsoft.Json.Linq.JToken`, which looks like this: `class JToken : IEnumerable<JToken>`. Register your own types once, before any test runs, for example from a module initializer:

```csharp
ShouldlyConfiguration.CompareAsObjectTypes.Add(typeof(IdBag).FullName!);
```

See [Enumerable types that override `Equals`](equality/shouldBe.md#enumerable-types-that-override-equals) for when it applies.

**Default value:** Newtonsoft.Json.Linq.JToken


## MaxStringLengthInMessages

How many characters of the actual and expected values a string assertion echoes back in its
failure message. Longer values are truncated, and the message says so along with the value's
full length:

```
actual
    should be
"Lorem ipsum dolor sit amet" (truncated to 1000 of 42317 characters, see ShouldlyConfiguration.MaxStringLengthInMessages)
```

This only bounds the verbatim echo. The `difference` section is always computed from the full,
untruncated values, so lowering this can never hide a difference — it just trims the surrounding
noise. Raise it when you want more of the raw value in the message:

```csharp
ShouldlyConfiguration.MaxStringLengthInMessages = 20000;
```

Scoped to the logical call context, so it flows through `async`/`await` and concurrent tests get
their own value.

**Default value:** 1000

Note that the `difference` section has its own fixed limits, independent of this setting: it shows
at most 3 differing regions, at most 20 changed lines per side in line mode, and windows each
region to roughly 60 characters of surrounding context. `ShouldContain` and friends separately clip
the searched string to 100 characters, since the useful information there is the substring, not the
haystack.


## MaxEnumerableItemsInMessages

How many items of a collection or sequence a failure message shows. Longer ones are cut off and
end with `...`:

```
naturals
    should be
[0, 1]
    but was
[0, 1, 2, 3, 4, ...]
    difference
[0, 1, *2*, *3*, *4*, ...]
```

This also bounds how far Shouldly reads a sequence to describe it, so a failing assertion on an
infinite or very long lazy sequence returns promptly instead of hanging. Unlike
`MaxStringLengthInMessages`, it applies to the `difference` section too, since a sequence may never
end: a difference past the limit is not shown. Raise it when you need to see further into long
collections:

```csharp
ShouldlyConfiguration.MaxEnumerableItemsInMessages = 5000;
```

Scoped to the logical call context, so it flows through `async`/`await` and concurrent tests get
their own value.

**Default value:** 1000


## DiffStyle

Character set used for the markers that point at a difference. `Unicode` uses `▼`/`▲`, `Ascii`
uses `v`/`^` for terminals that can't render the arrows.

**Default value:** `DiffStyle.Unicode`


## EscapeStyle

How control characters are rendered in difference output.

| Value              | `\r\n` renders as |
| ------------------ | ----------------- |
| `CStyle`           | `\r`, `\n`        |
| `ControlPictures`  | `␍`, `␊`          |
| `Descriptive`      | `<CR>`, `<LF>`    |

Scoped to the logical call context, so it flows through `async`/`await` and concurrent tests get
their own value.

**Default value:** `EscapeStyle.CStyle`
