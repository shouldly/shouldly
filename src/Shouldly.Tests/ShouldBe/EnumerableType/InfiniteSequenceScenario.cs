namespace Shouldly.Tests.ShouldBe.EnumerableType;

// Issue #1407: an infinite actual (or expected) value must produce a prompt, capped failure
// rather than hanging. GuardedSequence throws well before "forever", so a regression fails
// fast instead of hanging the run.
public class InfiniteSequenceScenario
{
    [Fact]
    public void InfiniteActualShouldFail()
    {
        var naturals = new GuardedSequence();

        using (MaxEnumerableItemsScope.Of(5))
        {
            Verify.ShouldFail(() => naturals.ShouldBe([0, 1]));
        }
    }

    [Fact]
    public void InfiniteActualIsOnlyReadAsFarAsTheMessageNeeds()
    {
        var naturals = new GuardedSequence();

        using (MaxEnumerableItemsScope.Of(5))
        {
            Should.Throw<ShouldAssertException>(() => naturals.ShouldBe([0, 1]));
        }

        // One past the cap, so the message knows whether to print "...".
        naturals.ElementsRead.ShouldBe(6);
        naturals.Disposed.ShouldBeTrue();
    }

    [Fact]
    public void InfiniteActualIsCappedAtTheDefaultLimit()
    {
        var message = Should.Throw<ShouldAssertException>(() => new GuardedSequence().ShouldBe([0, 1])).Message;

        message.ShouldContain("998, 999, ...]");
        message.ShouldContain("*998*, *999*, ...]");
        message.ShouldNotContain("1000");
    }

    [Fact]
    public void InfiniteExpectedShouldFail()
    {
        var naturals = new GuardedSequence();

        using (MaxEnumerableItemsScope.Of(5))
        {
            Verify.ShouldFail(() => new[] { 0, 1 }.ShouldBe(naturals));
        }
    }

    [Fact]
    public void InfiniteActualIgnoringOrderShouldFail()
    {
        var naturals = new GuardedSequence();

        using (MaxEnumerableItemsScope.Of(5))
        {
            Verify.ShouldFail(() => naturals.ShouldBe([1, 0], ignoreOrder: true));
        }
    }

    [Fact]
    public void InfiniteActualWithComparerShouldFail()
    {
        var naturals = new GuardedSequence();

        using (MaxEnumerableItemsScope.Of(5))
        {
            Verify.ShouldFail(() => naturals.ShouldBe([0, 1], EqualityComparer<int>.Default));
        }
    }

    [Fact]
    public void InfiniteActualShouldBeNullOrEmptyShouldFail()
    {
        var naturals = new GuardedSequence();

        using (MaxEnumerableItemsScope.Of(5))
        {
            Verify.ShouldFail(() => naturals.ShouldBeNullOrEmpty());
        }
    }

    [Fact]
    public void InfiniteActualShouldBeEmptyShouldFail()
    {
        var naturals = new GuardedSequence();

        using (MaxEnumerableItemsScope.Of(5))
        {
            Verify.ShouldFail(() => naturals.ShouldBeEmpty());
        }
    }

    [Fact]
    public void InfiniteActualShouldNotBeNullOrEmptyShouldPass()
    {
        var naturals = new GuardedSequence();

        naturals.ShouldNotBeNullOrEmpty();

        naturals.ElementsRead.ShouldBe(1);
    }
}
