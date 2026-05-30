# AssertionScope

`AssertionScope` collects multiple assertion failures and reports them all at once when the scope is disposed, instead of stopping at the first failure. This is useful when you want to verify several properties of an object and see all the failures together.

## Usage

<!-- snippet: AssertionScopeExamples.BasicUsage.codeSample.approved.cs -->
<a id='snippet-AssertionScopeExamples.BasicUsage.codeSample.approved.cs'></a>
```cs
var homer = new Person { Name = "Homer", Salary = 30000 };
using var scope = new AssertionScope();
homer.Name.ShouldBe("Mr.Burns");
homer.Salary.ShouldBeGreaterThan(1000000);
```
<sup><a href='/src/DocumentationExamples/CodeExamples/AssertionScopeExamples.BasicUsage.codeSample.approved.cs#L1-L4' title='Snippet source file'>snippet source</a> | <a href='#snippet-AssertionScopeExamples.BasicUsage.codeSample.approved.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: AssertionScopeExamples.BasicUsage.exceptionText.approved.txt -->
```
2 assertions in this scope failed:
---------------- Error 1 ----------------
    homer.Name
        should be
    "Mr.Burns"
        but was
    "Homer"
        difference
    Expected: "Mr.Burns"
    Actual:   "Homer"

---------------- Error 2 ----------------
    homer.Salary
        should be greater than
    1000000
        but was
    30000

-----------------------------------------
```
<!-- endInclude -->


## Nesting

Scopes can be nested. Failures from inner scopes propagate to the outermost scope, which throws the aggregated exception:

```cs
using var outer = new AssertionScope();
// assertions here...

using (new AssertionScope())
{
    // inner assertions - failures propagate to outer
}
// outer scope collects everything and throws on dispose
```

## How it works

- Wrap assertions in a `using var scope = new AssertionScope()` (or `using (new AssertionScope()) { }`)
- Any assertion that fails within the scope is **recorded** instead of thrown immediately
- When the scope is disposed, all recorded failures are thrown as a single `ShouldAssertException`
- Without an `AssertionScope`, assertions behave as usual — failing immediately on the first error

## Return values and nullability inside a scope

Because a failing assertion is recorded rather than thrown, it cannot short-circuit the rest of your test while a scope is active. Two consequences follow:

- Assertions that normally return a value — for example `ShouldNotBeNull()`, `Should.Throw<T>()` or `ShouldBeOfType<T>()` — return `null`/`default` when they fail inside a scope, because there is no valid value to hand back.
- Assertions that normally narrow nullability or guarantee a condition on return (via `[NotNull]`, `[ContractAnnotation]`, etc.) no longer make that guarantee inside a scope, since they can record a failure and continue.

The recorded failure is always reported when the scope is disposed, so the cause is never lost. But if you chain off such a result inside a scope, guard it defensively — otherwise dereferencing the unchecked value will end the scope early and any later assertions won't be collected:

```cs
using var scope = new AssertionScope();

var name = customer.Name.ShouldNotBeNull(); // may be null inside a scope if this failed
if (name is not null)
    name.Length.ShouldBeGreaterThan(0);
```

## Comparison with ShouldSatisfyAllConditions

`ShouldSatisfyAllConditions` and `AssertionScope` both collect multiple failures, but they work differently:

| | `ShouldSatisfyAllConditions` | `AssertionScope` |
|---|---|---|
| **Scope** | Single method call | Any block of code |
| **Syntax** | Lambda per assertion | Regular assertion calls |
| **Nesting** | N/A | Inner scopes propagate to outer |
| **Use case** | Validating one object | Validating across a larger test |
