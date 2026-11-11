---
status: accepted
---

# Failure message public surface in v5

Through v4, every type involved in building a failure message was public: 16 message classes, `IShouldlyAssertionContext` and `ShouldlyAssertionContext`. Wording was chosen by matching the assertion method's name against an ordered list of patterns, so Shouldly's own assertions and custom ones shared one mechanism, and neither could change without the other.

In v5, Shouldly's own assertions stop using those types and hand their facts to an internal failure message module. Custom assertions get a small public building block with a few controls over the message, the documented v4 classes keep working, and everything else leaves the public surface. v5 is the last release in which this can change.

## The building block

A custom assertion passes its own three parameters, in the order it declared them, and throws the result:

```csharp
throw ShouldAssertException.For(actual, customMessage, actualExpression)
    .Expected("an awesome person");
```

`For` returns a `ShouldlyFailure`, a sealed subclass of `ShouldAssertException`. By default the message shows the subject, a phrase derived from the calling method's name, and the actual value. The author controls five things:

| Control | Effect |
| --- | --- |
| `.Expected(value)` | Adds the expected line. Leaving it off leaves the line out. |
| `.Phrase(text)` | States the phrase instead of deriving it from the method name. |
| `.WithoutActual()` | Leaves the actual line out. |
| `.ExpectedInWords(text)`, `.ActualInWords(text)` | Describes the expected or actual in words, shown as written and not quoted. |
| `.Context(label, text)` | Appends a labelled block after the message. Repeatable. |

Shouldly keeps the subject, value formatting, difference highlighting, layout and the "Additional Info" tail. Reordering lines, custom difference rendering, declaring new kinds of failure and reaching the built-in wordings are not offered. A further control is added as a new method on `ShouldlyFailure`, never as a parameter or an overload.

## What else stays public, unchanged

- `ShouldAssertException`
- `ShouldlyCoreExtensions.AssertAwesomely*`
- `ShouldlyMessage` (the base class), `ExpectedShouldlyMessage`, `ActualShouldlyMessage`, `ExpectedActualShouldlyMessage`, `ExpectedActualToleranceShouldlyMessage`, `ExpectedActualWithCaseSensitivityShouldlyMessage` (the five documented in `extending.md` for v4)
- `ShouldlyThrowMessage`, `ExpectedEquivalenceShouldlyMessage` (undocumented, but in use)

## What leaves the public surface

- `IShouldlyAssertionContext`, `ShouldlyAssertionContext`, and the protected `ShouldlyMessage.ShouldlyAssertionContext` property
- `ActualFilteredWithPredicateShouldlyMessage`, `AsyncShouldlyNotThrowShouldlyMessage`, `AsyncShouldlyThrowShouldlyMessage`, `CompleteInShouldlyMessage`, `ExpectedActualIgnoreOrderShouldlyMessage`, `ExpectedActualKeyShouldlyMessage`, `ExpectedOrderShouldlyMessage`, `ShouldContainWithCountShouldlyMessage`, `TaskShouldlyThrowMessage`

## Rules for custom assertions

- **Names.** A message class constructed from a method named exactly like a built-in assertion gets that assertion's wording. Any other name gets the plain shape of the class constructed.
- **Tolerance and case.** The tolerance and case-sensitivity classes always print their tolerance or case option, whatever the method is called.
- **Negation.** A failure is negated when its phrase contains the word "not". This applies to the building block and the message classes alike.

## Evidence

Public C# code on GitHub, searched 2026-11-11, first 100 hits per name, copies of Shouldly's own source excluded. 57 repositories use the documented message classes or `AssertAwesomely`; 35 of them are copies of one course template, leaving 22 distinct codebases. Counts below are distinct codebases, from one sampled file per repository, so they are lower bounds.

| Type | Distinct codebases using it |
| --- | --- |
| `ExpectedActualShouldlyMessage` | 11 |
| `AssertAwesomely` | 9 |
| `ActualShouldlyMessage` | 5 |
| `ExpectedShouldlyMessage` | 5 |
| `ShouldlyThrowMessage` | 4 (one organisation) |
| `ExpectedEquivalenceShouldlyMessage` | 3 |
| `ShouldlyAssertionContext` | 1 |
| `IShouldlyAssertionContext` | 0 |
| Each of the nine removed message classes | 0 |

The controls come from workarounds found in the same sample:

| Workaround | Seen in | Control that replaces it |
| --- | --- | --- |
| A literal phrase passed in the method-name slot | Assertions.Web | `Phrase` |
| Subclassing a message class and string-replacing the expected line out of the output | Shane32/TestHelpers | Not calling `Expected` |
| A sentence in the actual slot, or the reason in the custom-message slot | nmshd/backbone, Shouldly.Json | `ActualInWords` |
| Words in the expected slot, shown quoted | Shouldly.Json, showplan, SurrogateAttribute.Fody | `ExpectedInWords` |
| Subclassing `ShouldlyMessage`, or wrapping the message, to append an HTTP response | Altinn, Assertions.Web | `Context` |

Only 1 of the 57 sampled files captures the subject with `[CallerArgumentExpression]`, which is why `For` takes it as a required argument.

## Considered options

- **Keep every type public.** Rejected: the context is the name-matching mechanism itself, so keeping it public freezes that mechanism for good and leaves built-in assertions with two ways of producing a message.
- **Replace the custom assertion surface with a new one.** Rejected: it breaks every one of the 22 known codebases and gains nothing they asked for.
- **Keep pattern matching for custom method names.** Rejected: it keeps 37 ordered predicates alive only for names that match by accident.
- **Offer no building block, and have authors who want control build their own string.** Rejected: Shouldly should not encourage hand-built messages, which lose the subject, value formatting and difference highlighting.
- **A three-member building block with the controls as optional parameters.** Rejected: it reaches three members by giving one parameter several meanings, and a parameter list can never grow.
- **A builder where only stated lines print, ending in `.Exception`.** Rejected: the plainest assertion becomes the longest to write, with the actual value passed twice.

## Consequences

- A custom assertion that subclasses `ShouldlyMessage` and assigns its own `ShouldlyAssertionContext` no longer compiles. One repository is known to do this. It moves to the building block.
- A custom method whose name matched a built-in pattern without equalling a built-in name (a `ShouldBeNullish`, say) gets the plain shape instead of the built-in wording.
- A custom method named exactly like a built-in one, such as a `ShouldBeNull` for a user type, keeps today's wording (#1092).
- A name that merely contains the letters "Not", such as `ShouldBeNoteworthy`, is no longer treated as negated.
- Three mistakes still compile, as they do in v4: swapping `customMessage` and `actualExpression`; building a failure without throwing it; and composing a subject such as `$"{actualExpression}.Errors"`, which hides a missing expression from the trip-wire. Catching the first would need a public subject type; the others suit an analyzer.
