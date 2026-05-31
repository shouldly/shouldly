# ShouldSatisfy

Asserts that a value satisfies every one of the supplied conditions, reporting *all* the failures at once rather than stopping at the first.

> `ShouldSatisfyAllConditions` is the original name for this assertion. It still works but is now obsolete: it cannot capture the asserted expression via `CallerArgumentExpression` and falls back to stack-trace parsing, which is not trim- or AOT-safe. Prefer `ShouldSatisfy`.

<!-- snippet: ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditions.codeSample.approved.cs -->
<a id='snippet-ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditions.codeSample.approved.cs'></a>
```cs
var mrBurns = new Person { Name = null };
mrBurns.ShouldSatisfy(
                [
                    () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                    () => mrBurns.Name.ShouldBe("Mr.Burns")
                ]);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditions.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditions.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditions.exceptionText.approved.txt -->
```
mrBurns
    should satisfy all the conditions specified, but does not.
The following errors were found ...
---------------- Error 1 ----------------
    mrBurns.Name (null)
        should not be null or empty

---------------- Error 2 ----------------
    mrBurns.Name
        should be
    "Mr.Burns"
        but was
    null

-----------------------------------------
```
<!-- endInclude -->


## Generic

<!-- snippet: ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditionsGeneric.codeSample.approved.cs -->
<a id='snippet-ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditionsGeneric.codeSample.approved.cs'></a>
```cs
var mrBurns = new Person { Name = null };
mrBurns.ShouldSatisfy(
                [
                    p => p.Name.ShouldNotBeNullOrEmpty(),
                    p => p.Name.ShouldBe("Mr.Burns")
                ]);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditionsGeneric.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditionsGeneric.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditionsGeneric.exceptionText.approved.txt -->
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
