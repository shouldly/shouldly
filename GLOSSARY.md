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

**Equality**: Shouldly's single definition of when an actual value equals an expected one. The assertions that compare values without a tolerance or case option (`ShouldBe`, `ShouldNotBe`, sequences, membership and dictionary assertions) ask it, rather than assembling their own comparer. Lives in `Shouldly.Internals.Equality`.

**Equality rule**: a user-registered definition of equality for a type and its subtypes (`ShouldlyConfiguration.UseEquality<T>`), which Equality consults before its own rules, wherever it compares two values of that type. `ShouldBeEquivalentTo` doesn't consult equality rules: it compares member by member. A value an equality rule covers is never walked as a collection, except by `ignoreOrder: true`, which matches its elements instead. Shouldly ships none: it has no built-in knowledge of third-party types such as Newtonsoft.Json's `JToken`, whose users register `JToken.DeepEquals`.
_Avoid_: compare-as-object type (the v4 mechanism it replaces).
