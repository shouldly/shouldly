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

**Equality**: Shouldly's single definition of when an actual value equals an expected one. The assertions that compare values without a tolerance or case option (`ShouldBe`, `ShouldNotBe`, sequences, membership and dictionary assertions) ask it, rather than assembling their own comparer. Lives in `Shouldly.Internals.Equality`.

**Explanation**: the line that closes a `ShouldBe` failure message when the message doesn't otherwise show why the two values are unequal: they read the same, or they are sequences whose first difference takes finding. It names what decided the comparison (an equality rule, a custom comparer, a difference in type, reference equality, the type's own `Equals`) and, for a record or a tuple, the member it was decided in. Lives in `Shouldly.Internals.Inequality`, which follows the steps Equality takes.
_Avoid_: hint.

**Unordered collection**: a dictionary or a set, whose enumeration order is an implementation detail rather than part of its value. When both sides of a comparison are dictionaries or sets, Equality matches their items in any order, a dictionary's entries by key, and a failure message lists what one has that the other doesn't. When only one side is, the same holds unless it keeps an order of its own (a `SortedSet<T>`, an `OrderedDictionary`), which is compared with the other side's like any sequence's. `ShouldBeEquivalentTo` follows the same rule. Lives in `Shouldly.Internals.UnorderedCollection`.

**Equality rule**: a user-registered definition of equality for a type and its subtypes (`ShouldlyConfiguration.UseEquality<T>`), which Equality consults before its own rules, wherever it compares two values of that type. `ShouldBeEquivalentTo` doesn't consult equality rules: it compares member by member. A value an equality rule covers is never walked as a collection, except by `ignoreOrder: true`, which matches its elements instead. Shouldly ships none: it has no built-in knowledge of third-party types such as Newtonsoft.Json's `JToken`, whose users register `JToken.DeepEquals`.
_Avoid_: compare-as-object type (the v4 mechanism it replaces).
