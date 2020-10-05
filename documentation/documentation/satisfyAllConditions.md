# ShouldSatisfyAllConditions

<!-- snippet: ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditions.codeSample.approved.cs -->
<a id='99f0f74e'></a>
```cs
var mrBurns = new Person { Name = null };
mrBurns.ShouldSatisfyAllConditions
                    (
                        () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                        () => mrBurns.Name.ShouldBe("Mr.Burns")
                    );
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditions.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#99f0f74e' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditions.exceptionText.approved.txt -->
```
mrBurns
    should satisfy all the conditions specified, but does not.
The following errors were found ...
--------------- Error 1 ---------------
    mrBurns.Name (null)
        should not be null or empty

--------------- Error 2 ---------------
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
<a id='ed11a6af'></a>
```cs
var mrBurns = new Person { Name = null };
mrBurns.ShouldSatisfyAllConditions
                    (
                        p => p.Name.ShouldNotBeNullOrEmpty(),
                        p => p.Name.ShouldBe("Mr.Burns")
                    );
```
<sup><a href='/src/DocumentationExamples/CodeExamples/ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditionsGeneric.codeSample.approved.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#ed11a6af' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


**Exception**

<!-- include: ShouldSatisfyAllConditionsExamples.ShouldSatisfyAllConditionsGeneric.exceptionText.approved.txt -->
```
mrBurns
    should satisfy all the conditions specified, but does not.
The following errors were found ...
--------------- Error 1 ---------------
    p => p.Name (null)
        should not be null or empty

--------------- Error 2 ---------------
    p => p.Name
        should be
    "Mr.Burns"
        but was
    null

-----------------------------------------
```
<!-- endInclude -->
