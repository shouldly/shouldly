UPDATE: This repo has moved to [http://github.com/shouldly/shouldly](http://github.com/shouldly/shouldly)

Shouldly
========

### How asserting *Should* be

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

The **Shouldly** library has other features.
It integrates with RhinoMocks, to give clear messages about expectation failures:
Here's the message without Shouldly; helpful, but not great...
    Rhino.Mocks.Exceptions.ExpectationViolationException:
    IContestant.PlayGame("Shouldly"); Expected #1, Actual #0

**Shouldly's** message:
    Expected:
        IContestant.PlayGame("Shouldly");
    Recorded:
      IContestant.PlayGame("Debugging");
      IContestant.PlayGame("Logging");
      IContestant.PlayGame("Drinking coffee");
      IContestant.PlayGame("Commenting out test");

Other *Shouldly* features:
    ##Equality
        ShouldBe
        ShouldNotBe
        ShouldBeGreaterThan(OrEqualTo)
        ShouldBeLessThan(OrEqualTo)
		ShouldBeTypeOf<T>

    ##Enumerable
		ShouldContain
		ShouldContain(predicate)
		ShouldNotContain
		ShouldNotContain(predicate)

    ##String
        ShouldBeCloseTo
        ShouldStartWith
        ShouldEndWith
        ShouldContain
        ShouldNotContain
        ShouldContainWithoutWhitespace
        ShouldMatch

    ##Dictionary
        ShouldContainKeyShouldContainKeyAndValue
        ShouldNotContainKey
        ShouldNotContainKeyAndValue

    ##Rhino Mocks
        ShouldHaveBeenCalled

