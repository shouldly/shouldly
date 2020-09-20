# ShouldBe


## Objects

``ShouldBeExamples`` works on all types and compares using ``.Equals``.

<!-- snippet: ShouldBeObjects -->
<a id='snippet-shouldbeobjects'></a>
```cs
var theSimpsonsCat = new Cat { Name = "Santas little helper" };
theSimpsonsCat.Name.ShouldBe("Snowball 2");
```
<sup><a href='/src/DocumentationExamples/ShouldBeExamples.cs#L26-L31' title='File snippet `shouldbeobjects` was extracted from'>snippet source</a> | <a href='#snippet-shouldbeobjects' title='Navigate to start of snippet `shouldbeobjects`'>anchor</a></sup>
<!-- endSnippet -->

**Exception**

``` <!-- include: ShouldBeExamples.Objects.exceptionText.approved.txt. path:  -->
theSimpsonsCat.Name
    should be
"Snowball 2"
    but was
"Santas little helper"
    difference
Difference     |       |    |    |    |    |    |         |    |    |    |    |    |    |    |    |    |    |    |   
               |      \|/  \|/  \|/  \|/  \|/  \|/       \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   
Expected Value | S    n    o    w    b    a    l    l    \s   2                                                      
Actual Value   | S    a    n    t    a    s    \s   l    i    t    t    l    e    \s   h    e    l    p    e    r    
Expected Code  | 83   110  111  119  98   97   108  108  32   50                                                     
Actual Code    | 83   97   110  116  97   115  32   108  105  116  116  108  101  32   104  101  108  112  101  114  
```


## Numeric

``ShouldBe`` numeric overloads accept tolerances and has overloads for ``float``, ``double`` and ``decimal`` types.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.Numeric.codeSample.approved.txt

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.Numeric.exceptionText.approved.txt


## DateTime(Offset)

DateTime overloads are similar to the numeric overloads and support tolerances.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.DateTime.codeSample.approved.txt

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.DateTime.exceptionText.approved.txt


## TimeSpan

TimeSpan also has tolerance overloads

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.TimeSpanExample.codeSample.approved.txt

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.TimeSpanExample.exceptionText.approved.txt


## Enumerables

Enumerable comparison is done on the elements in the enumerable, so you can compare an array to a list and have it pass.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.Enumerables.codeSample.approved.txt

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.Enumerables.exceptionText.approved.txt


## Enumerables of Numerics

If you have enumerables of ``float``, ``decimal`` or ``double`` types then you can use the tolerance overloads, similar to the value extensions.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.EnumerablesOfNumerics.codeSample.approved.txt

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.EnumerablesOfNumerics.exceptionText.approved.txt


## Bools

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.BooleanExample.codeSample.approved.txt

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBeExamples.BooleanExample.exceptionText.approved.txt
