Shouldly
========

Shouldly is the "should" style test library you reach for when you've said "WTF just happened?!".  

Pop quiz friend! Fix this error:

    Expected 1337 but was 0

> Ok, let me just set a breakpoint, fire up my debugger and...

Now stop right there! Put down that debugger and pick up Shouldly! It transforms your error into this:

    contestant.Points should be 1337 but was 0

> Ahhh! I get it, I need more points! But how do I write these magical tests? I LOVE MY NUNIT ASSERT.THAT!!

Really.

> Yes

Really??

> Yes!!

You like writing this?

    Assert.That(contestant.Points, Is.EqualTo(1337));

> YES!!

What if you could write your test like this!

    contestant.Points.ShouldBe(1337);

> ZOMG!1  Wow, what else can it do??

Well apart from having a whole host of "Shoulds":

    ShouldBe
    ShouldBeGreaterThan
    ShouldBeLessThan
    ShouldContain
    ShouldNotContain
    ShouldBeCloseTo

> Wait, wait, shouldBeCloseTo?? Come on buddy, we're testing here, it's a precise activity, it's either equal or not, pass or fail, red or green capeche!?

Uh huh. Let's just say for interest, that you're testing XML in a string.

> Ewwwwwww

It looks like this:
    
    <enterprisey-configuration-block><enterprisey-logging>
        <loglevel alert="brown" />
      </enterprisey-logging>    </enterprisey-configuration-block>
    
Who formatted that so ugly!? Now we have to write an ugly string test to match it right? Wrong! I don't actually care if it doesn't match the whitespace properly:

    xmlConfig.ShouldBeCloseTo(@"
    <enterprisey-configuration-block>
      <enterprisey-logging>
        <loglevel alert='brown' />
      </enterprisey-logging>    
    </enterprisey-configuration-block>");

Now we have a nice flexible test and our enterprise can sleep safe knowing it's precious xml is being cared for.

Notice the quotes around "brown"? Shouldly will do the compare, ignoring whitespace and not fussing over which quotes you used.

> Ok Dave, this is cool and you're really awesome and stuff but what about when I get null refs in my mock tests or expectation violations?

    Rhino.Mocks.Exceptions.ExpectationViolationException:
    IContestant.PlayGame("Shouldly"); Expected #1, Actual #0
> I still have to run up my debugger to figure out why my method didn't get called right??

You could do that.

Or, you could write your test like this:

    contestant.ShouldHaveBeenCalled(c => c.PlayGame("Shouldly"));

and get this!

    Expected:
      IContestant.PlayGame("Shouldly");
    Recorded:
      IContestant.PlayGame("Debugging");
      IContestant.PlayGame("Logging");
      IContestant.PlayGame("Drinking coffee");
      IContestant.PlayGame("Commenting out test");
      
Now we see what's really happening here!

> Wowee! How do I get started?

Just download or git clone the latest and either include it the source in your project or run msbuild and copy the dll into your repository.

Oh and feel free to fork and improve friends, let's all help each other out.