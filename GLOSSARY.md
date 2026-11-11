# Glossary

Domain terms used across Shouldly's code, docs and design discussions.

**Actual**: the value under test, the receiver of a `Should*` assertion.

**Expected**: the value the assertion compares the actual value against. Where Shouldly calls `Equals`, it calls it on the expected value, so a broken `Equals` on the value under test can't make an assertion pass (#1076).

**Equality**: Shouldly's single definition of when an actual value equals an expected one. Every assertion family that compares values (`ShouldBe`, `ShouldNotBe`, membership, dictionary and tolerance assertions) asks it, rather than assembling its own comparer. Lives in `Shouldly.Internals.Equality`.

**Equality rule**: a user-registered definition of equality for a type and its subtypes (`ShouldlyConfiguration.UseEquality<T>`), which Equality consults before its own rules, wherever it compares two values of that type. A value an equality rule covers is never walked as a collection, except by `ignoreOrder: true`, which matches its elements instead. Shouldly ships none: it has no built-in knowledge of third-party types such as Newtonsoft.Json's `JToken`, whose users register `JToken.DeepEquals`.
_Avoid_: compare-as-object type (the v4 mechanism it replaces).
