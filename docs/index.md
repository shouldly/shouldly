### How asserting *Should* be

**NOTE: ** These docs are in progress! Get involved at [on GitHub](https://github.com/shouldly/shouldly/issues/308), contributions welcome! First time contributors welcome, we are happy to help you get started.

This is the old *Assert* way:

    Assert.That(contestant.Points, Is.EqualTo(1337));

For your troubles, you get this message, when it fails:

    Expected 1337 but was 0

How it **Should** be:

    contestant.Points.ShouldBe(1337);

Which is just syntax, so far, but check out the message when it fails:

    contestant.Points should be 1337 but was 0

It might be easy to underestimate how useful this is. Another example, side by side:

    Assert.That(map.IndexOfValue("boo"), Is.EqualTo(2));    // -> Expected 2 but was 1
    map.IndexOfValue("boo").ShouldBe(2);                    // -> map.IndexOfValue("boo") should be 2 but was 1

**Shouldly** uses the variables within the *ShouldBe* statement to report on errors, which makes diagnosing easier.

Another example, if you compare two collections:

    new[] { 1, 2, 3 }.ShouldBe(new[] { 1, 2, 4 });

and it fails because they're different, it'll show you the differences between the two collections:

        should be
    [1, 2, 4]
        but was
    [1, 2, 3]
        difference
    [1, 2, *3*]

Shouldly has plenty of different assertions, have a look under the assertions folder for all the options.
