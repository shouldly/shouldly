ShouldNotBe
===========

``ShouldNotBe`` is the inverse of ``ShouldBe``.

Objects
-------
``ShouldNotBe`` works on all types and compares using ``.Equals``.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/Objects.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/Objects.exceptionText.approved.txt

Numeric
-------
``ShouldNotBe`` also allows you to compare numeric values, regardless of their value type.

Integer
```````
.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/NumericInt.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/NumericInt.exceptionText.approved.txt

Long
````
.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/NumericLong.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/NumericLong.exceptionText.approved.txt

DateTime(Offset)
----------------
``ShouldNotBe`` DateTime overloads are similar to the numeric overloads and also support tolerances.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/DateTime.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/DateTime.exceptionText.approved.txt

TimeSpan
--------

``TimeSpan`` also has tolerance overloads

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/TimeSpanExample.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldNotBe/TimeSpanExample.exceptionText.approved.txt

Want to contribute to Shouldly? `#303 <https://github.com/shouldly/shouldly/issues/303>`_ makes this error message better!
