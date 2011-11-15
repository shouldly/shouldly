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

Another example, if you compare two collections:
    
    (new[] { 1, 2, 3 }).ShouldBe(new[] { 1, 2, 4 });
 
and it fails because they're different, it'll show you the differences between the two collections:
        should be
    [1, 2, 4]
        but was
    [1, 2, 3]
        difference
    [1, 2, *3*]

If you want to check that a particular call does/does not throw an exception, it's as simple as:
    
    Should.Throw<ArgumentOutOfRangeException>(() => widget.Twist(-1));
    
Then if it chucks a wobbly, you have access to the exception to help debug what the underlying cause was.

Other *Shouldly* features:

    ##Equality
        ShouldBe
        ShouldNotBe
        ShouldBeGreaterThan(OrEqualTo)
        ShouldBeLessThan(OrEqualTo)
		ShouldBeTypeOf<T>

    ##Enumerable
    	ShouldBe(with Tolerance)
        ShouldContain
        ShouldContain(predicate)
        ShouldNotContain
        ShouldNotContain(predicate)
        ShouldBeEmpty
        ShouldNotBeEmpty

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
	
    ##Exceptions
        Should.Throw<T>(Action)
        Should.NotThrow(Action)

