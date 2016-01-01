ShouldBe
========

Objects
-------
``ShouldBe`` works on all types and compares using ``.Equals``.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/Objects.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/Objects.exceptionText.approved.txt

Numeric
-------
``ShouldBe`` numeric overloads accept tolerances and has overloads for ``float``, ``double`` and ``decimal`` types.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/Numeric.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/Numeric.exceptionText.approved.txt

DateTime(Offset)
----------------
DateTime overloads are similar to the numeric overloads and support tolerances.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/DateTime.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/DateTime.exceptionText.approved.txt

TimeSpan
--------
TimeSpan also has tolerance overloads

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/TimeSpanExample.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/TimeSpanExample.exceptionText.approved.txt

Want to improve shouldy? We have an open issue at [#303](https://github.com/shouldly/shouldly/issues/303) to improve this error message!

Enumerables
-----------
Enumerable comparison is done on the elements in the enumerable, so you can compare an array to a list and have it pass.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/Enumerables.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/Enumerables.exceptionText.approved.txt

Enumerables of Numerics
-----------------------
If you have enumerables of ``float``, ``decimal`` or ``double`` types then you can use the tolerance overloads, similar to the value extensions.

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/EnumerablesOfNumerics.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/EnumerablesOfNumerics.exceptionText.approved.txt


Bools
-----
.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/BooleanExample.codeSample.approved.txt
	:language: c#

**Exception**

.. literalinclude:: /../src/DocumentationExamples/CodeExamples/ShouldBe/BooleanExample.exceptionText.approved.txt