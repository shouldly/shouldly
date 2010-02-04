Shouldly
========

### How asserting *Shouldly* be

This is the old *Assert* way: 
    Assert.That(contestant.Points, Is.EqualTo(1337));
For your troubles, you get this message, when it fails:
    "Expected 1337 but was 0"

How it **Shouldly** be one..
    contestant.Points.ShouldBe(1337);
Which is just syntax, so far, but check out the message when it fails:
    contestant.Points should be 1337 but was 0

**Shouldly** uses the variables within the *ShouldBe* statement to report on errors.

The **Shouldly** library has other useful features.
It integrates with RhinoMocks, to give clearly messages about expectation failures:
Here's the *Assert* message:
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
    ShouldBe
    ShouldBeGreaterThan
    ShouldBeLessThan
    ShouldContain
    ShouldNotContain
    ShouldBeCloseTo

