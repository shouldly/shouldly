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
--------------- Error 1 ---------------
    homer.Name
        should be
    "Mr.Burns"
        but was
    "Homer"
        difference
    Difference     |  |    |    |    |    |    |    |    |   
                   | \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
    Index          | 0    1    2    3    4    5    6    7    
    Expected Value | M    r    .    B    u    r    n    s    
    Actual Value   | H    o    m    e    r                   
    Expected Code  | 77   114  46   66   117  114  110  115  
    Actual Code    | 72   111  109  101  114                 

--------------- Error 2 ---------------
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

## Comparison with ShouldSatisfyAllConditions

`ShouldSatisfyAllConditions` and `AssertionScope` both collect multiple failures, but they work differently:

| | `ShouldSatisfyAllConditions` | `AssertionScope` |
|---|---|---|
| **Scope** | Single method call | Any block of code |
| **Syntax** | Lambda per assertion | Regular assertion calls |
| **Nesting** | N/A | Inner scopes propagate to outer |
| **Use case** | Validating one object | Validating across a larger test |
