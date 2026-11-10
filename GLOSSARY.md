# Glossary

Domain terms used across Shouldly's code, docs and design discussions.

**Actual**: the value under test, the receiver of a `Should*` assertion.

**Expected**: the value the assertion compares the actual value against. Where Shouldly calls `Equals`, it calls it on the expected value, so a broken `Equals` on the value under test can't make an assertion pass (#1076).

**Equality**: Shouldly's single definition of when an actual value equals an expected one. Every assertion family that compares values (`ShouldBe`, `ShouldNotBe`, membership, dictionary and tolerance assertions) asks it, rather than assembling its own comparer. Lives in `Shouldly.Internals.Equality`.
