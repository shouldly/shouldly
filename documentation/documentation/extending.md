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

The first line is the **caller argument expression** — the literal source text `person.Manager`, captured at compile time by `[CallerArgumentExpression]`. The second line is the assertion method's name (`ShouldBeAwesome` → "should be awesome"), captured by `[CallerMemberName]`. Your custom assertion gets all of this for free as long as it declares and forwards the right parameters.

## The basic pattern

A custom assertion method needs three things: the `this` parameter being asserted, an optional `customMessage`, and a `[CallerArgumentExpression]` parameter that captures the call-site text:

```csharp
public static class CustomAssertions
{
    public static void ShouldBeAwesome(
        this Person? actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (actual is not { IsAwesome: true })
            throw new ShouldAssertException(
                new ExpectedActualShouldlyMessage("an awesome person", actual,
                    customMessage, actualExpression: actualExpression).ToString());
    }
}
```

> **Pass `actualExpression` by name.** The message constructors take a `[CallerMemberName]` parameter *before* `actualExpression` — passing it positionally silently clobbers the method name instead. Always write `actualExpression: actualExpression`.

The message classes cover the common shapes:

| Class | Message shape |
| --- | --- |
| `ExpectedShouldlyMessage` | `x should … expected but does not` |
| `ActualShouldlyMessage` | `x should … but was actual` |
| `ExpectedActualShouldlyMessage` | `x should … expected but was actual` |
| `ExpectedActualToleranceShouldlyMessage` | adds a tolerance line |
| `ExpectedActualWithCaseSensitivityShouldlyMessage` | adds case-sensitivity wording |

All take the failing method's name via `[CallerMemberName]`, so constructing them directly inside your assertion produces correctly-worded messages. Note that the method name also drives wording: a name containing `Not` is phrased as a negated assertion.

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

For assert-a-predicate cases, `ShouldlyCoreExtensions.AssertAwesomely` wraps the throw-a-message dance:

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
