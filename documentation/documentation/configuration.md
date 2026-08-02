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

Types which also are IEnumerable of themselves.

An example is `Newtonsoft.Json.Linq.JToken` which looks like this `class JToken : IEnumerable<JToken>`.

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
