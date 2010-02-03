Shouldly
========

How asserting shouldly be

This is the old way: 
    Assert.That(contestant.Points, Is.EqualTo(1337));
And you get this message, when it fails:
    "Expected 1337 but was 0"

That's not how it shouldly be...

    contestant.Points.ShouldBe(1337);
And you get this message when it fails
    contestant.Points should be 1337 but was 0

Clearly, Shouldly knows more about your tests

The Shouldly library has some useful features.
It integrates with RhinoMocks, to give clearly messages about expectation failures:

Here's the ShouldNoty's message:
    Rhino.Mocks.Exceptions.ExpectationViolationException:
    IContestant.PlayGame("Shouldly"); Expected #1, Actual #0


Here's Shouldly's message:
    Expected:
        IContestant.PlayGame("Shouldly");
    Recorded:
       IContestant.PlayGame("Debugging");
      IContestant.PlayGame("Logging");
      IContestant.PlayGame("Drinking coffee");
      IContestant.PlayGame("Commenting out test");


