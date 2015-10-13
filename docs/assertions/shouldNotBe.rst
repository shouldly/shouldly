ShouldNotBe
===========

``ShouldNotBe`` is the inverse of ``ShouldBe``.

Objects
-------
``ShouldNotBe`` works on all types and compares using ``.Equals``.

.. code-block:: c#

  var theSimpsonsCat = new Cat() { Name = "Santas little helper" };
  theSimpsonsCat.Name.ShouldNotBe("Santas little helper");

Exception::

  theSimpsonsCat.Name
      should not be
  "Santas little helper"
      but was
  "Santas little helper"

Want to contribute to Shouldly? `#304 <https://github.com/shouldly/shouldly/issues/304>`_ makes this error message better!

Numeric
-------
``ShouldNotBe`` also allows you to compare numeric values, regardless of their value type.

.. code-block:: c#

  const int one = 1;
  one.ShouldNotBe(1)

Exception::

  one should not be 1 but was 1

.. code-block:: c#

  const long aLong = 1L;
  aLong.ShouldNotBe(1);

Exception::

  aLong should not be 1 but was 1

DateTime(Offset)
----------------
``ShouldNotBe`` DateTime overloads are similar to the numeric overloads and also support tolerances.

.. code-block:: c#

  var date = new DateTime(2000, 6, 1);
  date.ShouldNotBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1.5));

Exception::

  date
      should not be within
  01:30:00
      of
  01/06/2000 01:00:01
      but was
  01/06/2000 00:00:00

TimeSpan
--------

``TimeSpan`` also has tolerance overloads

.. code-block:: c#

  var timeSpan = TimeSpan.FromHours(1);
  timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d));

Exception::

  timeSpan
      should not be within
  01:30:00
      of
  02:06:00
      but was
  01:00:00

Want to contribute to Shouldly? `#303 <https://github.com/shouldly/shouldly/issues/303>`_ makes this error message better!
