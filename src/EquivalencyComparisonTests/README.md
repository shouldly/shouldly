# Equivalency comparison tests

This project runs the same scenarios through Shouldly's `ShouldBeEquivalentTo` and
FluentAssertions' `BeEquivalentTo` (v7.2, the last Apache-2.0 licensed version, default options)
and asserts on *both* outcomes. It is an executable specification of where the two libraries
agree and diverge, modeled on the scenarios in
[FluentAssertions.Equivalency.Specs](https://github.com/fluentassertions/fluentassertions/tree/main/Tests/FluentAssertions.Equivalency.Specs).

Each test either documents parity (`ShouldAgree`) or pins down a divergence (`ShouldDiverge`)
with a `because:` explaining the behavioral difference. When Shouldly's equivalency defaults
change, the affected divergence tests fail, forcing the change to be acknowledged here — the
suite doubles as a progress metric for FluentAssertions convergence.

## Verified divergences (Shouldly 5.0-preview vs FluentAssertions 7.2 defaults)

Every remaining divergence is deliberate; the rationale is recorded in the roadmap
(shouldly#1265) and pinned by a `ShouldDiverge` test.

| Scenario | Shouldly | FluentAssertions | Why Shouldly diverges |
| --- | --- | --- | --- |
| Lists/arrays with same elements, different order (incl. nested in graphs) | Fail — ordered element-by-element | Pass — order-insensitive by default (byte arrays excepted) | Consistent with `ShouldBe`; respects the collection's contract; precise "element [i]" messages; a strict default fails loudly instead of masking ordering bugs. Opt out with `IgnoreOrder` |
| Class overriding `Equals`: equal by members, unequal by `Equals` (or vice versa) | Compares members, ignores `Equals` | Honors the `Equals` override (value semantics) | People reach for equivalency precisely to compare structure; `ShouldBe` already honors `Equals`; avoids the false pass where `Equals` hides real data differences |
| Different enum types with the same underlying value | Fail | Pass — enums compared by value | Two distinct enum types agreeing numerically is usually an accident |
| Cyclic object graphs, identical shape | Pass — visited pairs tracked by reference | Fail — cycles rejected by default | Strictly more useful; no configuration needed |
| Object graphs deeper than 10 levels | Pass — unbounded recursion | Fail — 10-level depth cap by default | Safe with correct cycle tracking |
| Root objects held in base-typed references with differing derived-only members | Fail — the `object`-typed API cannot see the static type, so the root falls back to the expectation's runtime type | Pass — the generic API's static type drives member selection | API-shape consequence, not a semantic choice |
| Two `new object()`s (no members) | Fail — vacuous-comparison guard with guidance | **Error** — "no members" `InvalidOperationException` | Both refuse a zero-member comparison; Shouldly fails the assertion with an actionable message instead of throwing |

Everywhere else tested, the two libraries agree — including the scenarios that diverged before
the v5 rewrite: cross-type and anonymous-type expectations (subset semantics, by the
expectation's members), declared-type member selection, dictionaries matched by key with
recursion into values, sets compared order-insensitively, container types ignored
(array ≡ `List<T>`), lossless cross-numeric equality (`int 1` ≡ `long 1`), multidimensional
array shape checks, indexers skipped, `Type`/`Uri` as leaf values, strings (ordinal,
case-sensitive), public fields, private member exclusion, structs and records member-wise,
`DateTime` (tick-exact, `Kind`-insensitive), `DateTimeOffset` (instant), `decimal` scale,
`Guid`, NaN, nullable lifting, null handling, and duplicate multiplicity in collections.
