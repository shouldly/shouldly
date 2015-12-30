Shouldly has a few configuration options:

DefaultFloatingPointTolerance
=============================
Allows specifying a floating point tolerance for all assertions

**Default value:** 0.0d

DefaultTaskTimeout
==================
:code:`Should.Throw(Func<Task>)` blocks, the timeout is a safeguard for deadlocks.

Shouldly runs the lambda without a synchronisation context, but deadlocks are still possible. Use :code:`Should.ThrowAsync` to be safe then await the returned task to prevent possible deadlocks.

**Default value:** 10 seconds

CompareAsObjectTypes
====================
Types which also are IEnumerable of themselves.

An example is :code:`Newtonsoft.Json.Linq.JToken` which looks like this :code:`class JToken : IEnumerable<JToken>`.

**Default value:** Newtonsoft.Json.Linq.JToken
