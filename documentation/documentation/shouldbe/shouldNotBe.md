# ShouldNotBe

`ShouldNotBe` is the inverse of `ShouldBe`.

Objects
-------
`ShouldNotBe` works on all types and compares using ``.Equals``.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.Objects.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.Objects.exceptionText.approved.txt

Numeric
-------
``ShouldNotBe`` also allows you to compare numeric values, regardless of their value type.

Integer
```````
.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.NumericInt.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.NumericInt.exceptionText.approved.txt

Long
````
.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.NumericLong.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.NumericLong.exceptionText.approved.txt

DateTime(Offset)
----------------
``ShouldNotBe`` DateTime overloads are similar to the numeric overloads and also support tolerances.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.DateTime.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.DateTime.exceptionText.approved.txt

TimeSpan
--------

``TimeSpan`` also has tolerance overloads

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.TimeSpanExample.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBeExamples.TimeSpanExample.exceptionText.approved.txt

Want to contribute to Shouldly? `#303 <https://github.com/shouldly/shouldly/issues/303>`_ makes this error message better!
