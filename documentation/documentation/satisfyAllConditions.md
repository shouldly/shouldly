# ShouldSatisfy

Asserts that every one of the supplied conditions holds, reporting *all* the failures at once rather than stopping at the first.

Which form to use:

- **All the assertions hang off a common subject** → use the `value.ShouldSatisfy([...])` extension method. The value under test is passed to each condition, and it appears as the subject in the failure message.
- **A group of otherwise unrelated assertions** → use the static `Should.Satisfy([...])` method. There is no single subject to pass, so each condition is a self-contained assertion.

> `ShouldSatisfyAllConditions` is the original name for this assertion. It still works but is now obsolete: it cannot capture the asserted expression via `CallerArgumentExpression` and falls back to stack-trace parsing, which is not trim- or AOT-safe. Prefer `ShouldSatisfy` / `Should.Satisfy`.

## A common subject — `ShouldSatisfy`

<!-- snippet: ShouldSatisfyAllConditionsExamples.ShouldSatisfy.codeSample.approved.cs -->
<a id='snippet-ShouldSatisfyAllConditionsExamples.ShouldSatisfy.codeSample.approved.cs'></a>
```cs
var mrBurns = new Person { Name = null };
mrBurns.ShouldSatisfy(
                [
                    p => p.Name.ShouldNotBeNullOrEmpty(),
                    p => p.Name.ShouldBe("Mr.Burns")
                ]);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldSatisfyAllConditionsExamples.ShouldSatisfy.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldSatisfyAllConditionsExamples.ShouldSatisfy.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: ShouldSatisfyAllConditionsExamples.ShouldSatisfy.exceptionText.approved.txt -->
```
mrBurns
    should satisfy all the conditions specified, but does not.
The following errors were found ...
---------------- Error 1 ----------------
    p.Name (null)
        should not be null or empty

---------------- Error 2 ----------------
    p.Name
        should be
    "Mr.Burns"
        but was
    null

-----------------------------------------
```
<!-- endInclude -->


## Unrelated conditions — `Should.Satisfy`

<!-- snippet: ShouldSatisfyAllConditionsExamples.Satisfy.codeSample.approved.cs -->
<a id='snippet-ShouldSatisfyAllConditionsExamples.Satisfy.codeSample.approved.cs'></a>
```cs
var mrBurns = new Person { Name = null };
var homer = new Person { Name = "Homer" };
Should.Satisfy(
                [
                    () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                    () => homer.Name.ShouldBe("Mr.Burns")
                ]);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldSatisfyAllConditionsExamples.Satisfy.codeSample.approved.cs#L1-L7' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldSatisfyAllConditionsExamples.Satisfy.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: ShouldSatisfyAllConditionsExamples.Satisfy.exceptionText.approved.txt -->
```
The conditions specified should all be satisfied, but were not.
The following errors were found ...
---------------- Error 1 ----------------
    mrBurns.Name (null)
        should not be null or empty

---------------- Error 2 ----------------
    homer.Name
        should be
    "Mr.Burns"
        but was
    "Homer"
        difference
    Expected: "Mr.Burns"
    Actual:   "Homer"

-----------------------------------------
```
<!-- endInclude -->
