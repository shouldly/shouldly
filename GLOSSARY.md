# Glossary

Domain terms used across Shouldly's code, docs and design discussions.

**Actual**: the value under test, the receiver of a `Should*` assertion.

**Expected**: the value the assertion compares the actual value against. Where Shouldly calls `Equals`, it calls it on the expected value, so a broken `Equals` on the value under test can't make an assertion pass (#1076).

**Subject**: the source text of the actual value's expression at the call site, such as `order.Total`. It opens every failure message.
_Avoid_: code part, caller argument expression (the compiler feature that captures it).

**Failure message**: the text Shouldly reports when an assertion fails: the subject, what the assertion required of it, and what was found instead.
_Avoid_: error message, Shouldly message (the v4 class family custom assertions construct).

**Phrase**: the words in a failure message that say what the assertion required of the subject, such as `should be awesome`.
_Avoid_: should method, method name (one source of a phrase, not the phrase).

**Captured sequence**: a sequence as an assertion read it. The check and the failure message both read the capture, never the sequence itself, so a sequence that can only be enumerated once is read once, and no further than the check and the message ask. Every built-in assertion with a sequence parameter captures it before checking it. Lives in `Shouldly.Internals.CapturedSequence`.
_Avoid_: proxy, snapshot, buffer.

**Equality**: Shouldly's single definition of when an actual value equals an expected one. The assertions that compare values (`ShouldBe`, `ShouldNotBe`, sequences, membership and dictionary assertions) ask it, rather than assembling their own comparer. An assertion holds an Equality for how it compares: Shouldly's own rules, or under a tolerance, case option or comparer of the caller's, in order or not. A tolerance or case option on a sequence assertion stands for an element comparer, which Equality compares the items with in place of its own rules. A comparer of the caller's is asked whether two values are equal with the expected value first, by every assertion that takes one; `ShouldBeUnique` and `ShouldBeSubsetOf` also hash with it, so a large collection can be matched quickly. The check and the failure message both read the one Equality, so the message describes the comparison that failed, and the items it marks as different are the ones the check found different. Lives in `Shouldly.Internals.Equality`.

**Number**: a value of one of .NET's built-in numeric types, `Half` included; not a `char`, an enum or a `BigInteger`. Equality and `ShouldBeEquivalentTo` compare two numbers by the values they hold, whatever their types, so `1` equals `1L`. Compared exactly, neither is rounded to the other's type first, so a `float` equals an `int` only when it holds exactly that integer. They give two numbers the same verdict. Lives in `Shouldly.Internals.Numbers`.

**Tolerance**: how far apart two numbers, or two dates and times, may be and still be equal. An assertion that takes a tolerance compares within it; otherwise floating-point numbers are compared within `ShouldlyConfiguration.DefaultFloatingPointTolerance`, zero unless configured, and every other number exactly. An assertion's own tolerance takes the place of the configured one; the two never add up. Whatever the tolerance, two NaNs are equal, an infinity equals only an infinity of the same sign, and no finite number is equal to an infinity. Lives in `Shouldly.Internals.Numbers` for numbers, and in `Shouldly.Internals.Equality` for dates and times.
_Avoid_: delta, epsilon, precision.

**Explanation**: the line that closes a `ShouldBe` failure message when the message doesn't otherwise show why the two values are unequal: they read the same, or they are sequences whose first difference takes finding. It names what decided the comparison (an equality rule, a custom comparer, a difference in type, reference equality, the type's own `Equals`) and, for a record or a tuple, the member it was decided in. Lives in `Shouldly.Internals.Failures.ExplanationWording`, which reads what decided from the Differences.
_Avoid_: hint.

**Differences**: what tells the actual value apart from the expected one, as the assertion's Equality compared them: the positions of two sequences that differ, what one of two unordered collections has that the other doesn't, the items a match in any order left without a partner, and which of Equality's steps compared the two. A failure message shows them but never finds them for itself. Lives in `Shouldly.Internals.Differences`.
_Avoid_: diff (the line-by-line comparison of two texts).

**Unordered collection**: a dictionary or a set, whose enumeration order is an implementation detail rather than part of its value. When both sides of a comparison are dictionaries or sets, Equality matches their items in any order, a dictionary's entries by key, and a failure message lists what one has that the other doesn't. When only one side is, the same holds unless it keeps an order of its own (a `SortedSet<T>`, an `OrderedDictionary`), which is compared with the other side's like any sequence's. `ShouldBeEquivalentTo` follows the same rule. Lives in `Shouldly.Internals.UnorderedCollection`.

**Equality rule**: a user-registered definition of equality for a type and its subtypes (`ShouldlyConfiguration.UseEquality<T>`), which Equality consults before its own rules, wherever it compares two values of that type. `ShouldBeEquivalentTo` consults them too, ahead of a type's shape: two values a rule covers are compared by the rule, as one value, and their members and items are not walked. In Equality, a value an equality rule covers is never walked as a collection, except by `ignoreOrder: true`, which matches its elements instead; `EquivalencyOptions` don't reach inside one. Shouldly ships none: it has no built-in knowledge of third-party types such as Newtonsoft.Json's `JToken`, whose users register `JToken.DeepEquals`. What it does know without a rule is .NET's own JSON: on .NET 8 and later, two `System.Text.Json` values (`JsonNode`, `JsonElement`) are compared as JSON by Equality and by `ShouldBeEquivalentTo`, when no rule applies. Lives in `Shouldly.Internals.JsonValues`.
_Avoid_: compare-as-object type (the v4 mechanism it replaces).
