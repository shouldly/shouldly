# Writing Custom Assertions

Shouldly is designed to be extended with your own domain-specific assertions. Since v5 a custom assertion is just an extension method — no attributes or registration required. This page covers the patterns, from simplest to most control.

## The anatomy of an assertion

Shouldly's error messages read like a sentence built from three parts: the source text of the value being asserted, the assertion method's name, and the expected/actual values:

```
person.Manager
    should be awesome
"an awesome person"
    but was
null
```

The first line is the **caller argument expression** — the literal source text `person.Manager`, captured at compile time by `[CallerArgumentExpression]`. The second line is the **phrase**: the assertion method's name (`ShouldBeAwesome` → "should be awesome"), captured by `[CallerMemberName]`. Your custom assertion gets all of this for free as long as it declares and forwards the right parameters.

## The basic pattern

A custom assertion declares the value being asserted, an optional `customMessage`, and a `[CallerArgumentExpression]` parameter that captures the call-site text. When the check fails, pass those three to `ShouldAssertException.For` in the same order and throw the result:

```csharp
public static class CustomAssertions
{
    public static void ShouldBeAwesome(
        this Person? actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (actual is not { IsAwesome: true })
            throw ShouldAssertException.For(actual, customMessage, actualExpression)
                .Expected("an awesome person");
    }
}
```

`For` returns a `ShouldlyFailure`, which is a `ShouldAssertException`. Shouldly supplies the subject (`person.Manager`), the phrase (`ShouldBeAwesome` → "should be awesome"), value formatting, difference highlighting between an expected and an actual value, and the `Additional Info` section for `customMessage`.

Always `throw` the failure. `For` only builds it, so a failure that isn't thrown lets the assertion pass.

Values are formatted as you pass them in, so the message is unaffected if a value is disposed or changed after the assertion throws, for example by a `using` block around the assertion.

The message reads a sequence once, and no further than it shows. Your own check reads the sequence before the message does, so if it may only be enumerable once, such as a LINQ query over a data reader, call `ToList()` on it before checking it and pass the list on.

## Controlling the message

By default the message shows the subject, the phrase, and the actual value. Add to the same expression to change that:

| Call | Effect |
| --- | --- |
| `.Expected(value)` | Adds the expected value, formatted by Shouldly. Leave it off for no expected line. |
| `.Phrase("should have error")` | States the phrase instead of taking it from the method name. |
| `.ExpectedInWords("at least 1 element")` | Describes what was expected in words, shown as written. |
| `.ActualInWords("no value at '/items'")` | Describes what was found in words, in place of the actual value. |
| `.WithoutActual()` | Leaves the actual value out. |
| `.Context("Response", text)` | Appends a labelled block after the message. Repeatable. |

For example, an assertion on an HTTP response can say what it was looking for in its own words and attach the response:

```csharp
public static void ShouldHaveError(
    this HttpResponse actual,
    string field,
    string? customMessage = null,
    [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
{
    var errors = ReadErrors(actual);
    if (!errors.ContainsKey(field))
        throw ShouldAssertException.For(errors.Keys, customMessage, actualExpression)
            .Phrase("should have an error for")
            .Expected(field)
            .Context("Response", Describe(actual));
}
```

```
response
    should have an error for
"name"
    but was
["email", "age"]

Response:
    HTTP/1.1 400 Bad Request
    {"errors":{"email":["required"],"age":["must be positive"]}}
```

A phrase containing the word "not", such as "should not be empty", marks the assertion as negated. A negated assertion fails because the values matched, so its message highlights no difference.

Shouldly keeps the subject, the formatting of values and the layout of the message. If you need a line the controls don't offer, describe it with `ExpectedInWords`, `ActualInWords` or a `Context` block rather than building the whole message as a string.

## Composing existing assertions

Often a custom assertion is just a bundle of existing ones. Forward your captured expression into the inner calls so the failure message points at the *caller's* code rather than your helper's parameter:

```csharp
public static void ShouldBeValidOrder(
    this Order? actual,
    string? customMessage = null,
    [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
{
    actual.ShouldNotBeNull(customMessage, actualExpression: actualExpression);
    actual.Lines.ShouldNotBeEmpty(customMessage, actualExpression: $"{actualExpression}.Lines");
    actual.Total.ShouldBeGreaterThan(0m, customMessage, actualExpression: $"{actualExpression}.Total");
}
```

Without the forwarding, a failure inside `ShouldBeValidOrder(order)` would report the expression as `actual` — with it, the message says `order.Total`.

## Predicate-style assertions with `AssertAwesomely`

For assert-a-predicate cases, `ShouldlyCoreExtensions.AssertAwesomely` runs the check and reports an expected and an actual value in one call:

```csharp
public static void ShouldBePositive(
    this Money actual,
    string? customMessage = null,
    [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
{
    actual.AssertAwesomely(m => m.Amount > 0, actual, "a positive amount",
        customMessage, actualExpression: actualExpression);
}
```

## The message classes

Assertions written for v4 construct a message class and pass its text to `ShouldAssertException`. These keep working:

```csharp
throw new ShouldAssertException(
    new ExpectedActualShouldlyMessage("an awesome person", actual,
        customMessage, actualExpression: actualExpression).ToString());
```

> **Pass `actualExpression` by name.** The message constructors and `AssertAwesomely` take a `[CallerMemberName]` parameter *before* `actualExpression` — passing it positionally silently clobbers the method name instead. `ShouldAssertException.For` has no such trap.

| Class | Message shape |
| --- | --- |
| `ExpectedShouldlyMessage` | `x should … expected but does not` |
| `ActualShouldlyMessage` | `x should … but was actual` |
| `ExpectedActualShouldlyMessage` | `x should … expected but was actual` |
| `ExpectedActualToleranceShouldlyMessage` | `x should be within tolerance of expected but was actual` |
| `ExpectedActualWithCaseSensitivityShouldlyMessage` | adds the case option to the phrase |
| `ShouldlyThrowMessage` | `` `x` should throw expected but threw actual `` |
| `ExpectedEquivalenceShouldlyMessage` | the path to a difference between two object graphs |

A method named exactly like one of Shouldly's own assertions gets that assertion's wording, so a `ShouldBeNull` for your own type reads like the built-in one. Any other name gets the shape above, with the phrase taken from the method name. The tolerance and case-sensitivity classes always show their option: if the built-in wording has no place for it, they keep the shape above.

Like `For`, the message classes and `AssertAwesomely` read a sequence once, and no further than the message shows.

For new assertions prefer `ShouldAssertException.For`, which gives you control over the message that these classes don't.

## Verifying your wiring with the trip-wire

Forgetting the `[CallerArgumentExpression]` parameter — or forgetting to forward it — does not fail any test; it just silently degrades your failure messages. Shouldly ships the guard it uses on its own test suite: arm `ShouldlyConfiguration.AssertCallerArgumentExpressionIsUsed()` once for the test run, and any Shouldly message built without a captured expression throws `InvalidOperationException` instead of degrading:

```csharp
internal static class ModuleInitializer
{
    [ModuleInitializer]
    internal static void Initialize() =>
        _ = ShouldlyConfiguration.AssertCallerArgumentExpressionIsUsed();
}
```

Call sites that legitimately cannot use caller argument expressions (e.g. assertions invoked through `dynamic`) can opt out locally with `using (ShouldlyConfiguration.AllowStackWalking()) { … }`.

## Wrapping `ShouldMatchApproved`

`ShouldMatchApproved` names and places its approval files using the calling test method, captured via `[CallerMemberName]` and `[CallerFilePath]`. A helper that wraps it must capture those itself and pass them through, otherwise the files are named after the helper:

```csharp
public static void ShouldMatchMySnapshot(
    this string actual,
    [CallerMemberName] string testMethodName = "",
    [CallerFilePath] string sourceFilePath = "") =>
    actual.ShouldMatchApproved(c => c.WithScrubber(Scrub),
        testMethodName: testMethodName, sourceFilePath: sourceFilePath);
```

## Polish

Two attributes Shouldly applies to its own assertion classes are worth copying:

- `[DebuggerStepThrough]` on the class keeps the debugger from stepping into assertion internals when a test fails.
- `[EditorBrowsable(EditorBrowsableState.Never)]` hides the static class from IntelliSense while leaving the extension methods visible on the asserted values.

### Do I still need `[ShouldlyMethods]`?

Not on modern targets. The attribute only matters to the legacy stack-walking fallback, which runs solely for `netstandard2.0` consumers whose compiler does not supply `[CallerArgumentExpression]` values. If your assertion library multi-targets and supports such consumers, keep `[ShouldlyMethods]` on the class so the fallback can skip your frames; otherwise omit it.
